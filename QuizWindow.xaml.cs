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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private QuizItem item;
        DispatcherTimer _timer;
        TimeSpan _time;

        public QuizWindow(QuizItem item)
        {
            this.item = item;
            InitializeComponent();
            InitializeLabelContents();

            _time = TimeSpan.FromSeconds(59);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                mytimer.Text = _time.ToString("c").Split(":")[2];
                if (_time == TimeSpan.Zero)
                {
                    this.DialogResult = false;
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }
        private void InitializeLabelContents()
        {
            (mypanel.Children[0] as Label).Content = this.item.Question.ToString();
            for (int i = 1; i < mypanel.Children.Count; i++)
            {
                (mypanel.Children[i] as Label).Content = this.item.Choices[i - 1];
            }
        }
        private void an1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Label && e.Source != null)
            {
                if ((e.Source as Label).Content.ToString() == this.item.Answer)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == null)
            {
                MessageBoxResult result = MessageBox.Show("Be akarja zárni az ablakot?", "QuizWindow", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        break;
                    case MessageBoxResult.No:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
