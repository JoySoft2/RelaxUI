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
    /// Interaction logic for Minosito.xaml
    /// </summary>
    public partial class Minosito : UserControl
    {
        private static readonly SolidColorBrush hlbrush = new (Colors.Orange),
                                                grbrush = new (Colors.LightGray);

        public Minosito()
        {
            InitializeComponent();
        }

        [Category("Common")]
        public int Minoseg
        {
            get { return (int)GetValue(MinosegProperty); }
            set { SetValue(MinosegProperty, value); }
        }

        public static readonly DependencyProperty MinosegProperty =
            DependencyProperty.Register("Minoseg", typeof(int), typeof(Minosito), new PropertyMetadata(0, MinosegValtozott));

        private static void MinosegValtozott(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null) return;

            Minosito m = d as Minosito;
            TextBlock[] txts = new TextBlock[] { m.csillag1, m.csillag2, m.csillag3, m.csillag4, m.csillag5 };

            for (int i = 0; i < 5; i++) txts[i].Foreground = i < (int)e.NewValue ? hlbrush : grbrush;
        }

#pragma warning disable IDE1006 // Naming Styles
        private void csillag1_MouseDown(object sender, MouseButtonEventArgs e) => Minoseg = 1;

        private void csillag2_MouseDown(object sender, MouseButtonEventArgs e) => Minoseg = 2;

        private void csillag3_MouseDown(object sender, MouseButtonEventArgs e) => Minoseg = 3;

        private void csillag4_MouseDown(object sender, MouseButtonEventArgs e) => Minoseg = 4;

        private void csillag5_MouseDown(object sender, MouseButtonEventArgs e) => Minoseg = 5;
#pragma warning restore IDE1006 // Naming Styles
    }
}
