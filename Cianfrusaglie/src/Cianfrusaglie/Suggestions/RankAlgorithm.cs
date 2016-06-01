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
        private readonly ApplicationDbContext _context;

        public RankAlgorithm(ApplicationDbContext context) { _context = context; }

        public int CalculateRank(Announce announce, User user) {
            int score = CalculateDistanceScore( announce, user ) + CalculateMatchedCategoriesScore( announce, user ) + CalculateMatchedGatsScore( announce, user );
            double rank = score * CalculateFeedbackMultiplier(announce.Author);
            return (int) rank;
        }

        public double CalculateFeedbackMultiplier(User user) {
            double multiplier = Math.Sqrt(user.FeedbacksCount) * user.FeedbacksMean;
            return multiplier <= 0.05 ? 1 : multiplier;
        }

        public int CalculateDistanceScore( Announce announce, User user ) {
            double distance = GeoCoordinate.Distance( announce.Latitude, announce.Longitude, user.Latitude, user.Longitude );
            return (int) (100 - Math.Min( 50, distance));
        }

        public int CalculateMatchedGatsScore(Announce announce, User user) {
            var announceGats = _context.AnnounceGats.Where( a=> a.AnnounceId.Equals( announce.Id ) );
            var userHistogramGats = _context.UserGatHistograms.Where( u=> u.UserId.Equals( user.Id ) );
            int userTotalCount = userHistogramGats.Sum( a => a.Count );
            double score = 0;
            foreach( var gat in userHistogramGats ) {
                if( announceGats.Select( a=> a.Gat ).Contains( gat.Gat ) ) {
                    score += 100 * gat.Count / (double) userTotalCount;
                } 
            }
            return (int) score;
        }

        public int CalculateMatchedCategoriesScore( Announce announce, User user ) {
            var announceCategories = _context.AnnounceCategories.Where( a => a.AnnounceId.Equals( announce.Id ) ).Select( a=> a.CategoryId );
            var userPreferredCategories = _context.UserCategoryPreferenceses.Where( p => p.UserId.Equals( user.Id ) ).Select( p=> p.CategoryId );
            int score = 0;
            foreach (var category in announceCategories)
            {
                if (userPreferredCategories.Contains(category))
                {
                    score += 20;
                }

            }
            return Math.Min( 100, score );
        }

    }
}
