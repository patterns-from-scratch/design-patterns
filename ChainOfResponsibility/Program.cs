using ChainOfResponsibility.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            new int[0].Select(x =>
            {
                return
                    new ChainLink<int, IObject>(e => false, e => new CustomerRequest(),
                    new ChainLink<int, IObject>(Singleton.Instance, e => new AlternativeRequest(e),
                    new ChainLink<int, IObject>(e => false, e => new VoidRequest()
                    ))).Transform(int.MinValue, new VoidRequest());
            })
            .OfType<IObject>();


            new ChainLink<int>(e => false, e => { },
            new ChainLink<int>(new Specification(), e => e.ToString()
            )).Process(int.MinValue, e => { });

            var @object =
                new ChainLink<int, IObject>(e => false, e => new CustomerRequest(),
                new ChainLink<int, IObject>(Singleton.Instance, e => new AlternativeRequest(e),
                new ChainLink<int, IObject>(e => false, e => new VoidRequest())
                )).Transform(int.MinValue, new VoidRequest());


            // Customers requests
            var request1 = new CustomerRequest() { DesiredSpeed = 10 };
            var request2 = new CustomerRequest() { DesiredSpeed = 30 };
            var request3 = new CustomerRequest() { DesiredSpeed = 70 };
            var requests = new CustomerRequest[] { request1, request2, request3 };

            Selector baseSelector = new MB20Selector();
            Selector mb40 = new MB40Selector();
            Selector mb60 = new MB60Selector();

            baseSelector.Successor = mb40;
            mb40.Successor = mb60;

            foreach (var request in requests)
            {
                baseSelector.ProcessRequest(request);
                Console.WriteLine("******************************");
            }
            Console.ReadKey();
        }
    }
}
