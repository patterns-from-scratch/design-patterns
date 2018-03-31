using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    // Request event argument
    public class RequestEventArgs : EventArgs
    {
        internal CustomerRequest Request { get; set; }
    }

    /// <summary>
    /// Handler
    /// </summary>
    abstract class Selector
    {
        public abstract IOfferInterface Offer { get; }

        // Request event 
        public EventHandler<RequestEventArgs> RequestEvent;

        // Request event handler
        public abstract void RequestHandler(object sender, RequestEventArgs e);

        public Selector()
        {
            RequestEvent += RequestHandler;
        }

        public void ProcessRequest(CustomerRequest request)
        {
            OnRequest(new RequestEventArgs { Request = request });
        }

        // Invoke the Request event
        public virtual void OnRequest(RequestEventArgs e)
        {
            if (RequestEvent != null)
            {
                RequestEvent(this, e);
            }
        }

        // Sets or gets the next approver
        public Selector Successor { get; set; }
    }

    /// <summary>
    /// ConcreteHandler
    /// </summary>
    class MB20Selector : Selector
    {
        public override IOfferInterface Offer => new Offer20Mb();

        public override void RequestHandler(object sender, RequestEventArgs e)
        {
            if (e.Request.DesiredSpeed <= (int)NetworkSpeed.Mb20)
            {
                Console.WriteLine($"Offer speed: {Offer.NetworkSpeed}");
                Console.WriteLine($"Offer price: {Offer.Price}");
            }
            else if (Successor != null)
            {
                Successor.RequestHandler(this, e);
            }
        }
    }

    /// <summary>
    /// ConcreteHandler
    /// </summary>
    class MB40Selector : Selector
    {
        public override IOfferInterface Offer => new Offer40Mb();

        public override void RequestHandler(object sender, RequestEventArgs e)
        {
            if (e.Request.DesiredSpeed <= (int)NetworkSpeed.Mb40)
            {
                Console.WriteLine($"Offer speed: {Offer.NetworkSpeed}");
                Console.WriteLine($"Offer price: {Offer.Price}");
            }
            else if (Successor != null)
            {
                Successor.RequestHandler(this, e);
            }
        }
    }

    /// <summary>
    /// ConcreteHandler
    /// </summary>
    class MB60Selector : Selector
    {
        public override IOfferInterface Offer => new Offer60Mb();

        public override void RequestHandler(object sender, RequestEventArgs e)
        {
            if (e.Request.DesiredSpeed >= (int)NetworkSpeed.Mb60)
            {
                Console.WriteLine($"Offer speed: {Offer.NetworkSpeed}");
                Console.WriteLine($"Offer price: {Offer.Price}");
            }
            else if (Successor != null)
            {
                Successor.RequestHandler(this, e);
            }
        }
    }

    /// <summary>
    /// Keeps request details
    /// </summary>
    class CustomerRequest
    {
        public int DesiredSpeed { get; set; }
    }
}
