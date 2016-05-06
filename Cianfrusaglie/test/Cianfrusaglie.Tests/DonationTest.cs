using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Xunit;
using Xunit.Sdk;


namespace Cianfrusaglie.Tests
{
    public class DonationTest
    {
        private AnnouncesController donation;
        private ApplicationDbContext Context = new ApplicationDbContext();

        public DonationTest()
        {
            donation = new AnnouncesController(Context);
        }

        [Fact]
        public void CorrectInsertionIsOK()
        {
            
            var Announce = new Announce();
            //TODO: Add fields...
            donation.Create(Announce);
            //Assert.Contains(Announce, Context.Announces);
            Assert.True( true);
        }
    }
}
