using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App.Renderes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using App.Droid.Renderes;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace App.Droid.Renderes
{
    internal class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (CustomEntry)Element;
                if (Control != null && this.Element != null)
                {
                    DrawControl(view);
                    if (e.NewElement != e.OldElement)
                    {
                        if (e.OldElement != null)
                        {
                            e.OldElement.PropertyChanged -= ElementPropertyChanged;
                        }

                        if (e.NewElement != null)
                        {
                            e.NewElement.PropertyChanged += ElementPropertyChanged;
                        }
                    }
                }
            }
        }

        private new void ElementPropertyChanged(Object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);
            var view = sender as CustomEntry;
            if (args.PropertyName == CustomEntry.BorderColorProperty.PropertyName)
            {
                DrawControl(view);
            }
        }

        private void DrawControl(CustomEntry view)
        {
            //CREAR COLOR DE FONDO
            var _background = new GradientDrawable();
            _background.SetShape(ShapeType.Rectangle);
            _background.SetColor(view.BackgroundColor.ToAndroid());

            //CREAR ANCHO Y COLOR DEL CONTORNO
            _background.SetStroke(view.BorderWith, view.BorderColor.ToAndroid());

            //CREAR RADIO DEL CONTORNO
            _background.SetCornerRadius(Convert.ToSingle(view.CornerRadius));

            Control.Background = _background;

            Control.Gravity = GravityFlags.CenterHorizontal;

            Control.SetCursorVisible(true);


        }
    }
}