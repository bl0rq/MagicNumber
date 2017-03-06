using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core
{
    public class SetBrushConverter : System.Windows.FrameworkElement, System.Windows.Data.IValueConverter
    {
        public object Convert ( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            var set = value as ViewModel.Set;
            if ( set != null )
            {
                var dp = ms_tileBrushProperties [ Math.Abs ( set.Model.Id.GetHashCode ( ) % ms_tileBrushProperties.Length ) ];
                return GetValue ( dp );
            }
            return null;
        }

        public object ConvertBack ( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            throw new NotImplementedException ( );
        }


        //This is a bit of a mess...
        private static System.Windows.DependencyProperty [] ms_tileBrushProperties;
        static SetBrushConverter ( )
        {
            ms_tileBrushProperties = new []
            {
                TileBrush1Property, TileBrush2Property, TileBrush3Property,
                TileBrush4Property, TileBrush5Property, TileBrush6Property,
                TileBrush7Property, TileBrush8Property, TileBrush9Property,
                TileBrush10Property, TileBrush11Property, TileBrush12Property,
            };
        }

        public System.Windows.Media.Brush TileBrush1
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush1Property ); }
            set { SetValue ( TileBrush1Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush1Property =
            System.Windows.DependencyProperty.Register ( "TileBrush1", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush2
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush2Property ); }
            set { SetValue ( TileBrush2Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush2Property =
            System.Windows.DependencyProperty.Register ( "TileBrush2", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush3
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush3Property ); }
            set { SetValue ( TileBrush3Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush3Property =
            System.Windows.DependencyProperty.Register ( "TileBrush3", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush4
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush4Property ); }
            set { SetValue ( TileBrush4Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush4Property =
            System.Windows.DependencyProperty.Register ( "TileBrush4", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush5
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush5Property ); }
            set { SetValue ( TileBrush5Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush5Property =
            System.Windows.DependencyProperty.Register ( "TileBrush5", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush6
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush6Property ); }
            set { SetValue ( TileBrush6Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush6Property =
            System.Windows.DependencyProperty.Register ( "TileBrush6", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush7
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush7Property ); }
            set { SetValue ( TileBrush7Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush7Property =
            System.Windows.DependencyProperty.Register ( "TileBrush7", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush8
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush8Property ); }
            set { SetValue ( TileBrush8Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush8Property =
            System.Windows.DependencyProperty.Register ( "TileBrush8", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush9
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush9Property ); }
            set { SetValue ( TileBrush9Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush9Property =
            System.Windows.DependencyProperty.Register ( "TileBrush9", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush10
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush10Property ); }
            set { SetValue ( TileBrush10Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush10Property =
            System.Windows.DependencyProperty.Register ( "TileBrush10", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush11
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush11Property ); }
            set { SetValue ( TileBrush11Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush11Property =
            System.Windows.DependencyProperty.Register ( "TileBrush11", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );


        public System.Windows.Media.Brush TileBrush12
        {
            get { return (System.Windows.Media.Brush)GetValue ( TileBrush12Property ); }
            set { SetValue ( TileBrush12Property, value ); }
        }
        public static readonly System.Windows.DependencyProperty TileBrush12Property =
            System.Windows.DependencyProperty.Register ( "TileBrush12", typeof ( System.Windows.Media.Brush ), typeof ( SetBrushConverter ), new System.Windows.PropertyMetadata ( null ) );

    }
}
