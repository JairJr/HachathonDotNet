using Microsoft.EntityFrameworkCore;

namespace ServiceImpl
{
    public class VideoContext : DbContext
    {
        public DbSet<Entities.Database.Video> Videos { get; set; }

        public VideoContext(DbContextOptions<VideoContext> options) : base(options) { }
    }
}
