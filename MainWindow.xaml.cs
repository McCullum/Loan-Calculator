using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Assignment3_8694870
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //For Placeholder Text Color
            txtLoanAmount.Foreground = System.Windows.Media.Brushes.DarkGray;
            txtInterestRate.Foreground = System.Windows.Media.Brushes.DarkGray;
            txtNoMonth.Foreground = System.Windows.Media.Brushes.DarkGray;
        }

        //Returns Monthly Payment
        private double CalculationOfInterest(double Amount, double RateOfInterest, double Month) { 
            return Math.Round(Amount * RateOfInterest * (Math.Pow((1 + RateOfInterest), Month)) / (Math.Pow((1 + RateOfInterest), Month) - 1),2);
        }
        
        //btnCalculate Click Event
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            float Amount = 0, RateOfInterest = 0, Month = 0;

            int Flag = 0;
            if (txtLoanAmount.Text == "Loan Amount" || txtLoanAmount.Text == "") {
                Flag = 1;
            }
            if (txtInterestRate.Text == "Inter. Rate Max(100)" || txtInterestRate.Text == "") {
                Flag = 1;
            }
            if (txtNoMonth.Text == "" || txtNoMonth.Text == "Months Max(360)") {
                Flag = 1;
            }
            if (Flag == 1)
            {
                MessageBox.Show("Please Enter All Details.","Fill Details",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                Flag = 0;
                if ((!float.TryParse(txtLoanAmount.Text.Trim(), out Amount)) || Amount <= 0)
                {
                    lblErrorLoanAmount.Visibility = Visibility.Visible;
                    Flag = 1;
                }
                else {
                    lblErrorLoanAmount.Visibility = Visibility.Hidden;
                }
                if ((!float.TryParse(txtInterestRate.Text.Trim(), out RateOfInterest)) || RateOfInterest <= 0 || RateOfInterest >100    )
                {
                    lblErrorInterestRate.Visibility = Visibility.Visible;
                    Flag = 1;
                }
                else { 
                    lblErrorInterestRate.Visibility = Visibility.Hidden;
                }
                if (CalculateInterest.SelectedIndex == 1)
                {
                    if ((!float.TryParse(txtNoMonth.Text.Trim(), out Month)) || Month <= 0 || Month > 30)
                    {
                        lblErrorNoMonths.Visibility = Visibility.Visible;
                        Flag = 1;
                    }
                    else
                    {
                        lblErrorNoMonths.Visibility = Visibility.Hidden;
                    }
                }
                else {
                    if ((!float.TryParse(txtNoMonth.Text.Trim(), out Month)) || Month <= 0 || Month > 360)
                    {
                        lblErrorNoMonths.Visibility = Visibility.Visible;
                        Flag = 1;
                    }
                    else
                    {
                        lblErrorNoMonths.Visibility = Visibility.Hidden;
                    }
                }
                
                if(Flag==0)
                {
                    string Calculation = CalculateInterest.Text;


                    if (Calculation == "Calculate Monthly Payment")
                    {
                        float TempRateOfInterest = RateOfInterest;
                        TempRateOfInterest = RateOfInterest / 1200;
                        double MonthlyPayment = CalculationOfInterest(Amount, TempRateOfInterest, Month);
                        lblResult.Content = "Your Monthly Payment Is $" + Math.Round(MonthlyPayment,2).ToString();
                    }
                    else if (Calculation == "Calculate Annual Payment")
                    {
                        float TempRateOfInterest = RateOfInterest;
                        TempRateOfInterest = RateOfInterest / 100;
                        double AnnualPayment = CalculationOfInterest(Amount, TempRateOfInterest, Month);
                        
                        lblResult.Content = "Your Annual Payment Is $" + AnnualPayment.ToString();
                    }
                    else if (Calculation == "Calculate Complete Payment")
                    {
                        float TempRateOfInterest = RateOfInterest;
                        TempRateOfInterest = RateOfInterest / 1200;
                        double MonthlyPayment = CalculationOfInterest(Amount, TempRateOfInterest, Month);
                        double CompletePayment = Math.Round(MonthlyPayment * Month , 2);
                        lblResult.Content = "Your Complete Payment Is $" + CompletePayment.ToString();
                    }
                    lblResult.HorizontalContentAlignment = HorizontalAlignment.Center;
                }
            }
        }

        //btnClear Click Event
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

            txtLoanAmount.Foreground = System.Windows.Media.Brushes.DarkGray;
            txtInterestRate.Foreground = System.Windows.Media.Brushes.DarkGray;
            txtNoMonth.Foreground = System.Windows.Media.Brushes.DarkGray;
            txtInterestRate.Text = "Inter. Rate Max(100)";
            txtLoanAmount.Text = "Loan Amount";
            txtNoMonth.Text = "Months Max(360)";
            lblErrorNoMonths.Content = "Invalid No. Of Months Entered.";
            lblResult.Content = "";
            lblErrorNoMonths.Visibility = Visibility.Hidden;
            lblErrorLoanAmount.Visibility = Visibility.Hidden;
            lblErrorInterestRate.Visibility = Visibility.Hidden;
        }
        
        //Placeholder In Textbox
        private void txtLoanAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLoanAmount.Text == "Loan Amount") {
                txtLoanAmount.Text = "";
                txtLoanAmount.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void txtLoanAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLoanAmount.Text == "") {
                txtLoanAmount.Text = "Loan Amount";
                txtLoanAmount.Foreground = System.Windows.Media.Brushes.DarkGray;
            }
        }        
        private void txtInterestRate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtInterestRate.Text == "Inter. Rate Max(100)")
            {
                txtInterestRate.Text = "";
                txtInterestRate.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void txtInterestRate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtInterestRate.Text == "") {
                txtInterestRate.Text = "Inter. Rate Max(100)";
                txtInterestRate.Foreground = System.Windows.Media.Brushes.DarkGray;
            }
        }
        private void txtNoMonth_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtNoMonth.Text == "Months Max(360)" || txtNoMonth.Text== "Years Max(360)")
            {
                txtNoMonth.Text = "";
                txtNoMonth.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void txtNoMonth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNoMonth.Text == "") {
                
                if (CalculateInterest.SelectedIndex == 1)
                {
                    txtNoMonth.Text = "Months Max(360)";
                }
                else {
                    txtNoMonth.Text = "Years (Max 30)";
                }
                
                txtNoMonth.Foreground = System.Windows.Media.Brushes.DarkGray;
            }
        }
        
        //For Validating User Input    Only Numeric And . Should Be Accepted
        private void txtLoanAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtInterestRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtNoMonth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CalculateInterest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int CISelectedIndex = CalculateInterest.SelectedIndex;
            if (CISelectedIndex == 0) {
                lblDuration.Content = "Number Of Months";
                txtNoMonth.Text = "Months Max(360)";
            } else if (CISelectedIndex == 1) {
                lblDuration.Content = "Number Of Years";
                txtNoMonth.Text = "Years Max(30)";
                lblErrorNoMonths.Content = "Invalid No. Of Years Entered.";
            } else if (CISelectedIndex == 2) {
                lblDuration.Content = "Number Of Months";
                txtNoMonth.Text = "Months Max(360)";
            }
            txtNoMonth.Foreground = System.Windows.Media.Brushes.DarkGray;
        }
    }
}
