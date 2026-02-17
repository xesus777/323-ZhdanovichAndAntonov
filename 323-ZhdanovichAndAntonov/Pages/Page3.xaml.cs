using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ResultPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public ResultPoint(double x, double y)
        {
            X = Math.Round(x, 3);
            Y = Math.Round(y, 6);
        }
    }

    public partial class Page3 : Page
    {
        private ObservableCollection<ResultPoint> results;

        public Page3()
        {
            InitializeComponent();
            results = new ObservableCollection<ResultPoint>();
            ResultsList.ItemsSource = results;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtA.Text) ||
                    string.IsNullOrWhiteSpace(txtB.Text) ||
                    string.IsNullOrWhiteSpace(txtX0.Text) ||
                    string.IsNullOrWhiteSpace(txtXk.Text) ||
                    string.IsNullOrWhiteSpace(txtDx.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля ввода",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!double.TryParse(txtA.Text.Replace('.', ','), out double a))
                {
                    MessageBox.Show("Некорректное значение a. Используйте число (например: 1,35 или 1.35)",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtB.Text.Replace('.', ','), out double b))
                {
                    MessageBox.Show("Некорректное значение b. Используйте число (например: -6,25 или -6.25)",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtX0.Text.Replace('.', ','), out double x0))
                {
                    MessageBox.Show("Некорректное значение x₀. Используйте число (например: -1,5 или -1.5)",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtXk.Text.Replace('.', ','), out double xk))
                {
                    MessageBox.Show("Некорректное значение xₖ. Используйте число (например: 10,3 или 10.3)",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtDx.Text.Replace('.', ','), out double dx))
                {
                    MessageBox.Show("Некорректное значение dx. Используйте число (например: 0,25 или 0.25)",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (dx <= 0)
                {
                    MessageBox.Show("Шаг dx должен быть положительным числом",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (x0 >= xk)
                {
                    MessageBox.Show("Начальное значение x₀ должно быть меньше конечного xₖ",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                results.Clear();

                for (double x = x0; x <= xk + dx / 2; x += dx) 
                {
                    try
                    {
                        double xCubed = Math.Pow(x, 3);
                        double y = a * xCubed + Math.Pow(Math.Cos(xCubed - b), 2);

                        results.Add(new ResultPoint(x, y));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при вычислении для x = {x}: {ex.Message}",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                UpdateChart();

                MessageBox.Show($"Вычислено {results.Count} точек",
                    "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateChart()
        {
            try
            {
                FunctionSeries.ItemsSource = null;

                FunctionSeries.ItemsSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении графика: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtA.Text = "1,35";
            txtB.Text = "-6,25";
            txtX0.Text = "-1,5";
            txtXk.Text = "10,3";
            txtDx.Text = "0,25";

            results.Clear();

            FunctionSeries.ItemsSource = null;
        }
    }
}
