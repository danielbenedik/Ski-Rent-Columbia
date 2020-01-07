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
    public partial class NewOrderForm : Form
    {

        Order newOrder;
        string invalidUIString = "One of the values in this suit is invalid";
        public Order NewOrder { get => newOrder; }

        public NewOrderForm()
        {
            InitializeComponent();

            panel_size.Visible = false;
        }

   
        private void add_suits_Click(object sender, EventArgs e)
        {
            try
            {

                if (checkBox_jaket.Checked || checkBox_pants.Checked)
                {
                    ClothingItem jaketItem;
                    ClothingItem pantsItem;
                    //GENDER currentGender = (GENDER)Enum.Parse(typeof(GENDER), comboBox_gender_jaket.Text);
                    Suit newSuit;

                    if (checkBox_jaket.Checked && checkBox_pants.Checked) // to refresh this function
                    {
                        GENDER GenderJaket = (GENDER)Enum.Parse(typeof(GENDER), comboBox_gender_jaket.Text);
                        SIZE jaketSize = (SIZE)Enum.Parse(typeof(SIZE), comboBox_sizes_of_jakets.Text);
                        COMPENY jaketCompany = (COMPENY)Enum.Parse(typeof(COMPENY), comboBox_company_jaket.Text);
                        jaketItem = new ClothingItem(jaketSize, jaketCompany, GenderJaket);

                        GENDER GenderPants = (GENDER)Enum.Parse(typeof(GENDER), comboBox_gender_pants.Text);
                        SIZE pantsSize = (SIZE)Enum.Parse(typeof(SIZE), comboBox_size_of_pants.Text);
                        COMPENY pantsCompany = (COMPENY)Enum.Parse(typeof(COMPENY), comboBox_company_pants.Text);
                        pantsItem = new ClothingItem(pantsSize, pantsCompany, GenderPants);


                        newSuit = new Suit(pantsItem, jaketItem);
                        newOrder.SuitsList.Add(newSuit);
                    }
                    else if(checkBox_jaket.Checked && checkBox_pants.Checked == false)
                    {
                        GENDER GenderJaket = (GENDER)Enum.Parse(typeof(GENDER), comboBox_gender_jaket.Text);
                        SIZE jaketSize = (SIZE)Enum.Parse(typeof(SIZE), comboBox_sizes_of_jakets.Text);
                        COMPENY jaketCompany = (COMPENY)Enum.Parse(typeof(COMPENY), comboBox_company_jaket.Text);
                        jaketItem = new ClothingItem(jaketSize, jaketCompany, GenderJaket);

                        newSuit = new Suit(null, jaketItem);
                        newOrder.SuitsList.Add(newSuit);
                    }
                    else if (checkBox_pants.Checked && checkBox_jaket.Checked == false)
                    {
                        GENDER GenderPants = (GENDER)Enum.Parse(typeof(GENDER), comboBox_gender_pants.Text);
                        SIZE pantsSize = (SIZE)Enum.Parse(typeof(SIZE), comboBox_size_of_pants.Text);
                        COMPENY pantsCompany = (COMPENY)Enum.Parse(typeof(COMPENY), comboBox_company_pants.Text);
                        pantsItem = new ClothingItem(pantsSize, pantsCompany, GenderPants);

                        newSuit = new Suit(pantsItem, null);
                        newOrder.SuitsList.Add(newSuit);
                    }

                    number_of_suits.Text = newOrder.SuitsList.Count.ToString();

                    ClearItemsSelect();
                }


            }
            catch
            {
                MessageBox.Show(invalidUIString);
            }
        }

        private void ClearItemsSelect()
        {
            comboBox_size_of_pants.ResetText();
            comboBox_sizes_of_jakets.ResetText();

            comboBox_gender_jaket.ResetText();
            comboBox_gender_pants.ResetText();

            comboBox_company_pants.ResetText();
            comboBox_company_jaket.ResetText();

        }


        private void button_create_order_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime flightDay = DateTime.Parse(dateTimePicker1.Text);
                DateTime ReturnDay = DateTime.Parse(dateTimePicker_return_date.Text);
                if (flightDay.CompareTo(DateTime.Today) > 0 && ReturnDay.CompareTo(DateTime.Today) > 0)
                {
                    newOrder = new Order(name_TextBox.Text, int.Parse(ID_TextBox.Text), int.Parse(code_TextBox.Text), flightDay, ReturnDay);
                    panel_size.Visible = true;
                    button_close_and_save.Visible = true;

                    button_create_order.Visible = false;

                    comboBox_size_of_pants.Visible = false;
                    comboBox_company_pants.Visible = false;
                    comboBox_gender_pants.Visible = false;

                    comboBox_sizes_of_jakets.Visible = false;
                    comboBox_company_jaket.Visible = false;
                    comboBox_gender_jaket.Visible = false;
                }
                else
                {
                    throw new System.InvalidOperationException();
                }
            }
            catch
            {
                MessageBox.Show(invalidUIString);
            }
        }

        private void checkBox_jaket_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_jaket.Checked)
            {
                comboBox_sizes_of_jakets.Visible = true;
                comboBox_company_jaket.Visible = true;
                comboBox_gender_jaket.Visible = true;

            }
            else
            {
                comboBox_sizes_of_jakets.Visible = false;
                comboBox_company_jaket.Visible = false;
                comboBox_gender_jaket.Visible = false;
            }
        }

        private void checkBox_pants_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_pants.Checked)
            {
                comboBox_size_of_pants.Visible = true;
                comboBox_company_pants.Visible = true;
                comboBox_gender_pants.Visible = true;
            }
            else
            {
                comboBox_size_of_pants.Visible = false;
                comboBox_company_pants.Visible = false;
                comboBox_gender_pants.Visible = false;
            }
        }

        private void button_close_and_save_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
