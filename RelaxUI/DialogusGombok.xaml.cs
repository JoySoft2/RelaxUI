using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RelaxUI
{
    /// <summary>
    /// Interaction logic for DialogusGombok.xaml
    /// </summary>
    public partial class DialogusGombok : UserControl
    {
        public DialogusGombok()
        {
            InitializeComponent();
        }

        public delegate void OkGombKattHandler();
        public event OkGombKattHandler OkGombKatt = null;
        public delegate void MegseGombKattHandler();
        public event MegseGombKattHandler MegseGombKatt = null;

        [Category("Common")]
        public string OkGombSzoveg
        {
            get { return (string)GetValue(OkGombSzovegProperty); }
            set { SetValue(OkGombSzovegProperty, value); }
        }

        public static readonly DependencyProperty OkGombSzovegProperty =
            DependencyProperty.Register("OkGombSzoveg", typeof(string), typeof(DialogusGombok), new PropertyMetadata("Ok"));

        [Category("Common")]
        public string MegseGombSzoveg
        {
            get { return (string)GetValue(MegseGombSzovegProperty); }
            set { SetValue(MegseGombSzovegProperty, value); }
        }

        public static readonly DependencyProperty MegseGombSzovegProperty =
            DependencyProperty.Register("MegseGombSzoveg", typeof(string), typeof(DialogusGombok), new PropertyMetadata("Mégse"));

        [Category("Common")]
        public double OkGombSzelesseg
        {
            get { return (double)GetValue(OkGombSzelessegProperty); }
            set { SetValue(OkGombSzelessegProperty, value); }
        }

        public static readonly DependencyProperty OkGombSzelessegProperty =
            DependencyProperty.Register("OkGombSzelesseg", typeof(double), typeof(DialogusGombok), new PropertyMetadata(80.0d));

        [Category("Common")]
        public double MegseGombSzelesseg
        {
            get { return (double)GetValue(MegseGombSzelessegProperty); }
            set { SetValue(MegseGombSzelessegProperty, value); }
        }

        public static readonly DependencyProperty MegseGombSzelessegProperty =
            DependencyProperty.Register("MegseGombSzelesseg", typeof(double), typeof(DialogusGombok), new PropertyMetadata(80.0d));

        [Category("Common")]
        public bool MegseGombLathato
        {
            get { return (bool)GetValue(MegseGombLathatoProperty); }
            set { SetValue(MegseGombLathatoProperty, value); }
        }

        public static readonly DependencyProperty MegseGombLathatoProperty =
            DependencyProperty.Register("MegseGombLathato", typeof(bool), typeof(DialogusGombok), new PropertyMetadata(true));

        [Category("Common")]
        public bool TulajdonosAblakotBezar
        {
            get { return (bool)GetValue(TulajdonosAblakotBezarProperty); }
            set { SetValue(TulajdonosAblakotBezarProperty, value); }
        }

        public static readonly DependencyProperty TulajdonosAblakotBezarProperty =
            DependencyProperty.Register("TulajdonosAblakotBezar", typeof(bool), typeof(DialogusGombok), new PropertyMetadata(true));

        private void OkGomb_Click(object sender, RoutedEventArgs e)
        {
            OkGombKatt?.Invoke();

            if (TulajdonosAblakotBezar)
            {
                Window tulajdonosablak = Window.GetWindow(this);

                if (System.Windows.Interop.ComponentDispatcher.IsThreadModal) tulajdonosablak.DialogResult = true;

                tulajdonosablak.Close();
            }
        }

        private void MegseGomb_Click(object sender, RoutedEventArgs e)
        {
            MegseGombKatt?.Invoke();

            /*if (TulajdonosAblakotBezar)
            {
                Window tulajdonosablak = Window.GetWindow(this);

                if (System.Windows.Interop.ComponentDispatcher.IsThreadModal) tulajdonosablak.DialogResult = false;

                tulajdonosablak.Close();
            }*/
        }
    }
}
