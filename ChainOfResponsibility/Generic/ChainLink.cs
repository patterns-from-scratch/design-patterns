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
        public bool IsSatisfiedBy(int item)
        {
            throw new NotImplementedException();
        }
    }

    public class ChainLink<T>
    {
        ChainLink<T> Successor { get; }
        Func<T, bool> CanProcess { get; }
        Action<T> Process { get; }

        public ChainLink(Func<T, bool> canProcess, Action<T> process, ChainLink<T> successor = null)
        {
            this.CanProcess = canProcess;
            this.Process = process;
            this.Successor = successor;
        }

        public ChainLink(Func<T, bool> canProcess, IProcessor<T> processor, ChainLink<T> successor = null)
            : this(canProcess, processor.Process, successor) { }

        public ChainLink(ISpecification<T> specification, Action<T> process, ChainLink<T> successor = null)
            : this(specification.IsSatisfiedBy, process, successor) { }

        public ChainLink(ISpecification<T> specification, IProcessor<T> processor, ChainLink<T> successor = null)
            : this(specification.IsSatisfiedBy, processor.Process, successor) { }

        public void TryProcess(T item)
        {
            if (this.CanProcess(item)) { this.Process(item); }
            else { this?.Successor.TryProcess(item); }
        }
    }
}
