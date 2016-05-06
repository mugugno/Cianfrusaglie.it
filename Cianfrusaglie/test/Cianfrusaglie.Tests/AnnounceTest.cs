using System.Linq;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Xunit;


namespace Cianfrusaglie.Tests
{
    public class AnnounceTest : BaseTestSetup
    {

        //private AnnouncesController donation;
        //private ApplicationDbContext Context = new ApplicationDbContext();

        //public DonationTest()
        //{
        //    donation = new AnnouncesController(Context);
        //}

        [Fact]
        public void CorrectInsertionIsOK()
        {
            var announce = new Announce();
            var usr = _context.Users.First();
            announce.Author = usr;
            announce.Title = "Un annuncio bello bello";
            announce.Description = "Sono bello";
            

            ////TODO: Add fields...
            //donation.Create(Announce);
            ////Assert.Contains(Announce, Context.Announces);
            //Assert.True(true);
        }
    }
}
