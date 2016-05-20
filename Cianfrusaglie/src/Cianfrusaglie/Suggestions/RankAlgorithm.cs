using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Constants;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.Announce;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Net.Http.Headers;
using static Cianfrusaglie.Constants.CommonFunctions;
using Cianfrusaglie.GeoPosition;

namespace Cianfrusaglie.Suggestions
{
    public class RankAlgorithm {
        private ApplicationDbContext _context;

        public RankAlgorithm(ApplicationDbContext context) { _context = context; }


        /*         
                     if( !LoginChecker.HasLoggedUser( this ) ){
                            ViewData[ "listAnnounces" ] = _context.Announces.OrderByDescending( u => u.PublishDate ).Take( 4 ).ToList();
                     }else{
                            var user = _context.Users.Single( u => User.GetUserId().Equals( u.Id ) );
                            ViewData[ "listAnnounces" ] = _context.Announces.OrderByDescending( a => calculateRank(a, user) ).Take( 4 ).ToList();
                     }
         */

        public int calculateRank(Announce announce, User user) {
            int score = calculateDistanceScore( announce, user ) + calculateMatchedCategoriesScore( announce, user ) +
                        calculateMatchedGatsScore( announce, user );
            int rank = score * calculateFeedbackMultiplier( announce );
            return rank;
        }

        public int calculateFeedbackMultiplier(Announce announce)
        {
            var userFeedbackVotes = _context.FeedBacks.Where(a => a.Receiver.Equals( announce.Author ));
            int userFeedbackSum = userFeedbackVotes.Sum( f => f.Vote );
            var userFeedbackMean = userFeedbackSum / userFeedbackVotes.Count();
            return (int) (userFeedbackMean * Math.Sqrt( userFeedbackSum));
        }

        public int calculateDistanceScore( Announce announce, User user ) {
            var distance = GeoCoordinate.Distance( announce.Latitude, announce.Longitude, user.Latitude, user.Longitude );
            return (int) (100 - Math.Max( 50, distance));
        }

        public int calculateMatchedGatsScore(Announce announce, User user) {
            var announceGats = _context.AnnounceGats.Where( a=> a.AnnounceId.Equals( announce.Id ) );
            var userHistogramGats = _context.UserGatHistograms.Where( u=> u.UserId.Equals( user.Id ) );
            var userTotalCount = userHistogramGats.Sum( a => a.Count );
            double score = 0;
            foreach( var gat in userHistogramGats ) {
                if( announceGats.Select( a=> a.Gat ).Contains( gat.Gat ) ) {
                    score += 100 * gat.Count / (double) userTotalCount;
                } 
            }
            return (int) score;
        }

        public int calculateMatchedCategoriesScore( Announce announce, User user ) {
            var announceCategories = _context.Announces.Single( a => a.Id.Equals( announce.Id ) ).AnnounceCategories;
            var userPreferredCategories = _context.UserCategoryPreferenceses.Where( p => p.UserId.Equals( user.Id ) );
            int score = 0;
            foreach( var category in announceCategories ) {
                if( userPreferredCategories.Select( a=> a.Category ).Contains( category.Category ) ) {
                    score += 20;
                }
                
            }
            return Math.Min( 100, score );
        }

    }
}
