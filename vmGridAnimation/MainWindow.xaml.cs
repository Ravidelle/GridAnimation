using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using vmGridAnimation.ViewModels;

namespace vmGridAnimation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _bodyToggle = false;

        private GridLength _gridBodyWidthOne;
        private GridLength _gridBodyWidthZero;
        private GridLength _gridBodyFrom;
        private GridLength _gridBodyTo;
        private GridLength _gridBodyX;
        private GridLength _gridBodyEndWidth;

        Storyboard storyboard1;
        Storyboard storyboard2;

        public RelayCommand ToggleMainBodyCommand { get; private set; }

        private GridAnimationViewModel g = new GridAnimationViewModel();

        #region Binders
        public bool BodyToggle
        {
            get { return _bodyToggle; }
            set
            {
                if (_bodyToggle != value)
                {
                    _bodyToggle = value;
                    OnPropertyChanged();
                }
            }
        }
        public GridLength GridBodyEndWidth
        {
            get { return _gridBodyEndWidth; }
            set
            {
                if (_gridBodyEndWidth == null || _gridBodyEndWidth != value)
                {
                    _gridBodyEndWidth = value;
                    OnPropertyChanged();
                }
            }
        }
        public GridLength GridBodyFrom
        {
            get { return _gridBodyFrom; }
            set
            {
                if (_gridBodyFrom == null || _gridBodyFrom != value)
                {
                    _gridBodyFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        public GridLength GridBodyTo
        {
            get { return _gridBodyTo; }
            set
            {
                if (_gridBodyTo == null || _gridBodyTo != value)
                {
                    _gridBodyTo = value;
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
            InitializeComponent();
            DataContext = this;

            GridBodyWidthOne = new GridLength(1, GridUnitType.Star);
            GridBodyWidthZero = new GridLength(0, GridUnitType.Star);
            GridBodyFrom = GridBodyWidthOne;
            GridBodyTo = GridBodyWidthZero;

            ToggleMainBodyCommand = new RelayCommand(ToggleMainBody, ToggleMainBodyCanUse);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

        }

        public void ToggleMainBody(object o)
        {
            //GridBodyX = GridBodyWidthOne;
            //StopAnimation();

            //if (!_toggle)
            //{
            //    RevealMainBody1();
            //}
            //else
            //{
            //    HideMainBody1();
            //}

            //_toggle = !_toggle;
        }

        public bool ToggleMainBodyCanUse(object o)
		{
            return true;
		}

        private void HideMainBody1()
        {
            //storyboard1.Begin(this, true);
        }

        private void RevealMainBody1()
        {
            //double g0 = GridBody0Width.Value;
            //double g1 = GridBody1Width.Value;
            //double x = g0 / (g0 + g1);

            //GridBodyX = new GridLength(x, GridUnitType.Star);
            //storyboard2.Begin(this, true);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            //StopAnimation();

            //GridBody0Width = new GridLength(9, GridUnitType.Star);
            //GridBody1Width = new GridLength(1, GridUnitType.Star);
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
		#endregion

		private void Button2_Click(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("F:" + GridBodyFrom + " | " + "T:" + GridBodyTo);
            //GridBodyFrom = GridBodyWidthOne;
            //GridBodyTo = GridBodyWidthZero;
        }

		private void GridLengthAnimation_Completed(object sender, EventArgs e)
		{

		}

		private void SetMainBodyToggle(object sender, EventArgs e)
		{
            if(_bodyToggle)
			{
                GridBodyFrom = GridBodyWidthOne;
                GridBodyTo = GridBodyWidthZero;
            }
            else
			{
                GridBodyFrom = GridBodyWidthZero;
                GridBodyTo = GridBodyWidthOne;
            }

            _bodyToggle = !_bodyToggle;
		}
	}
}
