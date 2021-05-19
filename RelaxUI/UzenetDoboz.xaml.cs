using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace RelaxUI
{
    public enum UzenetDobozIkon : int { Semmi = 0, Ok = 1, Informacio = 2, Figyelmeztetes = 3, Hiba = 4, Kerdes = 5 }

    /// <summary>
    /// Interaction logic for UzenetDoboz.xaml
    /// </summary>
    public partial class UzenetDoboz : Window
    {
        [DllImport("user32.dll")]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        private const int GWL_STYLE = -16;
        private const uint WS_SYSMENU = 0x80000;

        public UzenetDoboz()
        {
            InitializeComponent();

            this.okgomb.Visibility = Visibility.Visible;
            this.megsegomb.Visibility = this.igengomb.Visibility = this.nemgomb.Visibility = Visibility.Collapsed;

            this.okgomb.IsDefault = true;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            _ = SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & (0xFFFFFFFF ^ WS_SYSMENU));

            base.OnSourceInitialized(e);
        }

        /// <summary>
        /// Az határozza meg az Eredmeny állapotát, hogy a dialógus melyik gombjára kattintott a felhasználó.
        /// </summary>
        public MessageBoxResult Eredmeny { get; private set; } = MessageBoxResult.None;

        /// <summary>
        /// A dialógus fejlécének tartalma.
        /// </summary>
        public string Fejlec
        {
            get { return (string)GetValue(FejlecProperty); }
            set { SetValue(FejlecProperty, value); }
        }

        public static readonly DependencyProperty FejlecProperty =
            DependencyProperty.Register("Fejlec", typeof(string), typeof(UzenetDoboz), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Az üzenet, amelyet a dialógus megjelenít.
        /// </summary>
        public string Uzenet
        {
            get { return (string)GetValue(UzenetProperty); }
            set { SetValue(UzenetProperty, value); }
        }

        public static readonly DependencyProperty UzenetProperty =
           DependencyProperty.Register("Uzenet", typeof(string), typeof(UzenetDoboz), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Meghatározza, hogy mely gombok látszódjanak a dialóguson.
        /// </summary>
        public MessageBoxButton Gombok
        {
            get { return (MessageBoxButton)GetValue(GombokProperty); }
            set { SetValue(GombokProperty, value); }
        }

        public static readonly DependencyProperty GombokProperty =
            DependencyProperty.Register("Gombok", typeof(MessageBoxButton), typeof(UzenetDoboz), new PropertyMetadata(MessageBoxButton.OK, (d, e) =>
            {
                UzenetDoboz ud = d as UzenetDoboz;
                MessageBoxButton gombok = (MessageBoxButton)e.NewValue;

                ud.okgomb.Visibility = gombok == MessageBoxButton.OK || gombok == MessageBoxButton.OKCancel ? Visibility.Visible : Visibility.Collapsed;
                ud.megsegomb.Visibility = gombok == MessageBoxButton.OKCancel || gombok == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;
                ud.igengomb.Visibility = gombok == MessageBoxButton.YesNo || gombok == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;
                ud.nemgomb.Visibility = gombok == MessageBoxButton.YesNo || gombok == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;

                ud.okgomb.IsDefault = gombok == MessageBoxButton.OK || gombok == MessageBoxButton.OKCancel;
                ud.igengomb.IsDefault = gombok == MessageBoxButton.YesNo || gombok == MessageBoxButton.YesNoCancel;
                ud.megsegomb.IsCancel = gombok == MessageBoxButton.OKCancel || gombok == MessageBoxButton.YesNoCancel;
                ud.nemgomb.IsCancel = gombok == MessageBoxButton.YesNo;
            }));

        /// <summary>
        /// Meghatázozza, hogy milyen ikon jelenjen meg a dialóguson.
        /// </summary>
        public UzenetDobozIkon Ikon
        {
            get { return (UzenetDobozIkon)GetValue(IkonProperty); }
            set { SetValue(IkonProperty, value); }
        }

        public static readonly DependencyProperty IkonProperty =
            DependencyProperty.Register("Ikon", typeof(UzenetDobozIkon), typeof(UzenetDoboz), new PropertyMetadata(UzenetDobozIkon.Semmi));

        /// <summary>
        /// Létrehoz egy üzenetdialógust és megjeleníti.
        /// </summary>
        /// <param name="_tulajdonos">A dialógus tulajdonos ablaka.</param>
        /// <param name="_uzenet">Az üzenet, amelyet a dialógus megjelenít.</param>
        /// <param name="_fejlec">A dialógus fejlécének tartalma.</param>
        /// <param name="_gombok">Meghatározza, hogy melyik gombok látszódjanak a dialóduson.</param>
        /// <param name="_ikon">Meghatázozza, hogy milyen ikon jelenjen meg a dialóguson.</param>
        /// <returns></returns>
        public static MessageBoxResult Megjelenit(Window _tulajdonos, string _uzenet, string _fejlec = null, MessageBoxButton _gombok = MessageBoxButton.OK, UzenetDobozIkon _ikon = UzenetDobozIkon.Semmi)
        {
            UzenetDoboz ud = new() { Owner = _tulajdonos, Uzenet = _uzenet, Fejlec = _fejlec, Gombok = _gombok, Ikon = _ikon };

            if (string.IsNullOrEmpty(_fejlec))
                ud.Fejlec = _ikon switch
                {
                    UzenetDobozIkon.Semmi          => "Üzenet",
                    UzenetDobozIkon.Ok             => "Üzenet",
                    UzenetDobozIkon.Informacio     => "Információ",
                    UzenetDobozIkon.Figyelmeztetes => "Figyelmeztetés",
                    UzenetDobozIkon.Hiba           => "Hiba",
                    UzenetDobozIkon.Kerdes         => "Kérdés",
                    _                              => "Üzenet"
                };

            ud.ShowDialog();

            return ud.Eredmeny;
        }

        /// <summary>
        /// Létrehoz egy üzenetdialógust és megjeleníti.
        /// </summary>
        /// <param name="_uzenet">Az üzenet, amelyet a dialógus megjelenít.</param>
        /// <param name="_fejlec">A dialógus fejlécének tartalma.</param>
        /// <param name="_gombok">Meghatározza, hogy melyik gombok látszódjanak a dialóduson.</param>
        /// <param name="_ikon">Meghatázozza, hogy milyen ikon jelenjen meg a dialóguson.</param>
        /// <returns></returns>
        public static MessageBoxResult Megjelenit(string _uzenet, string _fejlec = null, MessageBoxButton _gombok = MessageBoxButton.OK, UzenetDobozIkon _ikon = UzenetDobozIkon.Semmi)
        {
            UzenetDoboz ud = new() { Uzenet = _uzenet, Gombok = _gombok, Ikon = _ikon };

            if (string.IsNullOrEmpty(_fejlec))
                ud.Fejlec = _ikon switch
                {
                    UzenetDobozIkon.Semmi          => "Üzenet",
                    UzenetDobozIkon.Ok             => "Üzenet",
                    UzenetDobozIkon.Informacio     => "Információ",
                    UzenetDobozIkon.Figyelmeztetes => "Figyelmeztetés",
                    UzenetDobozIkon.Hiba           => "Hiba",
                    UzenetDobozIkon.Kerdes         => "Kérdés",
                    _                              => "Üzenet"
                };

            ud.Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            ud.ShowDialog();

            return ud.Eredmeny;
        }


#pragma warning disable IDE1006 // Naming Styles
        private void okgomb_Click(object sender, RoutedEventArgs e)
        {
            Eredmeny = MessageBoxResult.OK;
            this.Close();
        }

        private void megsegomb_Click(object sender, RoutedEventArgs e)
        {
            Eredmeny = MessageBoxResult.Cancel;
            this.Close();
        }

        private void igengomb_Click(object sender, RoutedEventArgs e)
        {
            Eredmeny = MessageBoxResult.Yes;
            this.Close();
        }

        private void nemgomb_Click(object sender, RoutedEventArgs e)
        {
            Eredmeny = MessageBoxResult.No;
            this.Close();
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
