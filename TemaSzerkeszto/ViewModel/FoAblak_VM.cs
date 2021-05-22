using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RelaxUI;

namespace TemaSzerkeszto.ViewModel
{
    public class FoAblak_VM : BazisViewModel
    {
        public FoAblak_VM()
        {
            Temak.Egyke.AktivTemaValtozott += at => KivalasztottTema = at;
        }

        private Tema kivalasztotttema = Temak.Egyke.TemaLista[0];
        public Tema KivalasztottTema
        {
            get => kivalasztotttema;
            set { if (kivalasztotttema != value) { kivalasztotttema = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); } }
        }

        private Parancs klonozparancs = null;
        public Parancs KlonozParancs => klonozparancs ??= new Parancs(a =>
        {
            Tema masolt = KivalasztottTema.Clone() as Tema;

            masolt.Megnevezes = KivalasztottTema.Megnevezes + " (" + (Temak.Egyke.TemaLista.Count(t => t.Megnevezes.Contains(masolt.Megnevezes)) + 1).ToString() + ')';

            Temak.Egyke.TemaLista.Add(masolt);
            Temak.Egyke.Beallit(masolt.Megnevezes);
        }, p => KivalasztottTema != null);

        private Parancs torolparancs = null;
        public Parancs TorolParancs => torolparancs ??= new Parancs(a =>
        {
            if (Temak.Egyke.AktivTema.Megnevezes.Equals("Alapértelmezett"))
            { MessageBox.Show("Az alapértelmezett téma nem törölhető!", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information); return; }

            Temak.Egyke.TemaLista.Remove(KivalasztottTema);

            Temak.Egyke.AktivTema = Temak.Egyke.TemaLista[0];
        }, p => KivalasztottTema != null);

        private Parancs mentesparancs = null;
        public Parancs MentesParancs => mentesparancs ??= new Parancs(a =>
        {
            try { Temak.Egyke.Elment(); }
            catch (Exception exc) { MessageBox.Show("A mentés sikertelen!\n" + exc.Message, "Mentés", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            MessageBox.Show("A mentés sikeres!", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
        });
    }
}
