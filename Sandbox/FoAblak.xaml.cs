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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RelaxUI;

namespace Sandbox
{
    /// <summary>
    /// Interaction logic for FoAblak.xaml
    /// </summary>
    public partial class FoAblak : Window, INotifyPropertyChanged
    {
        public FoAblak()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private Parancs uzenetdobozparancs = null;
        public Parancs UzenetDobozParancs => uzenetdobozparancs ??= new Parancs(a => UzenetDoboz.Megjelenit("Teszt üzenet.", _ikon: UzenetDobozIkon.Informacio));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string _propnev = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propnev));
    }
}
