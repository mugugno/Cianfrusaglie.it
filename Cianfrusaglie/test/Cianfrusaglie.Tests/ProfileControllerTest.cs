using System.Linq;
using Cianfrusaglie.Controllers;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class ProfileControllerTest : BaseTestSetup
    {
        protected ProfileController CreateProfileController(string userId)
        {
            return new ProfileController(Context)
            {
                ActionContext = MockActionContextForLogin(userId)
            };
        }

        [Fact]
        public void UserTryToViewHisProfileOk()
        {
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var profile = CreateProfileController(usr.Id);
            var result = profile.Index(usr.Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void UserTriesToVoteUnexistingFeedbackAndFails()
        {
            var announce = Context.Announces.First(a => !a.Closed && a.Author.UserName.Equals(FirstUserName));
            var feedbackAuthor = Context.Users.First(a => a.UserName.Equals(SecondUserName));
            var feedback = CreateNewFeedback(announce, feedbackAuthor, announce.Author);
            Context.FeedBacks.Add(feedback);
            Context.SaveChanges();
            var profileController = CreateProfileController(feedbackAuthor.Id);
            var result = profileController.VoteFeedbackUsefulness(999, true);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UserTriesToVoteForHisFeedbackAndFails()
        {
            var announce = Context.Announces.First(a => !a.Closed && a.Author.UserName.Equals(FirstUserName));
            var feedbackAuthor = Context.Users.First(a => a.UserName.Equals(SecondUserName));
            var feedback = CreateNewFeedback(announce, feedbackAuthor, announce.Author);
            var user = Context.Users.Single( u => u.UserName.Equals( feedback.Author.UserName ) );
            Context.FeedBacks.Add(feedback);
            Context.SaveChanges();
            var profileController = CreateProfileController(user.Id);
            var result = profileController.VoteFeedbackUsefulness(feedback.Id, true);
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData( true, 1 ), InlineData( false,-1 )]
        public void UserSetFeedbackIsUsefulAndIsOk(bool useful, int expected) {
            var announce = Context.Announces.First( a => !a.Closed && a.Author.UserName.Equals( FirstUserName ) );
            var feedbackAuthor = Context.Users.First( a => a.UserName.Equals(SecondUserName));
            var feedback = CreateNewFeedback( announce, feedbackAuthor, announce.Author );
            Context.FeedBacks.Add( feedback );
            Context.SaveChanges();
            var thirdUser = Context.Users.Single(u => u.UserName.Equals(ThirdUserName));
            var profileController = CreateProfileController(thirdUser.Id);
            var result = profileController.VoteFeedbackUsefulness( feedback.Id, useful );
            var vote = Context.UserFeedbackScores.Single( f => f.AuthorId.Equals(thirdUser.Id) && f.FeedBackId.Equals(feedback.Id) );
            Assert.Equal(vote.Useful,useful );
            Assert.IsType< ViewResult >( result );
            Assert.Equal( feedback.Usefulness, expected );
        }

        [Fact]
        public void UserWhoAlreadyGaveUsefulnessToFeedbackTriesToDoItAgainAndFails() {
            var announce = Context.Announces.First(a => !a.Closed && a.Author.UserName.Equals(FirstUserName));
            var feedbackAuthor = Context.Users.First(a => a.UserName.Equals(SecondUserName));
            var feedback = CreateNewFeedback(announce, feedbackAuthor, announce.Author);
            Context.FeedBacks.Add(feedback);
            Context.SaveChanges();
            var thirdUser = Context.Users.Single(u => u.UserName.Equals(ThirdUserName));
            var profileController = CreateProfileController(thirdUser.Id);
            profileController.VoteFeedbackUsefulness(feedback.Id, true);
            var vote = Context.UserFeedbackScores.Single(f => f.AuthorId.Equals(thirdUser.Id) && f.FeedBackId.Equals(feedback.Id));
            var result = profileController.VoteFeedbackUsefulness(feedback.Id, true);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UserWhoAlreadyGaveUsefulnessToFeedbackTriesToDoItAgainWithDifferentValueAndIsOk() {
            bool choice = true;
            var announce = Context.Announces.First(a => !a.Closed && a.Author.UserName.Equals(FirstUserName));
            var feedbackAuthor = Context.Users.First(a => a.UserName.Equals(SecondUserName));
            var feedback = CreateNewFeedback(announce, feedbackAuthor, announce.Author);
            var oldScore = feedback.Usefulness;
            Context.FeedBacks.Add(feedback);
            Context.SaveChanges();
            var thirdUser = Context.Users.Single( u => u.UserName.Equals( ThirdUserName ) );
            var profileController = CreateProfileController(thirdUser.Id);
            profileController.VoteFeedbackUsefulness(feedback.Id, choice);
            var vote = Context.UserFeedbackScores.Single(f => f.AuthorId.Equals(thirdUser.Id) && f.FeedBackId.Equals(feedback.Id));
            var result = profileController.VoteFeedbackUsefulness(feedback.Id, !choice);
            Assert.Equal(vote.Useful, !choice);
            Assert.IsType<ViewResult>(result);
            Assert.NotEqual( oldScore, feedback.Usefulness );
        }

        [Fact]
        public void VisitorTryToViewHisProfileBadRequest()
        {
            var history = CreateProfileController(null);
            Assert.IsType<BadRequestResult>(history.Index(null));
        }
    }
}