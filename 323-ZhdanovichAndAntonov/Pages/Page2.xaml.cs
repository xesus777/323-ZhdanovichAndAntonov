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

namespace _323_ZhdanovichAndAntonov.Pages
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private double GetFunctionValue(double x)
        {
            if (rbSh.IsChecked == true)
                return Math.Sinh(x);
            else if (rbX2.IsChecked == true)
                return x * x;
            else 
                return Math.Exp(x);
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtX.Text) ||
                    string.IsNullOrWhiteSpace(txtY.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля ввода",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!double.TryParse(txtX.Text, out double x))
                {
                    MessageBox.Show("Некорректное значение x", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtY.Text, out double y))
                {
                    MessageBox.Show("Некорректное значение y", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double fx = GetFunctionValue(x);
                double result;

                
                if (x > y)
                {
                    result = Math.Pow(fx - y, 3) + Math.Atan(fx);
                }
                else if (y > x)
                {
                    result = Math.Pow(y - fx, 3) + Math.Atan(fx);
                }
                else 
                {
                    result = Math.Pow(y + fx, 3) + 0.5;
                }

                txtResult.Text = result.ToString("F6");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear();
            txtY.Clear();
            txtResult.Clear();
            rbSh.IsChecked = true;
        }
    }
}
