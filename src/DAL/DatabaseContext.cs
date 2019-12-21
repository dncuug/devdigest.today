using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<FacebookPage> FacebookPage { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<TwitterAccount> TwitterAccount { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vacancy> Vacancy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=server2.agi.net.ua;user id=dot_net_in_ua;password=ZQRvbUYmD7Gr;database=dot_net_in_ua;charset=utf8");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Category_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Tags)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("Channel_Category_Id_fk");

                entity.HasIndex(e => e.Name)
                    .HasName("Channel_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Logo)
                    .HasColumnType("varchar(256)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Channel)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("Channel_Category_Id_fk");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("Event_Category_Id_fk");

                entity.HasIndex(e => e.UserId)
                    .HasName("Event_User_Id_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Link)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("Event_Category_Id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Event_User_Id_fk");
            });

            modelBuilder.Entity<FacebookPage>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("FacebookPage_Category_Id_fk");

                entity.HasIndex(e => e.Id)
                    .HasName("FacebookPages_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Token)
                    .HasName("FacebookPages_Token_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Logo)
                    .HasColumnType("varchar(256)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("varchar(256)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FacebookPage)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FacebookPage_Category_Id_fk");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Language_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(25)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("Publication_Category_Id_fk");

                entity.HasIndex(e => e.LanguageId)
                    .HasName("Publication_Language_Id_fk");

                entity.HasIndex(e => e.Link)
                    .HasName("Publication_Link_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("Publication_User_Id_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(5000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EmbededPlayerCode)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Image)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LanguageId).HasColumnType("int(11)");

                entity.Property(e => e.Link)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .HasColumnType("varchar(25)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.Views)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Visible).HasColumnType("bit(1)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Publication)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Publication_Category_Id_fk");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Publication)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("Publication_Language_Id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Publication)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Publication_User_Id_fk");
            });

            modelBuilder.Entity<TwitterAccount>(entity =>
            {
                entity.HasIndex(e => e.AccessToken)
                    .HasName("TwitterAccount_AccessToken_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.AccessTokenSecret)
                    .HasName("TwitterAccount_AccessTokenSecret_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.ConsumerKey)
                    .HasName("TwitterAccount_ConsumerKey_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.ConsumerSecret)
                    .HasName("TwitterAccount_ConsumerSecret_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("TwitterAccount_Name_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Url)
                    .HasName("TwitterAccount_Url_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AccessTokenSecret)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ConsumerKey)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ConsumerSecret)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Url)
                    .HasColumnType("varchar(500)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Key)
                    .HasName("User_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("Vacancy_Category_Id_fk");

                entity.HasIndex(e => e.Id)
                    .HasName(" Vacancy_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Image)
                    .HasName(" Vacancy_Image_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.LanguageId)
                    .HasName("Vacancy_Language_Id_fk");

                entity.HasIndex(e => e.UserId)
                    .HasName("Vacancy_User_Id_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Company)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(5000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Image)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Location)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Url)
                    .HasColumnType("varchar(5000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.Views)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Vacancy)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Vacancy_Category_Id_fk");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Vacancy)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("Vacancy_Language_Id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vacancy)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Vacancy_User_Id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
