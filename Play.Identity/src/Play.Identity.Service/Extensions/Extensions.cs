using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Play.identity.Service.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Play.identity.Service.Data;
using Microsoft.EntityFrameworkCore;
using Play.identity.Service.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


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
        public static IServiceCollection SetUpIdentity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions> (options => {
                // Thiết lập về Password
                options.Password.RequireDigit = true; // bắt phải có số
                options.Password.RequireLowercase = true; // bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = true; // bắt buộc chữ in
                options.Password.RequiredLength = 6;     // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 0; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ạ ế";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = false;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = false;


            });
            return services;
        }
        public static IServiceCollection AuthenticationCookie(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Thời gian Cookie có hiệu lực
                    options.SlidingExpiration = true;   // Cấp lại coookie mới 
                    options.AccessDeniedPath = "./ForNull";   // Đường dẫn chuyển hướng
                });
            return services;
        }
        
    }
}