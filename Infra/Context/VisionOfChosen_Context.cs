using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Infra.Context
{
    public class VisionOfChosen_Context : DbContext
    {
        public VisionOfChosen_Context(DbContextOptions<VisionOfChosen_Context> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<AiChatHistory> AiChatHistories => Set<AiChatHistory>();
        public DbSet<Scan> Scans => Set<Scan>();
        public DbSet<ScanDetail> ScanDetails => Set<ScanDetail>();
        public DbSet<Drift> Drifts => Set<Drift>();
        public DbSet<User> Users => Set<User>();
        public DbSet<AwsCredential> AwsCredentials => Set<AwsCredential>();
        public DbSet<EmailNotification> EmailNotifications => Set<EmailNotification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScanDetail>()
                .HasMany(s => s.Drifts)
                .WithOne(d => d.ScanDetail)
                .HasForeignKey(d => d.ScanDetailId);
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ExtendModel).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "deleted");
                    var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);

                    entityType.SetQueryFilter(filter);
                }
            }
        }
    }
}
