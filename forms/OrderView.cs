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

namespace formSkiRentColumbia
{
    public partial class OrderView : Form
    {
        Order currentOrder;

        public OrderView(Order _currentOrder)
        {
            InitializeComponent();
            currentOrder = _currentOrder;
            Organize_data();
        }



        private void Organize_data()
        {
            name_TextBox.Text = currentOrder.Name;
            ID_TextBox.Text = currentOrder.ID.ToString();
            code_TextBox.Text = currentOrder.CodeOrder.ToString();
            richTextBox_flight_date.Text = currentOrder.FlightDate.ToShortDateString();
            richTextBox_return_date.Text = currentOrder.ReturnDate.ToShortDateString();
            label_status_order.Text = currentOrder.StatusOfOrder.ToString();
            comboBox_status_order.SelectedIndex = (int)currentOrder.StatusOfOrder;
            Organize_data_suit_to_RichTextBox(currentOrder, richTextBox_suits_order);
        }

        internal static void Organize_data_suit_to_RichTextBox(Order order, RichTextBox toUpdate)
        {
            foreach (Suit temp in order.SuitsList)
            {
                OrderView.Update_suit_to_textBox(temp, toUpdate);
                toUpdate.SelectedText += Environment.NewLine;
            }

        }

        internal static void Update_suit_to_textBox(Suit currentSuit, RichTextBox toUpdate)
        {

            if (currentSuit.Jaket != null)
            {
                Set_font("Gender Jaket: ", currentSuit.Jaket.Gender.ToString(), toUpdate);
                Set_font("Jaket: ", currentSuit.Jaket.Size.ToString(), toUpdate);
                Set_font("", currentSuit.Jaket.TypeOfCompany.ToString(), toUpdate);
            }
            if (currentSuit.Pants != null)
            {
                Set_font("Gender Pants: ", currentSuit.Pants.Gender.ToString(), toUpdate);
                Set_font("Pants: ", currentSuit.Pants.Size.ToString(), toUpdate);
                Set_font("", currentSuit.Pants.TypeOfCompany.ToString(), toUpdate);
            }
        }

        internal static void Set_font(string title, string sentence, RichTextBox toSet)
        {
            toSet.SelectionFont = new Font("times new roman", 13, FontStyle.Regular);
            toSet.SelectedText = title;

            toSet.SelectionFont = new Font("times new roman", 12, FontStyle.Regular);
            toSet.SelectedText = sentence + Environment.NewLine;
        }

        private void button_save_change_status_order_Click(object sender, EventArgs e)
        {
            currentOrder.StatusOfOrder = (STATUS_ORDER)Enum.Parse(typeof(STATUS_ORDER), comboBox_status_order.Text);
            label_status_order.Text = currentOrder.StatusOfOrder.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void button_delete_order_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
