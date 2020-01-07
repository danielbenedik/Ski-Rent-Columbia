using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{
    public class Month
    {
        int endDay;
        Day[] days = new Day[40];

        public Month()
        {
            for(int i=0; i< days.Length; i++)
            {
                days[i] = new Day();
            }


        }



        public int EndDay { get => endDay; set => endDay = value; }
        public Day[] Days { get => days; set => days = value; }
    }
}
