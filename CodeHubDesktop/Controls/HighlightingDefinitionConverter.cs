using CodeHubDesktop.Models;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CodeHubDesktop.Controls
{
    public class HighlightingDefinitionConverter : IValueConverter
    {
        private static readonly HighlightingDefinitionTypeConverter Converter = new HighlightingDefinitionTypeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                LanguageModel code = value as LanguageModel;
                if (code != null)
                {
                    return Converter.ConvertFrom(code.DisplayName);
                }
                else
                {
                    return Converter.ConvertFrom("C#");
                }
            }
            else
            {
                return Converter.ConvertFrom("C#");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Converter.ConvertToString(value);
        }
    }
}
