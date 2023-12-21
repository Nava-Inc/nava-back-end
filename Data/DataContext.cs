using Microsoft.EntityFrameworkCore;
using Nava.Entities;

namespace Nava.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Comment> comments { get; set; }
    public DbSet<DownloadedMusic> downloadedMusics { get; set; }
    public DbSet<Music> musics { get; set; }
    public DbSet<PlayList> playLists { get; set; }
    public DbSet<PlayListMusic> playListMusics { get; set; }
    public DbSet<UserInfo> userInfos { get; set; }
    public DbSet<UserInteraction> userInteractions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayListMusic>()
            .HasKey(pm => new { pm.PlayListId, pm.MusicId });
        modelBuilder.Entity<PlayListMusic>()
            .HasOne(p => p.PlayList)
            .WithMany(pm => pm.PlayListMusics)
            .HasForeignKey(p => p.PlayListId);
        modelBuilder.Entity<PlayListMusic>()
            .HasOne(p => p.Music)
            .WithMany(pm => pm.PlayListMusics)
            .HasForeignKey(p => p.MusicId);
    }
}