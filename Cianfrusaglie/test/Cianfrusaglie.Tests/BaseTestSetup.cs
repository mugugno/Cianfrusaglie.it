using Cianfrusaglie.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Storage.Internal;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class BaseTestSetup
    {
        protected ApplicationDbContext _context;
        public BaseTestSetup()
        {
            var obj = new DbContextOptionsBuilder();
            obj.UseInMemoryDatabase();
            _context = new ApplicationDbContext(obj.Options);
            CreateUsers();

            _context.SaveChanges();
        }

        private void CreateUsers()
        {
            _context.Users.Add(new User() { UserName = "pippopaolo", Email = "pippopaolo@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            _context.Users.Add(new User() { UserName = "pippopaolo2", Email = "pippopaolo2@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            _context.Users.Add(new User() { UserName = "pippopaolo3", Email = "pippopaolo3@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            _context.Users.Add(new User() { UserName = "pippopaolo4", Email = "pippopaolo4@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
        }

        private void CreateAnnounces()
        {
            var announce = new Announce();
            var usr = _context.Users.First();
            announce.Author = usr;
            announce.Title = "Un annuncio bello bello";
            announce.Description = "Sono bello";
            announce.GeoCoordinate = new GeoCoordinateEntity();
            _context.Announces.Add(announce);
            _context.SaveChanges();
        }
      
    }
}
