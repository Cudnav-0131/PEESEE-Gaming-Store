using PEESEE_GAMING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace PEESEE_GAMING
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["Products"] = ProductData.GetProducts();
            var products = Application["Products"] as List<PEESEE_GAMING.Models.Product>;

            Application["Categories"] = products
                .Where(p => !string.IsNullOrWhiteSpace(p.Category))
                .Select(p => p.Category.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            Application["Orders"] = new List<StoreOrder>();

            Application["Users"] = new List<StoreUser>();

            var users = new List<StoreUser>();

            users.Add(new StoreUser
            {
                Id = Guid.NewGuid().ToString(),
                FullName = "Quản trị viên",
                Email = "admin@peesee.local",
                PasswordHash = HashPasswordForSeed("Admin@123"),
                Phone = "",
                Address = "",
                Role = "admin"
            });

            Application["Users"] = users;
        }
        private string HashPasswordForSeed(string password)
        {
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var derive = new Rfc2898DeriveBytes(
                password, salt, 100000))
            {
                byte[] hash = derive.GetBytes(32);

                return Convert.ToBase64String(salt)
                    + "."
                    + Convert.ToBase64String(hash);
            }
        }
    }
}