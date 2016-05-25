using System;
using Cianfrusaglie.GeoPosition;
using Cianfrusaglie.Models;
using Xunit;

namespace Cianfrusaglie.Tests {
   public class GeoCoordinateTest {
      [Fact]
      public void DistanceThrowsArgumentNull() {
         var gc = new GeoCoordinate();
         Assert.Throws< ArgumentNullException >( () => gc.Distance( null ) );
      }

      [Theory, InlineData( 0, 0 ), InlineData( 1.3, 1.3 ), InlineData( -7.6, -6.7 ), InlineData( 6.9, -3 )]
      public void DistanceSelfIsZero( double latitude, double longitude ) {
         var gc = new GeoCoordinate(latitude, longitude);
         Assert.True( gc.Distance( gc ) == 0 );
      }

      [Theory, InlineData( 44.3347675, 8.511976300000015, 44.3228049, 8.466561899999988, 3.849 ),
         InlineData( 44.40721300000001, 8.94755299999997, 44.3425496, 8.42938909999998, 41.81 )]
      public void DistanceIsOk( double latitude1, double longitude1, double latitude2, double longitude2, double distance ) {
         var calcDist = GeoCoordinate.Distance( latitude1, longitude1, latitude2, longitude2 );
         Assert.True( calcDist <= distance + 0.25 && calcDist >= distance - 0.25 );
      }
   }
}