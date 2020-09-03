using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace vmGridAnimation.ViewModels
{
	class GridAnimationViewModel : INotifyPropertyChanged
	{
        private bool _bodyToggle = false;

        private GridLength _gridBodyWidthOne;
        private GridLength _gridBodyWidthZero;
        private GridLength _gridBodyFrom;
        private GridLength _gridBodyTo;
        private GridLength _gridBodyX;
        private GridLength _gridBodyEndWidth;

        private string _something;

        Storyboard storyboard1;
        Storyboard storyboard2;

        public RelayCommand ToggleMainBodyCommand { get; private set; }

        #region Binders
        public string Something
		{
            get { return _something; }
            set
			{
                if (_something != value)
                {
                    _something = value;
                    OnPropertyChanged();
				}
			}
		}

        public bool BodyToggle
		{
            get { return _bodyToggle; }
            set
			{
                if(_bodyToggle != value)
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

		public GridAnimationViewModel()
		{
            BodyToggle = false;
            GridBodyWidthOne = new GridLength(1, GridUnitType.Star);
            GridBodyWidthZero = new GridLength(0, GridUnitType.Star);
            GridBodyFrom = new GridLength(1, GridUnitType.Star);
            GridBodyTo = new GridLength(0, GridUnitType.Star);
            GridBodyX = GridBodyWidthZero;

            ToggleMainBodyCommand = new RelayCommand(ToggleMainBody, ToggleMainBodyCanUse);
            Something = "HAHAHAHA";
        }

        public void ToggleMainBody(object something)
		{
            //GridBodyX = GridBodyWidthOne;

            //if (!_toggle)
            //{
            //	RevealMainBody1();
            //}
            //else
            //{
            //	HideMainBody1();
            //}
            if(BodyToggle)
			{
                //GridBodyEndWidth = GridBody1Width;
                GridBodyFrom = GridBodyWidthOne;
                GridBodyTo = GridBodyWidthZero;
            }
            else
			{
                GridBodyFrom = GridBodyWidthZero;
                GridBodyTo = GridBodyWidthOne;
                //GridBodyEndWidth = GridBody0Width;
            }
            

            BodyToggle = !BodyToggle;
		}

        public bool ToggleMainBodyCanUse(object message)
		{
            return true;
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
