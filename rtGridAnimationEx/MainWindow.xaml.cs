using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rtGridAnimationEx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Global Variables
        private bool _toggleMainBody1;
        private int _ucLoaderIndex;
        private string _employeeID;
        private string _employeeName;
        private string _regularFunctionsLevel;
        private string _accountingFunctionsLevel;

        private double _gridBody0Width;
        private double _gridBody0WidthMax;
        private double _gridBody0WidthHalf;
        private double _gridBody1WidthMax;
        private double _gridBody1Width;
        private double _gridWindowWidth;
        public double GridBody0Width
        {
            get { return _gridBody0Width; }
            set
            {
                if (_gridBody0Width != value)
                {
                    _gridBody0Width = value;
                    OnPropertyChanged();
                }
            }
        }
        public double GridBody0WidthMax
        {
            get { return _gridBody0WidthMax; }
            set
            {
                if (_gridBody0WidthMax != value)
                {
                    _gridBody0WidthMax = value;
                    OnPropertyChanged();
                }
            }
        }
        public double GridBody0WidthHalf
        {
            get { return _gridBody0WidthHalf; }
            set
            {
                if (_gridBody0WidthHalf != value)
                {
                    _gridBody0WidthHalf = value;
                    OnPropertyChanged();
                }
            }
        }
        public double GridBody1WidthMax
        {
            get { return _gridBody1WidthMax; }
            set
            {
                if (_gridBody1WidthMax != value)
                {
                    _gridBody1WidthMax = value;
                    OnPropertyChanged();
                }
            }
        }
        public double GridBody1Width
        {
            get { return _gridBody1Width; }
            set
            {
                if (_gridBody1Width != value)
                {
                    _gridBody1Width = value;
                    OnPropertyChanged();
                }
            }
        }
        public double GridWindowWidth
        {
            get { return _gridWindowWidth; }
            set
            {
                if (_gridWindowWidth != value)
                {
                    _gridWindowWidth = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        #region Initialization
        public MainWindow()
        {
            Name = "MainWindow";
            InitializeComponent();
            InitializeCustomWindow();
            Awake();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Start();
        }
        #endregion
        #region Events: Window
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridWindowWidth = e.NewSize.Width;

            GridBody0WidthMax = GridWindowWidth - GridBodyLeftControl.ActualWidth;
            GridBody0WidthHalf = (GridWindowWidth - GridBodyLeftControl.ActualWidth - GridBodyDivider.ActualWidth) / 2;
            GridBody1WidthMax = GridBody0WidthHalf;

            if (!_toggleMainBody1)
            {
                GridBody0.Width = new GridLength(GridBody0WidthMax);
                //HideMainBody1();
            }
            else
            {
                //HideMainBody1();
            }

            string debug = "";
            debug += _toggleMainBody1 ? "Revealing - " : "Hiding - ";
            debug += GridBody0.ActualWidth.ToString() + " : " + GridBody1.ActualWidth.ToString() + " <" + GridWindowWidth.ToString() + ">" +
                " | " + GridBody0.Width.ToString() + " : " + GridBody1.Width.ToString();
            GridBody0Width = GridBody0.ActualWidth;
            DebugSetTitleContent(debug);
        }
        #endregion
        #region Events: Grid Splitter
        private void GridSplitter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion
        private void SetTitleBarBackground(string title)
        {
            LabelTitleBar.Content = title;
        }
        private void ToggleMainBody1()
        {
            if (FrameMainBody1.Content != uc)
            {
                RevealMainBody1();
            }
            else
            {
                HideMainBody1();
            }
        }
        private void ToggleMainBody1()
        {
            if (!_toggleMainBody1)
                RevealMainBody1();
            else
                HideMainBody1();

            _toggleMainBody1 = !_toggleMainBody1;
        }
        private void HideMainBody1()
        {
            if (_toggleMainBody1)
            {
                Storyboard sb;
                sb = this.FindResource("retractBody0") as Storyboard;
                sb.Begin(this);
            }
        }
        private void RevealMainBody1()
        {
            if (!_toggleMainBody1)
            {
                Storyboard sb;
                sb = this.FindResource("expandBody0") as Storyboard;
                sb.Begin(this);
            }
        }
        
        #region Dynamic Bindings Block
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /*
         * protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if(handler!=null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        */
        #endregion
        #region Customized Window/Title Bar Block
        private void InitializeCustomWindow()
        {
            SourceInitialized += (s, e) =>
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
            };
            ButtonMinimize.Click += (s, e) => WindowState = WindowState.Minimized;
            ButtonMaximize.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            ButtonClose.Click += (s, e) => Close();
        }
        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }
        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary> x coordinate of point. </summary>
            public int x;
            /// <summary> y coordinate of point. </summary>
            public int y;
            /// <summary> Construct a point of coordinates (x, y). </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public static readonly RECT Empty = new RECT();
            public int Width { get { return Math.Abs(right - left); } }
            public int Height { get { return bottom - top; } }

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }

            public bool IsEmpty { get { return left >= right || top >= bottom; } }

            public override string ToString()
            {
                if (this == Empty) { return "RECT {Empty}"; }

                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }

            ///<summary> Return the HashCode for this struct (not guaranteed to be unique) </summary>
            public override int GetHashCode() => left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();

            ///<summary> Determine if 2 RECT are equal (deep compare) </summary>
            public static bool operator ==(RECT rect1, RECT rect2) { return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom); }

            ///<summary> Determine if 2 RECT are different(deep compare) </summary>
            public static bool operator !=(RECT rect1, RECT rect2) { return !(rect1 == rect2); }
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);


        #endregion
        #region Debug/Tester
        private void ButtonLoadTest_Click(object sender, RoutedEventArgs e)
        {
            string debug = "";
            debug += _toggleMainBody1 ? "Revealing - " : "Hiding - ";
            debug += GridBody0.ActualWidth.ToString() + " : " + GridBody1.ActualWidth.ToString() + " <" + GridWindowWidth.ToString() + ">" +
                " | " + GridBody0.Width.ToString() + " : " + GridBody1.Width.ToString();
            GridBody0Width = GridBody0.ActualWidth;
            DebugSetTitleContent(debug);

            if (!_toggleMainBody1)
            {
                RevealMainBody1();
            }
            else
            {
                HideMainBody1();
            }
            _toggleMainBody1 = !_toggleMainBody1;
        }
        private void DebugSetTitleContent(string title)
        {
            LabelTitleBar.Content = title;
        }
        #endregion
    }

    public enum Container
    {
        Frame0,
        Frame1
    }
}
