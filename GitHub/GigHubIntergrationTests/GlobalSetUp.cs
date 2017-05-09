using GitHub.Core.Models;
using GitHub.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace GigHubIntergrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void Setup()
        {
            MigrateToLatestVersion();
            Seed();
        }

        private static void MigrateToLatestVersion()
        {
            var configuration = new GitHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser() { UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-" });
            context.Users.Add(new ApplicationUser() { UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-" });
            context.SaveChanges();
        }
    }
}
