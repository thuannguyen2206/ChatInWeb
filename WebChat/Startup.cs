using System;
using System.IO;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Storage;
using WebChat.Common.Utilities.TokenProvider;
using WebChat.Common.Validations;
using WebChat.DataAccess.EF;
using WebChat.Entities.Model;
using WebChat.Service.IServices;
using WebChat.Service.Sevices;
using WebChat.WebApp.Hubs;

namespace WebChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebChatDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(SystemConstant.MainConnectionString)));

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<WebChatDbContext>().AddDefaultTokenProviders();

            // Cookie authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromDays(10);
                    options.Cookie.HttpOnly = true;
                });

            // Using fluent validation
            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());

            //add razor runtime
            IMvcBuilder builder = services.AddRazorPages();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
#if DEBUG
            if (environment == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif

            // Add session
            services.AddDistributedMemoryCache();
            services.AddSession(option => 
            {
                option.IdleTimeout = TimeSpan.FromHours(6);
                option.Cookie.IsEssential = true;
            });


            // Declare Dependence Injection
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<UserManager<User>, UserManager<User>>();
            services.AddTransient<SignInManager<User>, SignInManager<User>>();
            services.AddTransient<RoleManager<Role>, RoleManager<Role>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileStorage, FileStorage>();
            services.AddTransient<IDiscussionService, DiscussionService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<EmailConfirmationTokenProvider<User>>();
            services.AddTransient<IChatService, ChatService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Configuration);

            // Configuration
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Email settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.ProviderMap.Add("EmailConfirmation",
                    new TokenProviderDescriptor(typeof(EmailConfirmationTokenProvider<User>)));
                options.Tokens.EmailConfirmationTokenProvider = "EmailConfirmation";
            });

            // SignalR service
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
                o.KeepAliveInterval = TimeSpan.FromSeconds(SystemConstant.KeepAliveIntervalSeconds);
                o.ClientTimeoutInterval = TimeSpan.FromSeconds(SystemConstant.ClientTimeoutIntervalSeconds);
                o.MaximumReceiveMessageSize = SystemConstant.MaximumSizeFile; // 20Mb
            });

            // Add Toast notification
            services.AddNotyf(config => { config.DurationInSeconds = 5; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseNotyf();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "login",
                   pattern: "dang-nhap",
                   defaults: new { controller = "Account", action = "Login" });

                endpoints.MapControllerRoute(
                   name: "register",
                   pattern: "dang-ky",
                   defaults: new { controller = "Account", action = "Register" });

                endpoints.MapControllerRoute(
                   name: "changePassword",
                   pattern: "doi-mat-khau/{id?}",
                   defaults: new { controller = "User", action = "ChangePassword" });

                endpoints.MapControllerRoute(
                   name: "accessdenied",
                   pattern: "chan-truy-cap",
                   defaults: new { controller = "Account", action = "AccessDenied" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>(SystemConstant.ChatHub, option =>
                {
                    option.Transports = HttpTransportType.WebSockets |
                                        HttpTransportType.ServerSentEvents |
                                        HttpTransportType.LongPolling;
                });
            });
        }
    }
}
