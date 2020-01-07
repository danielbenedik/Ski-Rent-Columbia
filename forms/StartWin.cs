using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RentSkiColumbia;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;


namespace formSkiRentColumbia
{
    public partial class StartWin : Form
    {
        TotalRentData bigData;
        MyExel dataFrom;
        DialogResult isUserWantSaveAgain;
        string checkeStringForUI = "1- The Excel file is not open\n2- The file is in its place\n3- the parameters in the file is correct";

        public DialogResult IsUserWantSaveAgain { get => isUserWantSaveAgain; set => isUserWantSaveAgain = value; }

        public StartWin()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Save_data_to_excel_event);

            bigData = new TotalRentData();

            dataFrom = new MyExel(@"C:\Daniel\data ski rent columbia.xlsx");

            Set_data_from_exel();

            Refresh_list_show();
            make_problem_storage();
        }

        //// save data to excel  ////
        public void Save_data_to_excel_event(object sender, EventArgs e)
        {
            Save_data_to_excel();
        }
        public void Save_data_to_excel()
        {
            try
            {
                dataFrom.OpenFile();

                int row = 4;

                for (int i=0; i< bigData.MonthArray.Length; i++)
                {
                    Pass_month_to_save_data_excel(bigData.MonthArray[i], ref row);
                }
                dataFrom.CloseFile(true);
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                try
                {
                    dataFrom.CloseFile();
                }
                catch
                {

                }
                finally
                {
                    this.DialogResult = DialogResult.Cancel;
                    isUserWantSaveAgain = MessageBox.Show("There was a failure to save the information.\nplease check:\n" + checkeStringForUI + "Do you want to try saving again?", "warning", MessageBoxButtons.YesNo);
                }
            }
        }
        private void Pass_month_to_save_data_excel(Month monthToPass,ref int row)
        {
            for(int i =1; i<= monthToPass.EndDay; i++)
            {
                Save_list_orders_to_excel(monthToPass.Days[i].TodayOrdersTaking, ref row);
            }
        }
        private void Save_list_orders_to_excel(List<Order> listOrderToSave, ref int row)
        {
            foreach(Order order in listOrderToSave)
            {
                Save_order_to_excel(order, ref row);
                row++;
            }
        }
        private void Save_order_to_excel(Order orderToSave, ref int row)
        {
            int col=1;
            dataFrom.WorkSheet_orders.Cells[row, col].Value2 = orderToSave.CodeOrder.ToString();
            col++;

            dataFrom.WorkSheet_orders.Cells[row, col].value2 = orderToSave.StatusOfOrder.ToString();
            col++;

            dataFrom.WorkSheet_orders.Cells[row, col].value2 = orderToSave.FlightDate.ToShortDateString();
            col++;

            dataFrom.WorkSheet_orders.Cells[row, col].value2 = orderToSave.ReturnDate.ToShortDateString();
            col++;

            dataFrom.WorkSheet_orders.Cells[row, col].value2 = orderToSave.ID.ToString();
            col++;

            dataFrom.WorkSheet_orders.Cells[row, col].value2 = orderToSave.Name.ToString();
            col++;

            dataFrom.WorkSheet_orders.Cells[row, col].value2 = orderToSave.SuitsList.Count.ToString();
            col++;

            Save_list_suits_to_excel(orderToSave.SuitsList, ref row, ref col);
        }
        private void Save_list_suits_to_excel(List<Suit> suits, ref int row, ref int col)
        {

            foreach (Suit suit in suits)
            {
                Save_suits_to_excel(suit, ref row, ref col);
            }
        }
        private void Save_suits_to_excel(Suit suitToSave, ref int row, ref int col)
        {
           

            Save_jaket_or_pants_to_excel(suitToSave.Jaket, ref row, ref col);
            Save_jaket_or_pants_to_excel(suitToSave.Pants, ref row, ref col);
        }
        private void Save_jaket_or_pants_to_excel(ClothingItem item, ref int row, ref int col)
        {
            if(item != null)
            {
                dataFrom.WorkSheet_orders.Cells[row, col].value2 = item.Gender.ToString();
                col++;
                dataFrom.WorkSheet_orders.Cells[row, col].value2 = item.Size.ToString();
                col++;
                dataFrom.WorkSheet_orders.Cells[row, col].value2 = item.TypeOfCompany.ToString();
                col++;
            }
            else
            {
                col += 3;
            }
        }

        ////  load data from excel  //// 
        private void Set_data_from_exel()
        {
            try
            {
                Set_store_storage_data();
                Set_store_orders_data();

                MessageBox.Show("Successful loading data-storage");
            }
            catch
            {
                try
                {
                    dataFrom.CloseFile();

                }
                catch
                {

                }
                finally
                {
                    MessageBox.Show("loading did not work\n" + checkeStringForUI);
                    this.DialogResult = DialogResult.Cancel;
                }
            }

        }
        private void Set_store_orders_data()
        {
            dataFrom.OpenFile();

            int i = 4; // row to start
            int j = 1;
            object isEmpty = dataFrom.WorkSheet_orders.Cells[i,j].value2;
            while (isEmpty != null)
            {
                double code = double.Parse(dataFrom.WorkSheet_orders.Cells[i, j].value2.ToString());
                j++;
                STATUS_ORDER statusOrder = (STATUS_ORDER)Enum.Parse(typeof(STATUS_ORDER), dataFrom.WorkSheet_orders.Cells[i, j].value2);
                j++;

                DateTime flyDate = DateTime.Parse(dataFrom.WorkSheet_orders.Cells[i, j].value2);

                j++;
                DateTime flyReturn = DateTime.Parse(dataFrom.WorkSheet_orders.Cells[i, j].value2);
                j++;

                double ID = double.Parse(dataFrom.WorkSheet_orders.Cells[i, j].value2.ToString());

                j++;
                string name = dataFrom.WorkSheet_orders.Cells[i, j].value2;
                j++;
                double numOfSuit = double.Parse(dataFrom.WorkSheet_orders.Cells[i, j].value2.ToString());
                j++;

                Order newOrder = new Order(name, ID, code, flyDate, flyReturn, statusOrder);
                newOrder.SuitsList = Make_list_suits_from_data_excel(numOfSuit,i, dataFrom, ref j);

                bigData.AddOrder(newOrder);

                // check next row
                i++;
                j = 1;
                isEmpty = dataFrom.WorkSheet_orders.Cells[i, j].value2;
            }

            dataFrom.CloseFile();
        }
        private void Set_store_storage_data()
        {
            dataFrom.OpenFile();

            ClothesStorage dataTo = new ClothesStorage();

            for (int i = 0; i < 7; i++)
            {
                dataTo.Men.Jaket[i] = (int)dataFrom.WorkSheet_storage.Cells[2, i + 3].value2;
                dataTo.Men.Pants[i] = (int)dataFrom.WorkSheet_storage.Cells[3, i + 3].value2;

                dataTo.Women.Jaket[i] = (int)dataFrom.WorkSheet_storage.Cells[4, i + 3].value2;
                dataTo.Women.Pants[i] = (int)dataFrom.WorkSheet_storage.Cells[5, i + 3].value2;

                dataTo.YoungBoy.Jaket[i] = (int)dataFrom.WorkSheet_storage.Cells[6, i + 3].value2;
                dataTo.YoungBoy.Pants[i] = (int)dataFrom.WorkSheet_storage.Cells[7, i + 3].value2;

                dataTo.YoungGirl.Jaket[i] = (int)dataFrom.WorkSheet_storage.Cells[8, i + 3].value2;
                dataTo.YoungGirl.Pants[i] = (int)dataFrom.WorkSheet_storage.Cells[9, i + 3].value2;
            }

            bigData.CompanyStorage = dataTo;
            dataFrom.CloseFile();
        }
        private Suit Make_suit_from_data_excel(int row, ref int j,  MyExel dataFrom)
        {
            ClothingItem jaket = null; 
            ClothingItem pans = null;

            

            object isNull = dataFrom.WorkSheet_orders.Cells[row, j].value2;
            if(isNull !=null)
            {
                GENDER genderJaket = (GENDER)Enum.Parse(typeof(GENDER), dataFrom.WorkSheet_orders.Cells[row,j].value2);
                j++;
                SIZE jaketSize = (SIZE)Enum.Parse(typeof(SIZE), dataFrom.WorkSheet_orders.Cells[row, j].value2);
                j++;
                COMPENY compenyJaket = (COMPENY)Enum.Parse(typeof(COMPENY), dataFrom.WorkSheet_orders.Cells[row, j].value2);
                jaket = new ClothingItem(jaketSize, compenyJaket, genderJaket);
                j++;
            }
            else
            {
                j += 3;
            }

            isNull = dataFrom.WorkSheet_orders.Cells[row, j].value2;
            if (isNull != null)
            {
                GENDER genderPants = (GENDER)Enum.Parse(typeof(GENDER), dataFrom.WorkSheet_orders.Cells[row, j].value2);
                j++;
                SIZE pantsSize = (SIZE)Enum.Parse(typeof(SIZE), dataFrom.WorkSheet_orders.Cells[row, j].value2);
                j++;
                COMPENY compenypants = (COMPENY)Enum.Parse(typeof(COMPENY), dataFrom.WorkSheet_orders.Cells[row, j].value2);
                pans = new ClothingItem(pantsSize, compenypants, genderPants);
                j++;
            }
            else
            {
                j += 3;
            }

            Suit newSuit = new Suit(pans, jaket);
            return newSuit;
        }
        private List<Suit> Make_list_suits_from_data_excel(double numOfSuit, int row, MyExel dataFrom, ref int col)
        {
            List<Suit> retSuits = new List<Suit>();

            for(double k=0; k< numOfSuit; k++)
            {
                
                retSuits.Add(Make_suit_from_data_excel(row, ref col, dataFrom));
            }
            return retSuits;
        }



        private void add_order_button_Click(object sender, EventArgs e)
        {
            NewOrderForm takeDataOrder = new NewOrderForm();
            takeDataOrder.ShowDialog();

            if (takeDataOrder.DialogResult == DialogResult.OK)
            {
                bigData.AddOrder(takeDataOrder.NewOrder);
            }
        }
        private void button_check_suit_amount_Click(object sender, EventArgs e)
        {
            NewOrderForm takeDataOrder = new NewOrderForm();
            takeDataOrder.ShowDialog();

            if (takeDataOrder.DialogResult == DialogResult.OK)
            {
                richTextBox_problem.Clear();
                OrderView.Set_font(string.Empty, bigData.Check_if_problem_amount_to_order(takeDataOrder.NewOrder, true), richTextBox_problem);
            }
        }
        private void button_search_Click(object sender, EventArgs e)
        {
            try
            {
                int code_order = int.Parse(richTextBox_search_code.Text);
                Order retOrder = bigData.Search_order_by_code(code_order);

                if(retOrder != null)
                {
                    OrderView viewOrder = new OrderView(retOrder);
                    viewOrder.ShowDialog();
                    if(viewOrder.DialogResult == DialogResult.OK )// if something change
                    {
                        Refresh_list_show();
                    }
                }
                else
                {
                    MessageBox.Show("there is no order with this code");
                }
                richTextBox_search_code.Clear();
            }
            catch
            {
                MessageBox.Show("invalid code");
                richTextBox_search_code.Clear();
            }
        }
        private void Set_store_storage_button_Click(object sender, EventArgs e)
        {
            Set_store_storage_data();
            make_problem_storage();
        }
        private void button_specific_day_Click(object sender, EventArgs e)
        {
            Make_list_orders_by_date(DateTime.Parse(dateTimePicker_specific_day.Text), orders_by_day_textBox);
        }
        private void button_make_problem_Click(object sender, EventArgs e)
        {
            make_problem_storage();
        }
        private void make_problem_storage()
        {
            richTextBox_problem.Clear();
            OrderView.Set_font(string.Empty, bigData.Make_problem_storage(), richTextBox_problem);

        }
        

        private void Refresh_list_show()
        {
            Make_list_orders_by_date(DateTime.Today, how_will_rerutn_today_textBox, true);
            Make_list_ready_orders();

        }
        private void Make_list_ready_orders()
        {
            List<Order> readyList = bigData.Make_list_ready_orders();

            Print_list_data(readyList, Ready_orders_textBox);
        }
        private void Make_list_orders_by_date(DateTime date, RichTextBox toUpdate, bool isForOrderReturnTodayList=false)
        {
            List<Order> returnList;
            if (isForOrderReturnTodayList)
            {
                returnList = bigData.List_by_date_return_order(date);
            }
            else
            {
                returnList = bigData.List_by_date_taking_order(date);
            }

            Print_list_data(returnList, toUpdate);
        }


        ////  prepare data to UI  ////
        private void Print_list_data(List<Order> listToPrint, RichTextBox toUpdate)
        {
            toUpdate.Clear();
            if (listToPrint.Count == 0)
            {
                OrderView.Set_font("note: ", "there is no orders to show", toUpdate);
            }
            else
            {
                foreach (Order order in listToPrint)
                {
                    Organize_data_ui_order(order, toUpdate);
                    OrderView.Set_font("--------------------------------------",null, toUpdate);
                }
            }
        }
        internal void Organize_data_ui_order(Order order, RichTextBox toUpdate)
        {
            OrderView.Set_font("Name: ", order.Name, toUpdate);
            OrderView.Set_font("Status: ", order.StatusOfOrder.ToString(), toUpdate);
            OrderView.Set_font("Code number: ", order.CodeOrder.ToString(), toUpdate);
            OrderView.Set_font("flight date: ", order.FlightDate.ToShortDateString(), toUpdate);
        }

    }
}
