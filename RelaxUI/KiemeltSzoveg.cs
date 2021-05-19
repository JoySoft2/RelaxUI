using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace RelaxUI
{
    public class KiemeltSzoveg
    {
        #region Bold
        public static bool GetFelkover(DependencyObject obj) => (bool)obj.GetValue(FelkoverProperty);

        public static void SetFelkover(DependencyObject obj, bool value) => obj.SetValue(FelkoverProperty, value);

        public static readonly DependencyProperty FelkoverProperty =
            DependencyProperty.RegisterAttached("Felkover", typeof(bool), typeof(KiemeltSzoveg), new PropertyMetadata(false, Refresh));
        #endregion

        #region Italic
        public static bool GetDolt(DependencyObject obj) => (bool)obj.GetValue(DoltProperty);

        public static void SetDolt(DependencyObject obj, bool value) => obj.SetValue(DoltProperty, value);

        public static readonly DependencyProperty DoltProperty =
            DependencyProperty.RegisterAttached("Dolt", typeof(bool), typeof(KiemeltSzoveg), new PropertyMetadata(false, Refresh));
        #endregion

        #region Underline
        public static bool GetAlahuzott(DependencyObject obj) => (bool)obj.GetValue(AlahuzottProperty);

        public static void SetAlahuzott(DependencyObject obj, bool value) => obj.SetValue(AlahuzottProperty, value);

        public static readonly DependencyProperty AlahuzottProperty =
            DependencyProperty.RegisterAttached("Alahuzott", typeof(bool), typeof(KiemeltSzoveg), new PropertyMetadata(false, Refresh));
        #endregion

        #region HighlightTextBrush
        public static Brush GetSzovegKiemeloEcset(DependencyObject obj) => (Brush)obj.GetValue(SzovegKiemeloEcsetProperty);

        public static void SetSzovegKiemeloEcset(DependencyObject obj, Brush value) => obj.SetValue(SzovegKiemeloEcsetProperty, value);

        public static readonly DependencyProperty SzovegKiemeloEcsetProperty =
            DependencyProperty.RegisterAttached("SzovegKiemeloEcset", typeof(Brush), typeof(KiemeltSzoveg), new PropertyMetadata(SystemColors.HighlightTextBrush, Refresh));
        #endregion

        #region HighlightBrush
        public static Brush GetKiemeloEcset(DependencyObject obj) => (Brush)obj.GetValue(KiemeloEcsetProperty);

        public static void SetKiemeloEcset(DependencyObject obj, Brush value) => obj.SetValue(KiemeloEcsetProperty, value);

        public static readonly DependencyProperty KiemeloEcsetProperty =
            DependencyProperty.RegisterAttached("KiemeloEcset", typeof(Brush), typeof(KiemeltSzoveg), new PropertyMetadata(SystemColors.HighlightBrush, Refresh));
        #endregion

        #region HighlightText
        public static string GetKiemeltSzoveg(DependencyObject obj) => (string)obj.GetValue(KiemeltSzovegProperty);

        public static void SetKiemeltSzoveg(DependencyObject obj, string value) => obj.SetValue(KiemeltSzovegProperty, value);

        public static readonly DependencyProperty KiemeltSzovegProperty =
            DependencyProperty.RegisterAttached("KiemeltSzoveg", typeof(string), typeof(KiemeltSzoveg), new PropertyMetadata(string.Empty, Refresh));
        #endregion

        #region InternalText
        protected static string GetInternalText(DependencyObject obj) => (string)obj.GetValue(InternalTextProperty);

        protected static void SetInternalText(DependencyObject obj, string value) => obj.SetValue(InternalTextProperty, value);

        protected static readonly DependencyProperty InternalTextProperty =
            DependencyProperty.RegisterAttached("InternalText", typeof(string),
                typeof(KiemeltSzoveg), new PropertyMetadata(string.Empty, OnInternalTextChanged));

        private static void OnInternalTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBlock textblock)
            {
                textblock.Text = e.NewValue as string;
                Highlight(textblock);
            }
        }
        #endregion

        #region  IsBusy 
        private static bool GetIsBusy(DependencyObject obj) => (bool)obj.GetValue(IsBusyProperty);

        private static void SetIsBusy(DependencyObject obj, bool value) => obj.SetValue(IsBusyProperty, value);

        private static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.RegisterAttached("IsBusy", typeof(bool), typeof(KiemeltSzoveg), new PropertyMetadata(false));
        #endregion

        #region Methods
        private static void Refresh(DependencyObject d, DependencyPropertyChangedEventArgs e) => Highlight(d as TextBlock);

        private static void Highlight(TextBlock textblock)
        {
            if (textblock == null) return;

            string text = textblock.Text;

            if (textblock.GetBindingExpression(KiemeltSzoveg.InternalTextProperty) == null)
            {
                var textBinding = textblock.GetBindingExpression(TextBlock.TextProperty);

                if (textBinding != null)
                {
                    textblock.SetBinding(KiemeltSzoveg.InternalTextProperty, textBinding.ParentBindingBase);

                    var propertyDescriptor = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock));

                    propertyDescriptor.RemoveValueChanged(textblock, OnTextChanged);
                }
                else
                {
                    var propertyDescriptor = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock));

                    propertyDescriptor.AddValueChanged(textblock, OnTextChanged);

                    textblock.Unloaded -= Textblock_Unloaded;
                    textblock.Unloaded += Textblock_Unloaded;
                }
            }

            if (!String.IsNullOrEmpty(text))
            {
                SetIsBusy(textblock, true);

                var toHighlight = GetKiemeltSzoveg(textblock);

                if (!String.IsNullOrEmpty(toHighlight))
                {
                    var matches = Regex.Split(text, String.Format("({0})", Regex.Escape(toHighlight)), RegexOptions.IgnoreCase);

                    textblock.Inlines.Clear();

                    var highlightBrush = GetKiemeloEcset(textblock);
                    var highlightTextBrush = GetSzovegKiemeloEcset(textblock);

                    foreach (var subString in matches)
                    {
                        if (String.Compare(subString, toHighlight, true) == 0)
                        {
                            var formattedText = new Run(subString)
                            {
                                Background = highlightBrush,
                                Foreground = highlightTextBrush,
                            };

                            if (GetFelkover(textblock)) formattedText.FontWeight = FontWeights.Bold;
                            if (GetDolt(textblock)) formattedText.FontStyle = FontStyles.Italic;
                            if (GetAlahuzott(textblock)) formattedText.TextDecorations.Add(TextDecorations.Underline);

                            textblock.Inlines.Add(formattedText);
                        }
                        else textblock.Inlines.Add(subString);
                    }
                }
                else
                {
                    textblock.Inlines.Clear();
                    textblock.SetCurrentValue(TextBlock.TextProperty, text);
                }

                SetIsBusy(textblock, false);
            }
        }

        private static void Textblock_Unloaded(object sender, RoutedEventArgs e)
        {
            var propertyDescriptor = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock));

            propertyDescriptor.RemoveValueChanged(sender as TextBlock, OnTextChanged);
        }

        private static void OnTextChanged(object sender, EventArgs e)
        {
            if (sender is TextBlock textBlock && !GetIsBusy(textBlock))
                Highlight(textBlock);
        }
        #endregion
    }
}
