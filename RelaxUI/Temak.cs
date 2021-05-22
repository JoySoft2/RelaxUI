using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

        public delegate void AktivTemaValtozottKezelo(Tema _aktivtema);
        public event AktivTemaValtozottKezelo AktivTemaValtozott = null;

        public ObservableCollection<Tema> TemaLista { get; set; } = null;

        private Tema aktivtema = null;
        public Tema AktivTema
        {
            get => aktivtema;
            set
            {
                if (aktivtema != value)
                {
                    aktivtema = value;
                    
                    OnPropertyChanged();

                    AktivTemaValtozott?.Invoke(value);
                }
            }
        }

        public void Betolt()
        {
            bool fajlhiba = false;
            TemaLista = new();

            TemaLista.Add(new Tema()
            {
                Megnevezes                   = "Alapértelmezett",
                TemaSzin                     = ColorFromARGBHexText("FF00BFFF"),
                StatuszAlapSzin              = ColorFromARGBHexText("FF000000"),
                StatuszOkSzin                = ColorFromARGBHexText("FF32CD32"),
                StatuszInformacioSzin        = ColorFromARGBHexText("FF0076FF"),
                StatuszFigyelmeztetesSzin    = ColorFromARGBHexText("FFFFAF00"),
                StatuszHibaSzin              = ColorFromARGBHexText("FFFF0000"),
                AblakHatterSzin              = ColorFromARGBHexText("FFF0F8FF"),
                PanelSzin                    = ColorFromARGBHexText("FFEAEAEA"),
                SzovegSzin                   = ColorFromARGBHexText("FF000000"),
                SzovegLetiltottSzin          = ColorFromARGBHexText("FFA6A6A6"),
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
                string colorattrname = "ARGB";
                XmlNodeList xmltemalista = xmldoc.SelectNodes("/TemaLista/Tema");

                foreach (XmlNode node in xmltemalista)
                {
                    Tema tema = new();

                    tema.Megnevezes                   = node.Attributes["Megnevezes"].Value;
                    tema.TemaSzin                     = ColorFromARGBHexText(node["TemaSzin"                    ].Attributes[colorattrname].Value);
                    tema.StatuszAlapSzin              = ColorFromARGBHexText(node["StatuszAlapSzin"             ].Attributes[colorattrname].Value);
                    tema.StatuszOkSzin                = ColorFromARGBHexText(node["StatuszOkSzin"               ].Attributes[colorattrname].Value);
                    tema.StatuszInformacioSzin        = ColorFromARGBHexText(node["StatuszInformacioSzin"       ].Attributes[colorattrname].Value);
                    tema.StatuszFigyelmeztetesSzin    = ColorFromARGBHexText(node["StatuszFigyelmeztetesSzin"   ].Attributes[colorattrname].Value);
                    tema.StatuszHibaSzin              = ColorFromARGBHexText(node["StatuszHibaSzin"             ].Attributes[colorattrname].Value);
                    tema.AblakHatterSzin              = ColorFromARGBHexText(node["AblakHatterSzin"             ].Attributes[colorattrname].Value);
                    tema.PanelSzin                    = ColorFromARGBHexText(node["PanelSzin"                   ].Attributes[colorattrname].Value);
                    tema.SzovegSzin                   = ColorFromARGBHexText(node["SzovegSzin"                  ].Attributes[colorattrname].Value);
                    tema.SzovegLetiltottSzin          = ColorFromARGBHexText(node["SzovegLetiltottSzin"         ].Attributes[colorattrname].Value);
                    tema.VezerloHatterSzin            = ColorFromARGBHexText(node["VezerloHatterSzin"           ].Attributes[colorattrname].Value);
                    tema.VezerloKeretSzin             = ColorFromARGBHexText(node["VezerloKeretSzin"            ].Attributes[colorattrname].Value);
                    tema.VezerloHatterEgerFeletteSzin = ColorFromARGBHexText(node["VezerloHatterEgerFeletteSzin"].Attributes[colorattrname].Value);
                    tema.VezerloKeretEgerFeletteSzin  = ColorFromARGBHexText(node["VezerloKeretEgerFeletteSzin" ].Attributes[colorattrname].Value);
                    tema.VezerloHatterAkcioSzin       = ColorFromARGBHexText(node["VezerloHatterAkcioSzin"      ].Attributes[colorattrname].Value);
                    tema.VezerloKeretAkcioSzin        = ColorFromARGBHexText(node["VezerloKeretAkcioSzin"       ].Attributes[colorattrname].Value);
                    tema.VezerloHatterFokuszSzin      = ColorFromARGBHexText(node["VezerloHatterFokuszSzin"     ].Attributes[colorattrname].Value);
                    tema.VezerloKeretFokuszSzin       = ColorFromARGBHexText(node["VezerloKeretFokuszSzin"      ].Attributes[colorattrname].Value);
                    tema.VezerloHatterLetiltottSzin   = ColorFromARGBHexText(node["VezerloHatterLetiltottSzin"  ].Attributes[colorattrname].Value);
                    tema.VezerloKeretLetiltottSzin    = ColorFromARGBHexText(node["VezerloKeretLetiltottSzin"   ].Attributes[colorattrname].Value);

                    TemaLista.Add(tema);
                }
            }

            AktivTema = TemaLista[0];
        }

        public void Elment()
        {
            XmlDocument xmldoc = new() {  };

            XmlNode temalistanode = xmldoc.CreateNode(XmlNodeType.Element, "TemaLista", null);
            xmldoc.AppendChild(temalistanode);

            foreach (Tema tema in TemaLista)
            {
                if (tema.Megnevezes.Equals("Alapértelmezett")) continue;

                XmlNode temanode = xmldoc.CreateNode(XmlNodeType.Element, "Tema", null);

                (temanode as XmlElement).SetAttribute("Megnevezes", tema.Megnevezes);

                XmlNode tne;
                string colorattrname = "ARGB";

                tne = xmldoc.CreateNode(XmlNodeType.Element, "TemaSzin"                    , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.TemaSzin                    )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "StatuszAlapSzin"             , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.StatuszAlapSzin             )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "StatuszOkSzin"               , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.StatuszOkSzin               )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "StatuszInformacioSzin"       , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.StatuszInformacioSzin       )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "StatuszFigyelmeztetesSzin"   , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.StatuszFigyelmeztetesSzin   )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "StatuszHibaSzin"             , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.StatuszHibaSzin             )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "AblakHatterSzin"             , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.AblakHatterSzin             )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "PanelSzin"                   , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.PanelSzin                   )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "SzovegSzin"                  , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.SzovegSzin                  )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "SzovegLetiltottSzin"         , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.SzovegLetiltottSzin         )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloHatterSzin"           , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloHatterSzin           )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloKeretSzin"            , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloKeretSzin            )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloHatterEgerFeletteSzin", null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloHatterEgerFeletteSzin)); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloKeretEgerFeletteSzin" , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloKeretEgerFeletteSzin )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloHatterAkcioSzin"      , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloHatterAkcioSzin      )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloKeretAkcioSzin"       , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloKeretAkcioSzin       )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloHatterFokuszSzin"     , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloHatterFokuszSzin     )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloKeretFokuszSzin"      , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloKeretFokuszSzin      )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloHatterLetiltottSzin"  , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloHatterLetiltottSzin  )); temanode.AppendChild(tne);
                tne = xmldoc.CreateNode(XmlNodeType.Element, "VezerloKeretLetiltottSzin"   , null); (tne as XmlElement).SetAttribute(colorattrname, ColorToARGBHexText(tema.VezerloKeretLetiltottSzin   )); temanode.AppendChild(tne);

                temalistanode.AppendChild(temanode);
            }

            xmldoc.PreserveWhitespace = false;

            using (TextWriter tw = new StreamWriter("temak.xml", false, Encoding.UTF8))
                xmldoc.Save(tw);
        }

        public void Beallit(string _temamegnevezes)
        {
            Tema ujtema = TemaLista.SingleOrDefault(t => string.Equals(t.Megnevezes, _temamegnevezes));

            if (ujtema != default)
                this.AktivTema = ujtema;
        }

        private static Color ColorFromARGBHexText(string _argbhextext) =>
            Color.FromArgb(Convert.ToByte(_argbhextext[0..2], 16),
                           Convert.ToByte(_argbhextext[2..4], 16),
                           Convert.ToByte(_argbhextext[4..6], 16),
                           Convert.ToByte(_argbhextext[6..8], 16));

        private static string ColorToARGBHexText(Color _color) =>
            $"{_color.A:X2}{_color.R:X2}{_color.G:X2}{_color.B:X2}";

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string _propnev = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propnev));
    }
}
