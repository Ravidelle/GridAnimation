using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rtGridAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _toggle = false;

        private GridLength _gridBodyWidthOne;
        private GridLength _gridBodyWidthZero;
        private GridLength _gridBody0Width;
        private GridLength _gridBody1Width;
        private GridLength _gridBodyX;

        Storyboard storyboard1;
        Storyboard storyboard2;

        #region Binders
        public GridLength GridBody0Width
        {
            get { return _gridBody0Width; }
            set
            {
                if (_gridBody0Width == null || _gridBody0Width != value)
                {
                    _gridBody0Width = value;
                    OnPropertyChanged();
                }
            }
        }

        public GridLength GridBody1Width
        {
            get { return _gridBody1Width; }
            set
            {
                if (_gridBody1Width == null || _gridBody1Width != value)
                {
                    _gridBody1Width = value;
                    OnPropertyChanged();
                }
            }
        }

        public GridLength GridBodyWidthOne
        {
            get { return _gridBodyWidthOne; }
            set
            {
                if (_gridBodyWidthOne == null || _gridBodyWidthOne != value)
                {
                    _gridBodyWidthOne = value;
                    OnPropertyChanged();
                }
            }
        }

        public GridLength GridBodyWidthZero
        {
            get { return _gridBodyWidthZero; }
            set
            {
                if (_gridBodyWidthZero == null || _gridBodyWidthZero != value)
                {
                    _gridBodyWidthZero = value;
                    OnPropertyChanged();
                }
            }
        }

        public GridLength GridBodyX
        {
            get { return _gridBodyX; }
            set
            {
                if (_gridBodyX == null || _gridBodyX != value)
                {
                    _gridBodyX = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _toggle = false;
            GridBodyWidthOne = new GridLength(1, GridUnitType.Star);
            GridBodyWidthZero = new GridLength(0, GridUnitType.Star);
            GridBody0Width = GridBodyWidthOne;
            GridBody1Width = GridBodyWidthZero;
            GridBodyX = GridBodyWidthZero;

            storyboard1 = this.FindResource("expandBody0") as Storyboard;
            storyboard1.FillBehavior = FillBehavior.Stop;
            storyboard1.Completed += (object o, EventArgs ea) =>
            {
                GridBody0.Width = GridBodyWidthOne;
                GridBody1.Width = GridBodyWidthZero;
                GridBody1.BeginAnimation(ColumnDefinition.WidthProperty, null);
                GridSplitter0.IsEnabled = false;
            };

            storyboard2 = this.FindResource("retractBody0") as Storyboard;
            storyboard2.FillBehavior = FillBehavior.Stop;
            storyboard2.Completed += (object o, EventArgs ea) =>
            {
                GridBody0.Width = GridBodyWidthOne;
                GridBody1.Width = GridBodyWidthOne;
                GridBody1.BeginAnimation(ColumnDefinition.WidthProperty, null);
                GridSplitter0.IsEnabled = true;
            };
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleMainBody1();
        }

        private void GridSplitter_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void GridSplitter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void GridSplitter_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GridSplitter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToggleMainBody1()
        {
            GridBodyX = GridBodyWidthOne;
            StopAnimation();

            if (!_toggle)
            {
                RevealMainBody1();
            }
            else
            {
                HideMainBody1();
            }

            _toggle = !_toggle;
        }
        private void HideMainBody1()
        {
            storyboard1.Begin(this, true);
        }

        private void RevealMainBody1()
        {
            double g0 = GridBody0Width.Value;
            double g1 = GridBody1Width.Value;
            double x = g0 / (g0+g1);

            GridBodyX = new GridLength(x, GridUnitType.Star);
            storyboard2.Begin(this, true);
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            StopAnimation();

            GridBody0Width = new GridLength(9, GridUnitType.Star);
            GridBody1Width = new GridLength(1, GridUnitType.Star);
        }

        private void StopAnimation()
        {
            storyboard1.Stop(this);
            storyboard2.Stop(this);

            double maxWidth = GridBody0.ActualWidth + GridBody1.ActualWidth;
            double width0 = GridBody0.ActualWidth / maxWidth;
            double width1 = GridBody1.ActualWidth / maxWidth;
            GridBody0Width = new GridLength(width0, GridUnitType.Star);
            GridBody1Width = new GridLength(width1, GridUnitType.Star);

            PreprocessDisplayText();
        }

        private void PreprocessDisplayText()
        {
            string text = "Start G0:   " + GridBody0Width.ToString() + "          | Start G1:  " + GridBody1Width.ToString() + Environment.NewLine;

            if (!_toggle)
            {
                text += "Reveal G1 = From:   " + GridBody1Width.ToString() + "          | To:   " + GridBodyWidthOne.ToString() + Environment.NewLine;
            }
            else
            {
                text += "Hide1 = From:" + GridBody1Width.ToString() + " | To:" + GridBodyWidthZero.ToString() + Environment.NewLine;
            }

            double i = GridBody1.ActualWidth;
            double N = (GridMainContainer.ActualWidth - GridBodyDivider.ActualWidth) / 2;

            //float N
            text += "Whole Grid Width = " + GridMainContainer.ActualWidth.ToString() + Environment.NewLine;
            text += "i = " + i.ToString() + " (at the time of clicking)" + Environment.NewLine;
            
            text += "N = " + N.ToString();

            SetDisplayText(text);
        }

        private void SetDisplayText(string text)
        {
            TextBoxDisplay.Text = text;
        }
    }
}
