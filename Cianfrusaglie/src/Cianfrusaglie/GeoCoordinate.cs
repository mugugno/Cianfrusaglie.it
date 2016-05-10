using System;

namespace Cianfrusaglie {
   public class GeoCoordinate {
      public GeoCoordinate() { }

      public GeoCoordinate( double latitude, double longitude ) {
         Latitude = latitude;
         Longitude = longitude;
      }

      public double Latitude { get; set; }
      public double Longitude { get; set; }

      public static double Distance( double Lat1, double Long1, double Lat2, double Long2 ) {
         return new GeoCoordinate( Lat1, Lat2 ).Distance( new GeoCoordinate( Lat2, Long2 ) );
      }

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
         return 6371 * c;
      }
   }
}