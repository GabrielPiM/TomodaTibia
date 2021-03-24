using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class TomodaTibiaContext : DbContext
    {
        public TomodaTibiaContext()
        {
        }

        public TomodaTibiaContext(DbContextOptions<TomodaTibiaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Achivement> Achivements { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorFav> AuthorFavs { get; set; }
        public virtual DbSet<AuthorRat> AuthorRats { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<ClientVersion> ClientVersions { get; set; }
        public virtual DbSet<Equipament> Equipaments { get; set; }
        public virtual DbSet<Hunt> Hunts { get; set; }
        public virtual DbSet<HuntClientVersion> HuntClientVersions { get; set; }
        public virtual DbSet<HuntDesc> HuntDescs { get; set; }
        public virtual DbSet<HuntMonster> HuntMonsters { get; set; }
        public virtual DbSet<HuntSpecialReq> HuntSpecialReqs { get; set; }
        public virtual DbSet<HuntingPlace> HuntingPlaces { get; set; }
        public virtual DbSet<Imbuement> Imbuements { get; set; }
        public virtual DbSet<ImbuementItem> ImbuementItems { get; set; }
        public virtual DbSet<ImbuementLevel> ImbuementLevels { get; set; }
        public virtual DbSet<ImbuementType> ImbuementTypes { get; set; }
        public virtual DbSet<ImbuementValue> ImbuementValues { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Key> Keys { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LootRarity> LootRarities { get; set; }
        public virtual DbSet<Monster> Monsters { get; set; }
        public virtual DbSet<MonsterLoot> MonsterLoots { get; set; }
        public virtual DbSet<Mount> Mounts { get; set; }
        public virtual DbSet<Object> Objects { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerImbuement> PlayerImbuements { get; set; }
        public virtual DbSet<PlayerItem> PlayerItems { get; set; }
        public virtual DbSet<PlayerPrey> PlayerPreys { get; set; }
        public virtual DbSet<Prey> Preys { get; set; }
        public virtual DbSet<Quest> Quests { get; set; }
        public virtual DbSet<Situation> Situations { get; set; }
        public virtual DbSet<SpecialReqType> SpecialReqTypes { get; set; }
        public virtual DbSet<Spell> Spells { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }
        public virtual DbSet<Vocation> Vocations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Achivement>(entity =>
            {
                entity.ToTable("achivement");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsAdmin)
                    .HasColumnName("is_admin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsBan)
                    .HasColumnName("is_ban")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NameMainChar)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name_main_char");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UrlSocial)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("url_social");
            });

            modelBuilder.Entity<AuthorFav>(entity =>
            {
                entity.ToTable("author_fav");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAuthor).HasColumnName("id_author");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.AuthorFavs)
                    .HasForeignKey(d => d.IdAuthor)
                    .HasConstraintName("fk__author_fav__hunt");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.AuthorFavs)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk__author_fav__author");
            });

            modelBuilder.Entity<AuthorRat>(entity =>
            {
                entity.ToTable("author_rat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAuthor).HasColumnName("id_author");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.AuthorRats)
                    .HasForeignKey(d => d.IdAuthor)
                    .HasConstraintName("fk__author_rat__hunt");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.AuthorRats)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk__author_rat__author");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<ClientVersion>(entity =>
            {
                entity.ToTable("client_version");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.VersionName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("version_name");
            });

            modelBuilder.Entity<Equipament>(entity =>
            {
                entity.ToTable("equipament");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ammo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ammo");

                entity.Property(e => e.Amulet)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("amulet");

                entity.Property(e => e.Armor)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("armor");

                entity.Property(e => e.Bag)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bag");

                entity.Property(e => e.Boots)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("boots");

                entity.Property(e => e.Helmet)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("helmet");

                entity.Property(e => e.IdPlayer).HasColumnName("id_player");

                entity.Property(e => e.Legs)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("legs");

                entity.Property(e => e.Ring)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ring");

                entity.Property(e => e.WeaponLeft)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("weapon_left");

                entity.Property(e => e.WeaponRight)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("weapon_right");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.Equipaments)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk__equipment__player");
            });

            modelBuilder.Entity<Hunt>(entity =>
            {
                entity.ToTable("hunt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Difficulty)
                    .HasColumnName("difficulty")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdAuthor).HasColumnName("id_author");

                entity.Property(e => e.IdSituation).HasColumnName("id_situation");

                entity.Property(e => e.IsPremium)
                    .HasColumnName("is_premium")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LevelMinReq).HasColumnName("level_min_req");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TeamSize).HasColumnName("team_size");

                entity.Property(e => e.TutorialVideoUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("tutorial_video_url");

                entity.Property(e => e.XpHr).HasColumnName("xp_hr");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.Hunts)
                    .HasForeignKey(d => d.IdAuthor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__hunt__author");

                entity.HasOne(d => d.IdSituationNavigation)
                    .WithMany(p => p.Hunts)
                    .HasForeignKey(d => d.IdSituation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__hunt__situation");
            });

            modelBuilder.Entity<HuntClientVersion>(entity =>
            {
                entity.ToTable("hunt_client_version");

                entity.HasIndex(e => new { e.IdHunt, e.IdClientVersion }, "UQ__hunt_cli__2C9F99AC712AD959")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdClientVersion).HasColumnName("id_client_version");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.HasOne(d => d.IdClientVersionNavigation)
                    .WithMany(p => p.HuntClientVersions)
                    .HasForeignKey(d => d.IdClientVersion)
                    .HasConstraintName("FK__hunt_clie__id_cl__59063A47");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntClientVersions)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk__hunt_client_version__hunt");
            });

            modelBuilder.Entity<HuntDesc>(entity =>
            {
                entity.ToTable("hunt_desc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.Paragraph)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("paragraph");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntDescs)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk__hunt_desc__hunt");
            });

            modelBuilder.Entity<HuntMonster>(entity =>
            {
                entity.ToTable("hunt_monster");

                entity.HasIndex(e => new { e.IdHunt, e.IdMonster }, "UQ__hunt_mon__E736D91E2F088141")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdMonster).HasColumnName("id_monster");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntMonsters)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk__hunt_monster__hunt");

                entity.HasOne(d => d.IdMonsterNavigation)
                    .WithMany(p => p.HuntMonsters)
                    .HasForeignKey(d => d.IdMonster)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__hunt_monster__monster");
            });

            modelBuilder.Entity<HuntSpecialReq>(entity =>
            {
                entity.ToTable("hunt_special_req");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("value");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntSpecialReqs)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk__hunt_special_req__hunt");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.HuntSpecialReqs)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__hunt_special_req__special_req_type");
            });

            modelBuilder.Entity<HuntingPlace>(entity =>
            {
                entity.ToTable("hunting_place");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Imbuement>(entity =>
            {
                entity.ToTable("imbuement");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("desc");

                entity.Property(e => e.IdImbuementType).HasColumnName("id_imbuement_type");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.HasOne(d => d.IdImbuementTypeNavigation)
                    .WithMany(p => p.Imbuements)
                    .HasForeignKey(d => d.IdImbuementType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__imbuement__imbuement_type");
            });

            modelBuilder.Entity<ImbuementItem>(entity =>
            {
                entity.ToTable("imbuement_item");

                entity.HasIndex(e => new { e.IdImbuement, e.IdItem }, "UQ__imbuemen__F46AD84346E5BB60")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdImbuement).HasColumnName("id_imbuement");

                entity.Property(e => e.IdImbuementLevel).HasColumnName("id_imbuement_level");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.IdImbuementNavigation)
                    .WithMany(p => p.ImbuementItems)
                    .HasForeignKey(d => d.IdImbuement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__imbuement_item__imbuement");

                entity.HasOne(d => d.IdImbuementLevelNavigation)
                    .WithMany(p => p.ImbuementItems)
                    .HasForeignKey(d => d.IdImbuementLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__imbuement_item__imbuement_level");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.ImbuementItems)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__imbuement_item__item");
            });

            modelBuilder.Entity<ImbuementLevel>(entity =>
            {
                entity.ToTable("imbuement_level");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ImbuementType>(entity =>
            {
                entity.ToTable("imbuement_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ImbuementValue>(entity =>
            {
                entity.ToTable("imbuement_value");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdImbuementLevel).HasColumnName("id_imbuement_level");

                entity.Property(e => e.IdImbuementType).HasColumnName("id_imbuement_type");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("value");

                entity.HasOne(d => d.IdImbuementLevelNavigation)
                    .WithMany(p => p.ImbuementValues)
                    .HasForeignKey(d => d.IdImbuementLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__imbuement_value__imbuement_level");

                entity.HasOne(d => d.IdImbuementTypeNavigation)
                    .WithMany(p => p.ImbuementValues)
                    .HasForeignKey(d => d.IdImbuementType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__imbuement_value__imbuement_type");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Key>(entity =>
            {
                entity.ToTable("key");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<LootRarity>(entity =>
            {
                entity.ToTable("loot_rarity");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Monster>(entity =>
            {
                entity.ToTable("monster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.IsPray)
                    .HasColumnName("is_pray")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MonsterLoot>(entity =>
            {
                entity.ToTable("monster_loot");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("amount");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.IdLootRarity).HasColumnName("id_loot_rarity");

                entity.Property(e => e.IdMonster).HasColumnName("id_monster");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.MonsterLoots)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__monster_loot__item");

                entity.HasOne(d => d.IdLootRarityNavigation)
                    .WithMany(p => p.MonsterLoots)
                    .HasForeignKey(d => d.IdLootRarity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__monster_loot__loot_rarity");

                entity.HasOne(d => d.IdMonsterNavigation)
                    .WithMany(p => p.MonsterLoots)
                    .HasForeignKey(d => d.IdMonster)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__monster_loot__monster");
            });

            modelBuilder.Entity<Mount>(entity =>
            {
                entity.ToTable("mount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.ToTable("object");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Function)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("function");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Vocation).HasColumnName("vocation");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk__player__hunt");
            });

            modelBuilder.Entity<PlayerImbuement>(entity =>
            {
                entity.ToTable("player_imbuement");

                entity.HasIndex(e => new { e.IdPlayer, e.IdImbuement }, "UQ__player_i__F00E167766E01EFD")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdImbuement).HasColumnName("id_imbuement");

                entity.Property(e => e.IdImbuementLevel).HasColumnName("id_imbuement_level");

                entity.Property(e => e.IdImbuementType).HasColumnName("id_imbuement_type");

                entity.Property(e => e.IdPlayer).HasColumnName("id_player");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdImbuementNavigation)
                    .WithMany(p => p.PlayerImbuements)
                    .HasForeignKey(d => d.IdImbuement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__player_imbuement__imbuement");

                entity.HasOne(d => d.IdImbuementLevelNavigation)
                    .WithMany(p => p.PlayerImbuements)
                    .HasForeignKey(d => d.IdImbuementLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__player_imbuement__imbuement_level");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.PlayerImbuements)
                    .HasForeignKey(d => d.IdPlayer)
                    .HasConstraintName("fk__player_imbuement__player");
            });

            modelBuilder.Entity<PlayerItem>(entity =>
            {
                entity.ToTable("player_item");

                entity.HasIndex(e => new { e.IdPlayer, e.IdItem }, "UQ__player_i__EDB3E6884A7D6242")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.IdPlayer).HasColumnName("id_player");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.PlayerItems)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__player_item__item");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.PlayerItems)
                    .HasForeignKey(d => d.IdPlayer)
                    .HasConstraintName("fk__player_item__player");
            });

            modelBuilder.Entity<PlayerPrey>(entity =>
            {
                entity.ToTable("player_prey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdMonster).HasColumnName("id_monster");

                entity.Property(e => e.IdPlayer).HasColumnName("id_player");

                entity.Property(e => e.IdPrey).HasColumnName("id_prey");

                entity.Property(e => e.ReccStar).HasColumnName("recc_star");

                entity.HasOne(d => d.IdMonsterNavigation)
                    .WithMany(p => p.PlayerPreys)
                    .HasForeignKey(d => d.IdMonster)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__player_prey__monster");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.PlayerPreys)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk__player_prey__player");

                entity.HasOne(d => d.IdPreyNavigation)
                    .WithMany(p => p.PlayerPreys)
                    .HasForeignKey(d => d.IdPrey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__player_prey__prey");
            });

            modelBuilder.Entity<Prey>(entity =>
            {
                entity.ToTable("prey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Quest>(entity =>
            {
                entity.ToTable("quest");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Situation>(entity =>
            {
                entity.ToTable("situation");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SpecialReqType>(entity =>
            {
                entity.ToTable("special_req_type");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Spell>(entity =>
            {
                entity.ToTable("spell");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Statistic>(entity =>
            {
                entity.ToTable("statistic");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.QtyAchivement).HasColumnName("qty_achivement");

                entity.Property(e => e.QtyAuthor).HasColumnName("qty_author");

                entity.Property(e => e.QtyBook).HasColumnName("qty_book");

                entity.Property(e => e.QtyHunt).HasColumnName("qty_hunt");

                entity.Property(e => e.QtyHuntingPlace).HasColumnName("qty_hunting_place");

                entity.Property(e => e.QtyImbuement).HasColumnName("qty_imbuement");

                entity.Property(e => e.QtyKey).HasColumnName("qty_key");

                entity.Property(e => e.QtyLocation).HasColumnName("qty_location");

                entity.Property(e => e.QtyMonster).HasColumnName("qty_monster");

                entity.Property(e => e.QtyMonsterPrey).HasColumnName("qty_monster_prey");

                entity.Property(e => e.QtyMount).HasColumnName("qty_mount");

                entity.Property(e => e.QtyNpc).HasColumnName("qty_npc");

                entity.Property(e => e.QtyObject).HasColumnName("qty_object");

                entity.Property(e => e.QtyPrey).HasColumnName("qty_prey");

                entity.Property(e => e.QtyQuest).HasColumnName("qty_quest");

                entity.Property(e => e.QtySpell).HasColumnName("qty_spell");
            });

            modelBuilder.Entity<Vocation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vocation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
