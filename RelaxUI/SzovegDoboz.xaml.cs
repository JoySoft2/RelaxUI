using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public enum SzovegDobozBemenetFajtak : int { Barmi = 0, Betuk, EgeszSzamok, LebegopontosSzamok, BetukEsEgeszSzamok, Email, Telefonszam }

    /// <summary>
    /// Interaction logic for SzovegDoboz.xaml
    /// </summary>
    public partial class SzovegDoboz : TextBox
    {
        private static readonly string[] szuro_regex = new string[]
        {
            string.Empty, // bármi
            @"^[a-zA-ZáéíóöőúüűÁÉÍÓÖŐÚÜŰ]+$", // betűk
            @"^[0-9]+$", // egész számok
            @"^[0-9,]+$", // lebegőpontos számok
            @"^[0-9a-zA-ZáéíóöőúüűÁÉÍÓÖŐÚÜŰ]+$", // betűk és egész számok
            @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", // E-mail
            @"/^[\/+][36]{1,2}[ ][\/(][1-9]{1,2}[\/)][ ][1-9]{3}[-][1-9]{3,4}$/" // Telefonszám
        };

        public SzovegDoboz()
        {
            InitializeComponent();
        }

        [Category("Common")]
        public SzovegDobozBemenetFajtak BemenetFajtaja
        {
            get { return (SzovegDobozBemenetFajtak)GetValue(BemenetFajtajaProperty); }
            set { SetValue(BemenetFajtajaProperty, value); }
        }

        public static readonly DependencyProperty BemenetFajtajaProperty =
            DependencyProperty.Register("BemenetFajtaja", typeof(SzovegDobozBemenetFajtak), typeof(SzovegDoboz), new PropertyMetadata(SzovegDobozBemenetFajtak.Barmi));

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = BemenetFajtaja != SzovegDobozBemenetFajtak.Barmi && !new Regex(szuro_regex[(int)this.BemenetFajtaja]).IsMatch(e.Text);

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (BemenetFajtaja == SzovegDobozBemenetFajtak.Barmi) return;
            if (!e.DataObject.GetDataPresent(typeof(string))) e.CancelCommand();

            if (!new Regex(szuro_regex[(int)this.BemenetFajtaja]).IsMatch((string)e.DataObject.GetData(typeof(string)))) e.CancelCommand();
        }
    }
}
