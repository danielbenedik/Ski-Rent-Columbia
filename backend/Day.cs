using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{
    public class Day
    {
        List<Order> todayOrdersTaking = new List<Order>();
        List<Order> todayOrdersBack = new List<Order>();

        public List<Order> TodayOrdersTaking { get => todayOrdersTaking; set => todayOrdersTaking = value; }
        internal List<Order> TodayOrdersBack { get => todayOrdersBack; set => todayOrdersBack = value; }
    }
}
