using System.Collections.Generic;

namespace Builder
{
    interface IFluentProduct
    {
        int Number { get; }
        string Name { get; }
        List<string> Components { get; }
    }
    interface INumberHolder { INameHolder WithNumber(int number); }
    interface INameHolder { IComponentsHolder WithName(string name); }
    interface IComponentsHolder
    {
        IComponentsHolder WithComponent(string subComponent);
        FluentProduct AndNothingMore();
    }

    class FluentBuilder : IFluentProduct, INumberHolder, INameHolder, IComponentsHolder
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public List<string> Components { get; }

        private FluentBuilder() => this.Components = new List<string>();

        public static INumberHolder CreateProduct { get => new FluentBuilder(); }
        public INameHolder WithNumber(int number) { this.Number = number; return this; }
        public IComponentsHolder WithName(string name) { this.Name = name; return this; }
        public IComponentsHolder WithComponent(string component)
        {
            this.Components.Add(component);
            return this;
        }
        public FluentProduct AndNothingMore() => new FluentProduct(this);
    }

    class FluentProduct : IFluentProduct
    {
        public int Number { get; }
        public string Name { get; }
        public List<string> Components { get; }

        private FluentProduct() { }

        public FluentProduct(IFluentProduct builder)
        {
            this.Number = builder.Number;
            this.Name = builder.Name;
            this.Components = builder.Components;
        }
    }
}