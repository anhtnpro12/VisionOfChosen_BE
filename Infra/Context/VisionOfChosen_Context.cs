using Microsoft.EntityFrameworkCore;
using System;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Infra.Context
{
    public class VisionOfChosen_Context : DbContext
    {
        public VisionOfChosen_Context(DbContextOptions<VisionOfChosen_Context> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<AiChatHistory> AiChatHistories => Set<AiChatHistory>();
        public DbSet<Scan> Scans => Set<Scan>();
    }
}
