using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System;

namespace MilitariaPytanieRekrutacyjne2
{
    /// <summary>
    /// Klasa LevelToColorConverter implementuje interfejs IValueConverter i konwertuje wiersz na odpowiedni kolor.
    /// </summary>
    public class LevelToColorConverter : IValueConverter
    {
        /// <summary>
        /// Konwertuje bazowy kolor wiersza na odpowiedni.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string levelName = value as string;
            switch (levelName)
            {
                case "Temat":
                    return Brushes.Green;
                case "Zakres informacyjny":
                    return Brushes.Red;
                case "Dziedzina":
                    return Brushes.Yellow;
                default:
                    return Brushes.Transparent;
            }
        }

        /// <summary>
        /// Metoda konwertująca wartość z powrotem. Nie jest zaimplementowana.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}