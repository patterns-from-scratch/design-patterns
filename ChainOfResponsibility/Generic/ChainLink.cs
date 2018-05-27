using System;

namespace ChainOfResponsibility.Generic
{
    public interface IProcessor<T>
    {
        void Process(T item);
    }

    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T item);
    }

    public class Specification : ISpecification<int>
    {
        public bool IsSatisfiedBy(int item) => false;
    }

    public class Singleton : ISpecification<int>
    {
        private static Singleton _Instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Singleton();
                }
                return _Instance;
            }
        }

        public bool IsSatisfiedBy(int item)
        {
            //read excel or sth
            return true;
        }
    }

    public sealed class ChainLink<T>
    {
        ChainLink<T> Successor { get; }
        Func<T, bool> CanProcess { get; }
        Action<T> PerformProcess { get; }

        public ChainLink(Func<T, bool> canProcess, Action<T> process, ChainLink<T> successor = null)
        {
            CanProcess = canProcess;
            PerformProcess = process;
            Successor = successor;
        }

        public ChainLink(Func<T, bool> canProcess, IProcessor<T> processor, ChainLink<T> successor = null)
            : this(canProcess, processor.Process, successor) { }

        public ChainLink(ISpecification<T> specification, Action<T> process, ChainLink<T> successor = null)
            : this(specification.IsSatisfiedBy, process, successor) { }

        public ChainLink(ISpecification<T> specification, IProcessor<T> processor, ChainLink<T> successor = null)
            : this(specification.IsSatisfiedBy, processor.Process, successor) { }

        public void Process(T item, Action<T> defaultAction)
        {
            if (CanProcess(item)) { PerformProcess(item); }
            else if (Successor is ChainLink<T> successor) { successor.PerformProcess(item); }
            else { defaultAction(item); }
        }
    }

    public interface ITransformer<T, Y>
    {
        Y Transform(T item);
    }

    public sealed class ChainLink<TSource, TTarget>
    {
        ChainLink<TSource, TTarget> Successor { get; }
        Func<TSource, bool> CanProcess { get; }
        Func<TSource, TTarget> PerformTransform { get; }

        public ChainLink(Func<TSource, bool> canProcess, Func<TSource, TTarget> transform, ChainLink<TSource, TTarget> successor = null)
        {
            CanProcess = canProcess;
            PerformTransform = transform;
            Successor = successor;
        }

        public ChainLink(Func<TSource, bool> canProcess, ITransformer<TSource, TTarget> transformer, ChainLink<TSource, TTarget> successor = null)
            : this(canProcess, transformer.Transform, successor) { }

        public ChainLink(ISpecification<TSource> specification, Func<TSource, TTarget> transform, ChainLink<TSource, TTarget> successor = null)
            : this(specification.IsSatisfiedBy, transform, successor) { }

        public ChainLink(ISpecification<TSource> specification, ITransformer<TSource, TTarget> transformer, ChainLink<TSource, TTarget> successor = null)
            : this(specification.IsSatisfiedBy, transformer.Transform, successor) { }

        public TTarget Transform(TSource item, TTarget defaultResult)
        {
            if (CanProcess(item)) { return PerformTransform(item); }
            else if (Successor is ChainLink<TSource, TTarget> successor) { return successor.PerformTransform(item); }
            else { return defaultResult; }
        }
    }
}
