using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConvertApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataTemplateSelector();
        }

        private void DataTemplateSelector()//Create A class
        {
            DataTable dt=new DataTable();//Datable Datastructure that creates Rows And Columns to input data

            dt.Columns.Add("Text");//Definde Columns
            dt.Columns.Add("Value");

            dt.Rows.Add("--SELECT--", 0);//Text,Value Rows
            dt.Rows.Add("USD", 1);
            dt.Rows.Add("INR", 82);
            dt.Rows.Add("EUR", 0.93);
            dt.Rows.Add("GBP", 0.77);
            dt.Rows.Add("Lek", 0.93897);
            dt.Rows.Add("AUD", 1.49);
            dt.Rows.Add("CAD", 1.36);


            cmbFromCurrency.ItemsSource = dt.DefaultView;//Matching the object
            cmbFromCurrency.DisplayMemberPath = "Text";//Method that Displays the Text 
            cmbFromCurrency.SelectedValuePath = "Value";//Method that selects the Value of selected Item from SOurce
            cmbFromCurrency.SelectedIndex = 0;//Deafult Selected Index

            cmbToCurrency.ItemsSource = dt.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;

        }


        private void Convert_Click(object sender, RoutedEventArgs e)//Method When Click Button is Triggered
        {
            double Convereted_Value;//Double
            if (txtCurrency.Text == null || txtCurrency.Text.Trim()=="")
            {
                MessageBox.Show("Please Enter Currency Value");
                txtCurrency.Focus();
                    return;
            }

            if(!double.TryParse(txtCurrency.Text, out Convereted_Value))
{
                MessageBox.Show("Please enter a valid number.");
            }

            if (cmbFromCurrency.SelectedValue==null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency!!!");
                cmbFromCurrency.Focus();
                return;
            }

            if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency!!!");
                cmbFromCurrency.Focus();
                return;
            }

            if (cmbFromCurrency.Text == cmbToCurrency.Text) {

                Convereted_Value = double.Parse(txtCurrency.Text);
                lblCurrency.Content = cmbToCurrency.Text + " " + Convereted_Value.ToString("N3");
            }
            else
            {
                Convereted_Value = double.Parse(txtCurrency.Text) * double.Parse(cmbFromCurrency.SelectedValue.ToString()) / double.Parse(cmbToCurrency.SelectedValue.ToString());//Formula wich multiplies the text with value times the value of pair 2
                lblCurrency.Content = cmbToCurrency.Text + " " + Convereted_Value.ToString("N3");
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)//Regex to match the money input
        {
            Regex regex = new Regex("^\\$?\\d{1,3}(?:,\\d{3})*(?:\\.\\d{2})?$\r\n");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void Clear_Click(object sender,RoutedEventArgs e)
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
            {
                cmbFromCurrency.SelectedIndex = 0;
            }
            if (cmbToCurrency.Items.Count > 0)
            {
                cmbToCurrency.SelectedIndex = 0;
            }
            lblCurrency.Content = "";
            txtCurrency.Focus();
        }


    }
}