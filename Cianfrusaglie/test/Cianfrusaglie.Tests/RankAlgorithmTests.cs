using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Models;
using Cianfrusaglie.Suggestions;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class RankAlgorithmTests : BaseTestSetup
    {
        public RankAlgorithmTests() {
            
        }

        //[Theory]
        //[InlineData()]
        //public void CalculateDistanceScoreTestWithCorrectParameters(string username, int announceId, int rank) {
        //    var user = Context.Users.Single( u => u.UserName.Equals( username ) );
        //    var announce = Context.Announces.Single( a => a.Id.Equals( announceId ) );
        //    var rankAlgorithm = new RankAlgorithm(Context);
        //    int result = rankAlgorithm.CalculateDistanceScore( announce, user );
        //    Assert.Equal( result,rank );
        //}
    }
}
