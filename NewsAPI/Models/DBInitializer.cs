using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsAppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPI.Models
{
    public interface IDbInitializer
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    context.Users.RemoveRange(context.Users.ToList());
                    context.SaveChanges();
                    //add admin user
                    if (!context.Users.Any())
                    {
                        var adminUser = new User
                        {
                            CreatedAt = DateTime.Now,
                            Username = "admin",
                            Password = BCrypt.Net.BCrypt.HashPassword("admin"), // should be hash
                            FullName = "Admin Test",
                            Role = "Admin"
                        };
                        context.Users.Add(adminUser);
                        var writer = new User
                        {
                            CreatedAt = DateTime.Now,
                            Username = "writer",
                            Password = BCrypt.Net.BCrypt.HashPassword("writer"), // should be hash
                            FullName = "Writer Test 2",
                            Role = "Admin"
                        };
                        context.Users.Add(adminUser);
                        var writer1 = new User
                        {
                            CreatedAt = DateTime.Now,
                            Username = "writer1",
                            Password = BCrypt.Net.BCrypt.HashPassword("writer1"), // should be hash
                            FullName = "Writer Test 2",
                            Role = "Writer"
                        };
                        context.Users.Add(adminUser);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
