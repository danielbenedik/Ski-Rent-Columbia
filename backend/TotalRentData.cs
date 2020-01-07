using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{
    public class TotalRentData
    {
        Month[] monthArray= new Month[6];
        List<Order> totalOrder = new List<Order>();
        ClothesStorage companyStorage;

        public List<Order> TotalOrder { get => totalOrder; }
        public ClothesStorage CompanyStorage { get => companyStorage; set => companyStorage = value; }
        public Month[] MonthArray { get => monthArray; set => monthArray = value; }

        public TotalRentData()
        {
            InitializationMonth();
        }

        private void InitializationMonth()
        {
          for (int i=0; i< monthArray.Length; i++)
            {
                monthArray[i] = new Month();
            }
            monthArray[0].EndDay = 31; // december
            monthArray[1].EndDay = 31;
            monthArray[2].EndDay = 29;
            monthArray[3].EndDay = 31;
            monthArray[4].EndDay = 30;
            monthArray[5].EndDay = 31;
        }

        public void AddOrder(Order newOrder)
        {
            int month = newOrder.FlightDate.Month;
            int day = newOrder.FlightDate.Day;

            Add_to_order_list(day, month, false, newOrder);

            day = newOrder.ReturnDate.Day;
            month = newOrder.ReturnDate.Month;

            Add_to_order_list(day, month, true, newOrder);

            totalOrder.Add(newOrder);
        }

        private void Add_to_order_list(int day, int month, bool isReturentDay, Order newOrder)
        {
            if (month == 12)
            {
                if(isReturentDay)
                {
                    monthArray[0].Days[day].TodayOrdersBack.Add(newOrder);
                }
                else
                {
                    monthArray[0].Days[day].TodayOrdersTaking.Add(newOrder);

                }
            }
            else
            {
                if (isReturentDay)
                {
                    monthArray[month].Days[day].TodayOrdersBack.Add(newOrder);
                }
                else
                {
                    monthArray[month].Days[day].TodayOrdersTaking.Add(newOrder);

                }
            }
        }

        public Order Search_order_by_code(int code)
        {
            Order needOrderToFInd = totalOrder.Find(x => x.CodeOrder == code);

            return needOrderToFInd;
        }

        public List<Order> List_by_date_taking_order(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;

            if (month == 12)
            {
                return monthArray[0].Days[day].TodayOrdersTaking;
            }
            else
            {
                return monthArray[month].Days[day].TodayOrdersTaking;
            }
        }

        public List<Order> List_by_date_return_order(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;

            if (month == 12)
            {
                return monthArray[0].Days[day].TodayOrdersBack;
            }
            else
            {
                return monthArray[month].Days[day].TodayOrdersBack;
            }
        }

        public string Make_problem_storage()
        {
            string res = string.Empty;
            foreach (Order checkOrder in totalOrder)
            {
                if(checkOrder.FlightDate.CompareTo(DateTime.Today) >= 0)
                {
                    res += Check_if_problem_amount_to_order(checkOrder);
                }
            }
            return res;
        }

        public string Check_if_problem_amount_to_order(Order orderToCheck, bool isOrderFromOutOfList = false)
        {
            string res = string.Empty;


            if(orderToCheck.SuitsList != null)
            {
                ClothesStorage countClothingIntem = new ClothesStorage();

                foreach (Order tempOrder in totalOrder)
                {
                    if ((orderToCheck.FlightDate.CompareTo(tempOrder.FlightDate) >= 0 && orderToCheck.FlightDate.CompareTo(tempOrder.ReturnDate) <= 0))
                    {
                        countClothingIntem.Make_total_count_Item(tempOrder);
                    }
                }

                if(isOrderFromOutOfList == true)
                {
                    countClothingIntem.Make_total_count_Item(orderToCheck);
                }

                string restCurrent = countClothingIntem.Make_balance_Item(companyStorage, orderToCheck);

                if (restCurrent != string.Empty)
                {
                    res += restCurrent;
                    res += "Flight date: " + orderToCheck.FlightDate.ToShortDateString() + Environment.NewLine;
                    res += "Order name: " + orderToCheck.Name + Environment.NewLine;
                    res += "Code: " + orderToCheck.CodeOrder.ToString() + Environment.NewLine;
                    res += "--------------------------------------" + Environment.NewLine;
                }

            }

            return res;
        }

        public List<Order> Make_list_ready_orders()
        {
            List<Order> returnList = new List<Order>();

            foreach (Order order in TotalOrder)
            {
                if (order.StatusOfOrder == STATUS_ORDER.Ready)
                {
                    returnList.Add(order);
                }
            }
            return returnList;
        }


    }
}
