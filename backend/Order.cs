using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{

    public enum STATUS_ORDER {NotReady, Ready, HasTaken, Returned};
    public class Order
    {
        string name;
        double Id;
        double codeOrder;
        STATUS_ORDER statusOfOrder;
        DateTime flightDate;
        DateTime returnDate;


        List<Suit> suitsList = new List<Suit>();

        public Order(string _name, double _ID, double _codeOrder , DateTime _flightDate, DateTime _returnDate, STATUS_ORDER status = STATUS_ORDER.NotReady)
        {
            name = _name;
            Id = _ID;
            codeOrder = _codeOrder;
            flightDate = _flightDate;
            returnDate = _returnDate;
            statusOfOrder = status;
        }

        public List<Suit> SuitsList { get => suitsList; set => suitsList = value; }
        public string Name { get => name; set => name = value; }
        public double ID { get => Id; set => Id = value; }
        public double CodeOrder { get => codeOrder; set => codeOrder = value; }
        public DateTime FlightDate { get => flightDate; set => flightDate = value; }
        public STATUS_ORDER StatusOfOrder { get => statusOfOrder; set => statusOfOrder = value; }
        public DateTime ReturnDate { get => returnDate; set => returnDate = value; }
    }
}
