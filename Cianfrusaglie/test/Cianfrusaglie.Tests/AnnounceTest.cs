using System.Linq;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Xunit;


namespace Cianfrusaglie.Tests
{
    public class AnnounceTest : BaseTestSetup
    {

        private AnnouncesController _donation;
        private AccountController _accountController;
        //private ApplicationDbContext Context = new ApplicationDbContext();

        public AnnounceTest()
        {
            _donation = new AnnouncesController(Context);
        }

        [Fact]
        public void CorrectInsertionIsOk()
        {
            var announce = new Announce();
            var usr = Context.Users.First();
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
