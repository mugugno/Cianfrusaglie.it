using System;

namespace Cianfrusaglie.GeoPosition {
    public class GeoCoordinate {
        public GeoCoordinate() { }

        public GeoCoordinate( double latitude, double longitude ) {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /// <summary>
        /// Dati latititudine e longitudine di due punti, calcola la distanza tra punti GPS.
        /// </summary>
        /// <param name="lat1">Latitudine del primo punto</param>
        /// <param name="long1">Longitudine del primo punto</param>
        /// <param name="lat2">Latitudine del secondo punto</param>
        /// <param name="long2">Longitudine del secondo punto</param>
        /// <returns></returns>
        public static double Distance( double lat1, double long1, double lat2, double long2 ) {
            return new GeoCoordinate( lat1, long1 ).Distance( new GeoCoordinate( lat2, long2 ) );
        }

        /// <summary>
        /// Dato un punto GPS, ritorna la distanza in KM.
        /// </summary>
        /// <param name="gc">Il punto GPS</param>
        /// <returns>La distanza in KM</returns>
        public double Distance( GeoCoordinate other ) {
            if( other == null )
                throw new ArgumentNullException();

         const double toRad = Math.PI / 180.0;
         var d1 = Latitude * toRad;
         var num1 = Longitude * toRad;
         var d2 = other.Latitude * toRad;
         var num2 = other.Longitude * toRad - num1;
         var d3 = Math.Pow( Math.Sin( ( d2 - d1 ) / 2.0 ), 2.0 ) +
                  Math.Cos( d1 ) * Math.Cos( d2 ) * Math.Pow( Math.Sin( num2 / 2.0 ), 2.0 );
         return 6371 * ( 2.0 * Math.Atan2( Math.Sqrt( d3 ), Math.Sqrt( 1.0 - d3 ) ) );
      }
    }
}