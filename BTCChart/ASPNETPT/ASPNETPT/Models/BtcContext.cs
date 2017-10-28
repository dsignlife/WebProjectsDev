using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ASPNETPT.Models
{
    public class BtcContext : DbContext
    {
        private readonly IConfigurationRoot _config;

        public BtcContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public DbSet<BtCprop> Btcs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:BtcBackConnector"]);
        }
    }
}