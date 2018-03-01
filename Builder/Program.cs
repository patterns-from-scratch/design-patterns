namespace Builder
{
    class Program
    {
        static void Main(string[] args) => UseFluentBuilder();

        public static void UseBuilder()
        {
            var product = new Builder()
                .SetNumber(1)
                .SetName("A")
                .AddSubComponent("X")
                .Build();
        }

        public static void UseFluentBuilder()
        {
            var product = FluentBuilder
                .CreateProduct
                .WithNumber(2)
                .WithName("B")
                .WithComponent("Y")
                .WithComponent("Z")
                .AndNothingMore();
        }
    }
}