namespace ORM.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebMatrix.WebData;
    using ORM;

    internal sealed class Configuration : DbMigrationsConfiguration<ORM.EntityModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ORM.EntityModel context)
        {

            context.Genres.AddOrUpdate(x => x.Id,
        new Genre() { Id = 1, Name = "Hip-Hop",AuthorId=1 },
        new Genre() { Id = 2, Name = "Rap", AuthorId = 1 },
        new Genre() { Id = 3, Name = "Rock", AuthorId = 1 },
        new Genre() { Id = 4, Name = "Folk", AuthorId = 1 }
        );

            WebSecurity.InitializeDatabaseConnection("MuzixContent", "Users", "Id", "Name", autoCreateTables: true);

            var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;
            //var membership = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;
           
            if (!roles.RoleExists("Admin"))
                roles.CreateRole("Admin");

            if (!roles.RoleExists("Moderator"))
                roles.CreateRole("Moderator");

            if (!roles.RoleExists("User"))
                roles.CreateRole("User");

            if (!roles.RoleExists("Guest"))
                roles.CreateRole("Guest");


            if (!WebSecurity.UserExists("admin"))
                WebSecurity.CreateUserAndAccount(
                    "admin",
                    "q1w2e3"
                    );

            if (!WebSecurity.UserExists("moder"))
                WebSecurity.CreateUserAndAccount(
                    "moder",
                    "q1w2e3"
                    );

            if (!WebSecurity.UserExists("artem"))
                WebSecurity.CreateUserAndAccount(
                    "artem",
                    "q1w2e3"
                    );

            if (!roles.GetRolesForUser("admin").Contains("Admin"))
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin", "Moderator" });

            if (!roles.GetRolesForUser("moder").Contains("Moderator"))
                roles.AddUsersToRoles(new[] { "moder" }, new[] { "Moderator" });

            if (!roles.GetRolesForUser("artem").Contains("User"))
                roles.AddUsersToRoles(new[] { "artem" }, new[] { "User" });
        }
    }
}
