﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DeathBringer.Wpf.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Se il valore è nullo, eccezione
            if (value == null)
                throw new InvalidOperationException("I valori nulli non sono ammessi");

            //Il valore deve booleano
            if (value.GetType() != typeof(bool))
                throw new InvalidOperationException("Il tipo non è boolean!");

            //Sono sicuro che è boolean quindi faccio il cast
            bool castedValue = (bool)value;

            //Se è vero, Visible, altrimenti Collassato
            return castedValue == true 
                ? Visibility.Visible 
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
