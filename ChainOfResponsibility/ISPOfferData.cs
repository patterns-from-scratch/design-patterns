using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    public interface IOfferInterface
    {
        NetworkSpeed NetworkSpeed { get; }
        SubcriptionPrice Price { get; }
    }

    public class Offer20Mb : IOfferInterface
    {
        public NetworkSpeed NetworkSpeed { get; } = NetworkSpeed.Mb20;
        public SubcriptionPrice Price { get; } = SubcriptionPrice.PLN29;
    }
    public class Offer40Mb : IOfferInterface
    {
        public NetworkSpeed NetworkSpeed { get; } = NetworkSpeed.Mb40;
        public SubcriptionPrice Price { get; } = SubcriptionPrice.PLN39;
    }
    public class Offer60Mb : IOfferInterface
    {
        public NetworkSpeed NetworkSpeed { get; } = NetworkSpeed.Mb60;
        public SubcriptionPrice Price { get; } = SubcriptionPrice.PLN49;
    }

    public enum NetworkSpeed
    {
        Mb20 = 20,
        Mb40 = 40,
        Mb60 = 60
    }

    public enum SubcriptionPrice
    {
        PLN29 = 29,
        PLN39 = 39,
        PLN49 = 49
    }
}
