using System;
using System.Collections.Generic;
using System.IO;
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

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<QuizItem> list = new List<QuizItem>();
            foreach (string line in File.ReadLines("questions.txt"))
            {
                string[] obj = line.Split(";");
                list.Add(new QuizItem(obj[0], new List<string> { obj[1], obj[2], obj[3], obj[4] }, obj[5]));
            }

            list.ForEach(t =>
            {
                Label l = new Label();
                l.Tag = t;
                l.Background = Brushes.LightBlue;
                l.Margin = new Thickness(20);
                l.Width = this.ActualWidth / 12;
                l.Height = this.ActualHeight / 8;
                wrap.Children.Add(l);

                l.MouseLeftButtonDown += L_MouseLeftButtonDown;
            });
        }
        private void L_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label l = (sender as Label);
            QuizItem w = (QuizItem)(l.Tag);

            QuizWindow qw = new QuizWindow(w);
            qw.ShowDialog();
            if (qw.DialogResult == true)
            {
                l.Background = Brushes.LightGreen;
                l.IsEnabled = false;
            }
            else if (qw.DialogResult == false)
            {
                l.Background = Brushes.LightPink;
                l.IsEnabled = false;
            }
        }
    }
    public class QuizItem
    {
        public string Question { get; set; }
        public List<string> Choices
        {
            get; set;
        }

        public string Answer { get; set; }

        public QuizItem(string question, List<string> choices, string answer)
        {
            this.Question = question;
            this.Choices = choices;
            this.Answer = answer;
        }
    }
}
