using System.Collections.Generic;

namespace Builder
{
    class Program
    {
        static void Main(string[] args) => UseFluentBuilder();

        public static Product UseBuilder() 
            => new Builder()
                .SetNumber(1)
                .SetName("A")
                .AddSubComponent("X")
                .Build();

        public static FluentProduct UseFluentBuilder() 
            => FluentBuilder
                .CreateProduct
                .WithNumber(2)
                .WithName("B")
                .WithComponent("Y")
                .WithComponent("Z")
                .AndNothingMore();

        public static Product MyProperty { get; set; }

        public static Product P { get; set; }

        public static string AS() => S(MyProperty, P);

        public static string KK { get => AS(); }

        public static int H(FluentProduct fluent, int i) => fluent.GetHashCode() + i;

        public static string S(Product p, Product s) => $"{p.ToString()},{s.ToString(), {KK}}";

        public static FluentProduct FluentProduct(string component) => FluentBuilder.CreateProduct.WithNumber(1).WithName("").WithComponent(component).AndNothingMore();
    }
}