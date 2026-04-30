using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Clock_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double _ratio = 2 / 1;

        private DispatcherTimer _timer = new();

        private TimeSpan _totalTime = TimeSpan.Zero;
        private int StudyTime = 0;
        private int RestTime = 0;
        private int StudyCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            //设置窗口比例大小
            this.SizeChanged += MainWindow_SizeChanged;
            
            
        }

        private void InitTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100); // 每 100ms 触发一次
            _timer.Tick += Timer_Tick; // 计时事件
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _totalTime.Add(TimeSpan.FromMilliseconds(100));
        }


        #region Time Control Button Event
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessData();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender,RoutedEventArgs e)
        {

        }
        #endregion


        #region TextBox Input Event
        private void Num1TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessTextBoxIntInput(sender,ref StudyTime);
        }

        private void Num2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessTextBoxIntInput(sender,ref RestTime);
        }
        private void Num3TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessTextBoxIntInput(sender,ref StudyCount);
        }

        private void NumPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }


        private bool ProcessTextBoxIntInput(object sender, ref int res)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return false;
            bool b = Int32.TryParse(textBox.Text, out res);
            if (!b)
            {
                MessageBox.Show("Please Input Correct Context");
            }

            return b;
        }
        #endregion


        #region SetClock
        private bool ProcessData()
        {
            int allTime = StudyTime * StudyCount + RestTime * (StudyCount - 1);
            MessageBox.Show($"AllTime:{allTime}");


            return true;
        }

        #region Set SystemClock
        private void SetClock(PreData preData)
        {
            _timer.Start();
        }


        private void ClearClock()
        {

        }


        private void ClearClock(PreData preData)
        {

        }
        #endregion

        #endregion



        #region UI Style Change Method

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 先取消事件避免死循环
            this.SizeChanged -= MainWindow_SizeChanged;

            try
            {
                double w = this.ActualWidth;
                double h = this.ActualHeight;

                // 按比例修正高度
                double newH = w / _ratio;
                if (Math.Abs(h - newH) > 1)
                {
                    this.Height = newH;
                }
            }
            finally
            {
                // 重新绑定事件
                this.SizeChanged += MainWindow_SizeChanged;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion


    }
}