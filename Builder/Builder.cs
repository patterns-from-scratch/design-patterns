using System.Collections.Generic;

namespace Builder
{
    class Product
    {
        public int Number { get; internal set; }
        public string Name { get; internal set; }
        public List<string> SubComponents { get; internal set; }
    }

    class Builder
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public List<string> SubComponents { get; }

        public Builder() { this.SubComponents = new List<string>(); }

        public Builder SetNumber(int number) { this.Number = number; return this; }
        public Builder SetName(string name) { this.Name = name; return this; }
        public Builder AddSubComponent(string subComponent)
        {
            this.SubComponents.Add(subComponent);
            return this;
        }
        public Product Build() => new Product()
        {
            Number = this.Number,
            Name = this.Name,
            SubComponents = this.SubComponents
        };
    }
}