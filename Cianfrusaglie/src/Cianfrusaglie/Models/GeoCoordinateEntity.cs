using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
   [ComplexType]
   public class GeoCoordinateEntity {
      public int Id { get; set; }
      public double Latitude { get; set; }
      public double Longitude { get; set; }

      public GeoCoordinateEntity() { }

      public GeoCoordinateEntity( double latitude, double longitude ) {
         Latitude = latitude;
         Longitude = longitude;
      }

      public double Distance( GeoCoordinateEntity gc ) {
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