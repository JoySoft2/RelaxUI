using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RelaxUI
{
    public class Tema
    {
        public string Megnevezes                  { get; set; }
        public Color TemaSzin                     { get; set; }
        public Color AblakHatterSzin              { get; set; }
        public Color PanelSzin                    { get; set; }
        public Color SzovegSzin                   { get; set; }
        public Color SzovegLetiltottSzin          { get; set; }
        public Color VezerloHatterSzin            { get; set; }
        public Color VezerloKeretSzin             { get; set; }
        public Color VezerloHatterEgerFeletteSzin { get; set; }
        public Color VezerloKeretEgerFeletteSzin  { get; set; }
        public Color VezerloHatterAkcioSzin       { get; set; }
        public Color VezerloKeretAkcioSzin        { get; set; }
        public Color VezerloHatterFokuszSzin      { get; set; }
        public Color VezerloKeretFokuszSzin       { get; set; }
        public Color VezerloHatterLetiltottSzin   { get; set; }
        public Color VezerloKeretLetiltottSzin    { get; set; }

        public override string ToString() => Megnevezes;
    }
}
