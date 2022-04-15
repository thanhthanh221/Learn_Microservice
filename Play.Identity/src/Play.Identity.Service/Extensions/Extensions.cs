using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Play.identity.Service.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Play.identity.Service.Data;
using Microsoft.EntityFrameworkCore;
using Play.identity.Service.Models;


namespace Play.identity.Service.Extensions 
{
    public static class Extensions 
    {
        public static void CreateDatabase() {
            IdentityAppDbContext identityAppDbContext = new IdentityAppDbContext();

            String dbName = identityAppDbContext.Database.GetDbConnection().Database;

            bool kq =  identityAppDbContext.Database.EnsureCreated();

            identityAppDbContext.SaveChanges();
        }
        
    }
}