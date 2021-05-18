namespace WebPresentation.Migrations
{

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using WebPresentation.Models;

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/24
    /// 
    /// Automated build for 
    /// db migratoins.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<WebPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebPresentation.Models.ApplicationDbContext";
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/24
        /// 
        /// Modeled after Professor Glasgow's 
        /// demonstrations.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(WebPresentation.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string client = "chantal@backontrack.com";
            const string defaultPassword = "P@ssw0rd";

            const string donor = "goodSamaritan@backontrack.com";

            const string admin = "admin@backontrack.com";

            // Before creating our first user, create roles
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Donor" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Client" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" });

            context.SaveChanges(); // Save newly added roles

            if (!context.Users.Any(u => u.UserName == client) && !context.Users.Any(u => u.UserName == donor)
                    && !context.Users.Any(u => u.UserName == admin))
            {
                // Client set-up
                var user = new ApplicationUser()
                {
                    UserName = client,
                    Email = client
                };

                // Donor set-up
                var userTwo = new ApplicationUser()
                {
                    UserName = donor,
                    Email = donor
                };

                // Admin set-up
                var userThree = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin
                };

                // Create both users
                IdentityResult result = userManager.Create(user, defaultPassword);
                IdentityResult resultTwo = userManager.Create(userTwo, defaultPassword);
                IdentityResult resultThree = userManager.Create(userThree, defaultPassword);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    // Create client and give the client the client role
                    userManager.AddToRole(user.Id, "Client");
                    context.SaveChanges();
                }
                if (resultTwo.Succeeded)
                {
                    userManager.AddToRole(userTwo.Id, "Donor");
                    context.SaveChanges();
                }
                if (resultThree.Succeeded)
                {
                    userManager.AddToRole(userThree.Id, "Admin");
                    context.SaveChanges();
                }

            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
