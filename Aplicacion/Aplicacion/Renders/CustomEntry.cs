    using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

namespace App.Renderes
{
    public class CustomEntry : Entry
    {

        static readonly int defaultBorderValue = 10;
        static readonly double defaulCornerValue = 20;

        public static readonly BindableProperty BorderColorProperty =
         BindableProperty.Create(
             nameof(BorderColor),
             typeof(Color),
             typeof(CustomEntry),
             Color.FromHex("#707070")
             );

        public static readonly BindableProperty BorderWithProperty =
       BindableProperty.Create(
           nameof(BorderWith),
           typeof(int),
           typeof(CustomEntry),
           defaultBorderValue
           );

        public static readonly BindableProperty CornerRadiusProperty =
       BindableProperty.Create(
           nameof(CornerRadius),
           typeof(double),
           typeof(CustomEntry),
           defaulCornerValue
           );

        public static readonly BindableProperty BackgroundColorProperty =
       BindableProperty.Create(
           nameof(BackgroundColor),
           typeof(Color),
           typeof(CustomEntry),
           Color.FromHex("#D1DFE1")
           );


        public Color BorderColor
        {
            get=> (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public int BorderWith
        {
            get => (int)GetValue(BorderWithProperty);
            set => SetValue(BorderWithProperty, value);
        }

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }



    }
}
