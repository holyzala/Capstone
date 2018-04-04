namespace NetSheep.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Sheep : DbContext
    {
        public Sheep()
            : base("name=Sheep")
        {
        }

        public virtual DbSet<Table_Players> Table_Players { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Human> Humen { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Game_Round> Game_Round { get; set; }
        public virtual DbSet<Trick_Hand> Trick_Hand { get; set; }
        public virtual DbSet<Blind> Blinds { get; set; }
        public virtual DbSet<Bot> Bots { get; set; }
        public virtual DbSet<Hand> Hands { get; set; }
        public virtual DbSet<Scoresheet> Scoresheets { get; set; }
        public virtual DbSet<Table_Match> Table_Match { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Human>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Bot>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
