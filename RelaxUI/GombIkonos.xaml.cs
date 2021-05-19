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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RelaxUI
{
    /// <summary>
    /// Interaction logic for GombIkonos.xaml
    /// </summary>
    [ContentProperty("Szoveg")]
    public partial class GombIkonos : Button
    {
        public GombIkonos()
        {
            InitializeComponent();
        }

        [Category("Common")]
        public ImageSource Ikon
        {
            get { return (ImageSource)GetValue(IkonProperty); }
            set { SetValue(IkonProperty, value); }
        }

        public static readonly DependencyProperty IkonProperty =
            DependencyProperty.Register("Ikon", typeof(ImageSource), typeof(GombIkonos), new PropertyMetadata(null));

        [Category("Common")]
        public string Szoveg
        {
            get { return (string)GetValue(SzovegProperty); }
            set { SetValue(SzovegProperty, value); }
        }

        public static readonly DependencyProperty SzovegProperty =
            DependencyProperty.Register("Szoveg", typeof(string), typeof(GombIkonos), new PropertyMetadata(string.Empty));

        [Category("Common")]
        public Thickness IkonMargo
        {
            get { return (Thickness)GetValue(IkonMargoProperty); }
            set { SetValue(IkonMargoProperty, value); }
        }

        public static readonly DependencyProperty IkonMargoProperty =
            DependencyProperty.Register("IkonMargo", typeof(Thickness), typeof(GombIkonos), new PropertyMetadata(new Thickness(.0d, .0d, 6.0d, .0d)));
    }
}
