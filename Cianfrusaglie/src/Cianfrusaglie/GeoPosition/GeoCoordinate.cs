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
            return new GeoCoordinate( lat1, lat2 ).Distance( new GeoCoordinate( lat2, long2 ) );
        }

        /// <summary>
        /// Dato un punto GPS, ritorna la distanza in KM.
        /// </summary>
        /// <param name="gc">Il punto GPS</param>
        /// <returns>La distanza in KM</returns>
        public double Distance( GeoCoordinate gc ) {
            if( gc == null )
                throw new ArgumentNullException();

            const double toRad = Math.PI / 180;

            double dLat = ( gc.Latitude - Latitude ) * toRad;
            double dLon = ( gc.Longitude - Longitude ) * toRad;

            double lat1 = Latitude * toRad;
            double lat2 = gc.Latitude * toRad;

            double a = Math.Pow( Math.Sin( dLat / 2 ), 2 ) +
                       Math.Pow( Math.Sin( dLon / 2 ), 2 ) * Math.Cos( lat1 ) * Math.Cos( lat2 );
            double c = 2 * Math.Atan2( Math.Sqrt( a ), Math.Sqrt( 1 - a ) );
            return 6.371 * c;
        }
    }
}