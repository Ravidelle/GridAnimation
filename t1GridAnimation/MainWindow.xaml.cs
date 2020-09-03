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

namespace t1GridAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        bool _isexpanded = true;

        private GridLength _gridBodyWidthOne;
        private GridLength _gridBodyWidthZero;
        private GridLength _gridBody0Width;
        private GridLength _gridBody1Width;

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
        #endregion
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridBodyWidthOne = new GridLength(1, GridUnitType.Star);       //change this to star
            GridBodyWidthZero = new GridLength(0, GridUnitType.Star);      //change this to star
            GridBody0Width = new GridLength(1, GridUnitType.Star);
            GridBody1Width = new GridLength(1, GridUnitType.Star);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb;
            if (_isexpanded)
            {
                sb = this.FindResource("gridin") as Storyboard;
            }
            else
            {
                sb = this.FindResource("gridout") as Storyboard;
            }
            sb.Begin(this);
            _isexpanded = !_isexpanded;
        }

        

        #region Dynamic Bindings Block
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
