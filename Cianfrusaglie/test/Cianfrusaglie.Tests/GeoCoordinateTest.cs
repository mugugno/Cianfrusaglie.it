using System;
using Cianfrusaglie.Models;
using Xunit;

namespace Cianfrusaglie.Tests {
   public class GeoCoordinateTest {
      [Fact]
      public void DistanceThrowsArgumentNull() { 
         var gc= new GeoCoordinateEntity();
         Assert.Throws< ArgumentNullException >( () => gc.Distance( null ) );
      }

      [Theory, InlineData(0,0), InlineData(1.3,1.3), InlineData(-7.6,-7.6)]
      public void DistanceSelfIsZero(double latitude, double longitude) {
         var gc = new GeoCoordinateEntity();
         Assert.Throws<ArgumentNullException>( () => gc.Distance( null ) );
      }
   }
}