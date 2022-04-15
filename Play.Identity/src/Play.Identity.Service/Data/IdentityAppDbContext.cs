using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Play.identity.Service.Models;
namespace Play.identity.Service.Data {
    public class IdentityAppDbContext : IdentityDbContext<AppUser>
    {
        private const string temp = "Data Source = localhost,1433;Database= Play.Identity.Service;User ID = sa;Password = Password123 ";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(temp); 
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var item in builder.Model.GetEntityTypes())// lấy được thông tin các bảng trên SQL Sever
            {
                string? Table_Name = item.GetTableName();
                if(Table_Name.StartsWith("AspNet")){
                    // Phương thức subString bỏ đi số kí tự đầu tiên trong bảng
                    item.SetTableName(Table_Name.Substring(6));
                }
            }
        }
    }
}