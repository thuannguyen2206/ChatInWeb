using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebChat.DataAccess.EF
{
    public class WebChatDbContextFactory : IDesignTimeDbContextFactory<WebChatDbContext>
    {
        public WebChatDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("WebChatDb");
            var builder = new DbContextOptionsBuilder<WebChatDbContext>();
            builder.UseSqlServer(connectionString);

            return new WebChatDbContext(builder.Options);
        }
    }
}
