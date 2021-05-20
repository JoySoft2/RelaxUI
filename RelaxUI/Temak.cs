using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace RelaxUI
{
    public sealed class Temak : INotifyPropertyChanged
    {
        private static readonly Temak peldany = new();

        private Temak() { }

        static Temak() { }

        public static Temak Egyke => peldany;

        public List<Tema> TemaLista { get; set; } = null;

        private Tema aktivtema = null;
        public Tema AktivTema
        {
            get => aktivtema;
            set { if (aktivtema != value) { aktivtema = value; OnPropertyChanged(); } }
        }

        public void Inicializal()
        {
            bool fajlhiba = false;
            TemaLista = new();

            TemaLista.Add(new Tema()
            {
                Megnevezes                   = "Alapértelmezett",
                TemaSzin                     = ColorFromARGBHexText("FF672878"),
                StatuszAlapSzin              = ColorFromARGBHexText("FFEFEFEF"),
                StatuszOkSzin                = ColorFromARGBHexText("FF00AF00"),
                StatuszInformacioSzin        = ColorFromARGBHexText("FF0000AF"),
                StatuszFigyelmeztetesSzin    = ColorFromARGBHexText("FFFFAF00"),
                StatuszHibaSzin              = ColorFromARGBHexText("FFFF0000"),
                AblakHatterSzin              = ColorFromARGBHexText("FF128345"),
                PanelSzin                    = ColorFromARGBHexText("FF202080"),
                SzovegSzin                   = ColorFromARGBHexText("FF913654"),
                SzovegLetiltottSzin          = ColorFromARGBHexText("FF7F7F7F"),
                VezerloHatterSzin            = ColorFromARGBHexText("FF282828"),
                VezerloKeretSzin             = ColorFromARGBHexText("FF323232"),
                VezerloHatterEgerFeletteSzin = ColorFromARGBHexText("FF383838"),
                VezerloKeretEgerFeletteSzin  = ColorFromARGBHexText("FF424242"),
                VezerloHatterAkcioSzin       = ColorFromARGBHexText("FF585858"),
                VezerloKeretAkcioSzin        = ColorFromARGBHexText("FF626262"),
                VezerloHatterFokuszSzin      = ColorFromARGBHexText("FF383838"),
                VezerloKeretFokuszSzin       = ColorFromARGBHexText("FF424242"),
                VezerloHatterLetiltottSzin   = ColorFromARGBHexText("FF181818"),
                VezerloKeretLetiltottSzin    = ColorFromARGBHexText("FF222222")
            });

            XmlDocument xmldoc = new();

            try { xmldoc.Load("temak.xml"); }
            catch { fajlhiba = true; }

            if (!fajlhiba)
            {
                XmlNodeList xmltemalista = xmldoc.SelectNodes("/TemaLista/Tema");

                foreach (XmlNode node in xmltemalista)
                {
                    Tema tema = new();

                    tema.Megnevezes                   = node.Attributes["Megnevezes"].Value;
                    tema.TemaSzin                     = ColorFromARGBHexText(node["TemaSzin"                    ].Attributes["ARGB"].Value);
                    tema.StatuszAlapSzin              = ColorFromARGBHexText(node["StatuszAlapSzin"             ].Attributes["ARGB"].Value);
                    tema.StatuszOkSzin                = ColorFromARGBHexText(node["StatuszOkSzin"               ].Attributes["ARGB"].Value);
                    tema.StatuszInformacioSzin        = ColorFromARGBHexText(node["StatuszInformacioSzin"       ].Attributes["ARGB"].Value);
                    tema.StatuszFigyelmeztetesSzin    = ColorFromARGBHexText(node["StatuszFigyelmeztetesSzin"   ].Attributes["ARGB"].Value);
                    tema.StatuszHibaSzin              = ColorFromARGBHexText(node["StatuszHibaSzin"             ].Attributes["ARGB"].Value);
                    tema.AblakHatterSzin              = ColorFromARGBHexText(node["AblakHatterSzin"             ].Attributes["ARGB"].Value);
                    tema.PanelSzin                    = ColorFromARGBHexText(node["PanelSzin"                   ].Attributes["ARGB"].Value);
                    tema.SzovegSzin                   = ColorFromARGBHexText(node["SzovegSzin"                  ].Attributes["ARGB"].Value);
                    tema.SzovegLetiltottSzin          = ColorFromARGBHexText(node["SzovegLetiltottSzin"         ].Attributes["ARGB"].Value);
                    tema.VezerloHatterSzin            = ColorFromARGBHexText(node["VezerloHatterSzin"           ].Attributes["ARGB"].Value);
                    tema.VezerloKeretSzin             = ColorFromARGBHexText(node["VezerloKeretSzin"            ].Attributes["ARGB"].Value);
                    tema.VezerloHatterEgerFeletteSzin = ColorFromARGBHexText(node["VezerloHatterEgerFeletteSzin"].Attributes["ARGB"].Value);
                    tema.VezerloKeretEgerFeletteSzin  = ColorFromARGBHexText(node["VezerloKeretEgerFeletteSzin" ].Attributes["ARGB"].Value);
                    tema.VezerloHatterAkcioSzin       = ColorFromARGBHexText(node["VezerloHatterAkcioSzin"      ].Attributes["ARGB"].Value);
                    tema.VezerloKeretAkcioSzin        = ColorFromARGBHexText(node["VezerloKeretAkcioSzin"       ].Attributes["ARGB"].Value);
                    tema.VezerloHatterFokuszSzin      = ColorFromARGBHexText(node["VezerloHatterFokuszSzin"     ].Attributes["ARGB"].Value);
                    tema.VezerloKeretFokuszSzin       = ColorFromARGBHexText(node["VezerloKeretFokuszSzin"      ].Attributes["ARGB"].Value);
                    tema.VezerloHatterLetiltottSzin   = ColorFromARGBHexText(node["VezerloHatterLetiltottSzin"  ].Attributes["ARGB"].Value);
                    tema.VezerloKeretLetiltottSzin    = ColorFromARGBHexText(node["VezerloKeretLetiltottSzin"   ].Attributes["ARGB"].Value);

                    TemaLista.Add(tema);
                }
            }

            AktivTema = TemaLista[0];
        }

        public void Beallit(string _temamegnevezes)
        {
            Tema ujtema = TemaLista.SingleOrDefault(t => string.Equals(t.Megnevezes, _temamegnevezes));

            if (ujtema != null)
                this.AktivTema = ujtema;
        }

        private static Color ColorFromARGBHexText(string _argbhextext) =>
            Color.FromArgb(Convert.ToByte(_argbhextext[0..2], 16),
                           Convert.ToByte(_argbhextext[2..4], 16),
                           Convert.ToByte(_argbhextext[4..6], 16),
                           Convert.ToByte(_argbhextext[6..8], 16));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string _propnev = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propnev));
    }
}
