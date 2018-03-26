namespace ConsoleApp1
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

        public virtual DbSet<Bot> Bots { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Game_Round> Game_Round { get; set; }
        public virtual DbSet<Hand> Hands { get; set; }
        public virtual DbSet<Human> Humen { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Scoresheet> Scoresheets { get; set; }
        public virtual DbSet<Table_Match> Table_Match { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bot>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Game_Round)
                .WithOptional(e => e.Card)
                .HasForeignKey(e => e.Leading_Card);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Hands)
                .WithRequired(e => e.Card)
                .HasForeignKey(e => e.Hand_Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Games)
                .WithMany(e => e.Cards)
                .Map(m => m.ToTable("Blind").MapLeftKey("Blind_Card").MapRightKey("Blind_Game"));

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Game_Round)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Hands)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.Hand_Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game_Round>()
                .HasMany(e => e.Cards)
                .WithMany(e => e.Game_Round1)
                .Map(m => m.ToTable("Trick_Hand").MapLeftKey("Trick_Round").MapRightKey("Trick_Card"));

            modelBuilder.Entity<Human>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .HasOptional(e => e.Bot)
                .WithRequired(e => e.Player);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Partner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Games1)
                .WithRequired(e => e.Player1)
                .HasForeignKey(e => e.Picker)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Game_Round)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_Turn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Hands)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Hand_Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasOptional(e => e.Human)
                .WithRequired(e => e.Player);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Scoresheets)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Table_Match)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Dealer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Table_Match1)
                .WithRequired(e => e.Player1)
                .HasForeignKey(e => e.Host_Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Table_Match2)
                .WithMany(e => e.Players)
                .Map(m => m.ToTable("Table_Players").MapLeftKey("TP_Player_Id").MapRightKey("TP_Table_ID"));

            modelBuilder.Entity<Table_Match>()
                .HasMany(e => e.Scoresheets)
                .WithRequired(e => e.Table_Match)
                .WillCascadeOnDelete(false);
        }
    }
}
