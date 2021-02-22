using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFDataAcessLibrary.Models
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

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<ClientVersion> ClientVersions { get; set; }
        public virtual DbSet<Equipament> Equipaments { get; set; }
        public virtual DbSet<Hunt> Hunts { get; set; }
        public virtual DbSet<HuntClientVersion> HuntClientVersions { get; set; }
        public virtual DbSet<HuntImbuement> HuntImbuements { get; set; }
        public virtual DbSet<HuntItem> HuntItems { get; set; }
        public virtual DbSet<HuntLoot> HuntLoots { get; set; }
        public virtual DbSet<HuntMonster> HuntMonsters { get; set; }
        public virtual DbSet<HuntPrey> HuntPreys { get; set; }
        public virtual DbSet<Imbuement> Imbuements { get; set; }
        public virtual DbSet<ImbuementDesc> ImbuementDescs { get; set; }
        public virtual DbSet<ImbuementItem> ImbuementItems { get; set; }
        public virtual DbSet<ImbuementLevel> ImbuementLevels { get; set; }
        public virtual DbSet<ImbuementType> ImbuementTypes { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Monster> Monsters { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Prey> Preys { get; set; }
        public virtual DbSet<Vocation> Vocations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

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
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("url_social")
                    .HasDefaultValueSql("('None.')");
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
                    .HasColumnName("ammo")
                    .HasDefaultValueSql("('no_ammo.gif')");

                entity.Property(e => e.Amulet)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("amulet")
                    .HasDefaultValueSql("('no_amulet.gif')");

                entity.Property(e => e.Armor)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("armor")
                    .HasDefaultValueSql("('no_armor.gif')");

                entity.Property(e => e.Bag)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bag")
                    .HasDefaultValueSql("('no_bag.gif')");

                entity.Property(e => e.Boots)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("boots")
                    .HasDefaultValueSql("('no_boots.gif')");

                entity.Property(e => e.Helmet)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("helmet")
                    .HasDefaultValueSql("('no_helmet.gif')");

                entity.Property(e => e.IdPlayer).HasColumnName("id_player");

                entity.Property(e => e.Legs)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("legs")
                    .HasDefaultValueSql("('no_legs.gif')");

                entity.Property(e => e.Ring)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ring")
                    .HasDefaultValueSql("('no_ring.gif')");

                entity.Property(e => e.WeaponLeft)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("weapon_left")
                    .HasDefaultValueSql("('no_weapon_left.gif')");

                entity.Property(e => e.WeaponRight)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("weapon_right")
                    .HasDefaultValueSql("('no_weapon_right.gif')");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.Equipaments)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_equipment_player");
            });

            modelBuilder.Entity<Hunt>(entity =>
            {
                entity.ToTable("hunt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DescHunt)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("desc_hunt")
                    .HasDefaultValueSql("('No Description.')");

                entity.Property(e => e.Difficulty)
                    .HasColumnName("difficulty")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdAuthor).HasColumnName("id_author");

                entity.Property(e => e.IsPremium)
                    .HasColumnName("is_premium")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsValid)
                    .HasColumnName("is_valid")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LevelMinReq).HasColumnName("level_min_req");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.QtyPlayer).HasColumnName("qty_player");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TutorialVideoUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("tutorial_video_url")
                    .HasDefaultValueSql("('No Tutorial.')");

                entity.Property(e => e.XpHr).HasColumnName("xp_hr");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.Hunts)
                    .HasForeignKey(d => d.IdAuthor)
                    .HasConstraintName("FK__hunt__id_author__2B3F6F97");
            });

            modelBuilder.Entity<HuntClientVersion>(entity =>
            {
                entity.ToTable("hunt_client_version");

                entity.HasIndex(e => new { e.IdHunt, e.IdClientVersion }, "UQ__hunt_cli__2C9F99ACA597B4C0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdClientVersion).HasColumnName("id_client_version");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.HasOne(d => d.IdClientVersionNavigation)
                    .WithMany(p => p.HuntClientVersions)
                    .HasForeignKey(d => d.IdClientVersion)
                    .HasConstraintName("FK__hunt_clie__id_cl__35BCFE0A");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntClientVersions)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk_hunt_client_version");
            });

            modelBuilder.Entity<HuntImbuement>(entity =>
            {
                entity.ToTable("hunt_imbuement");

                entity.HasIndex(e => new { e.IdHunt, e.IdImbuement }, "UQ__hunt_imb__096A2349003D507A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdImbuement).HasColumnName("id_imbuement");

                entity.Property(e => e.IdImbuementLevel).HasColumnName("id_imbuement_level");

                entity.Property(e => e.IdImbuementType).HasColumnName("id_imbuement_type");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntImbuements)
                    .HasForeignKey(d => d.IdHunt)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_hunt_imbuement");

                entity.HasOne(d => d.IdImbuementNavigation)
                    .WithMany(p => p.HuntImbuements)
                    .HasForeignKey(d => d.IdImbuement)
                    .HasConstraintName("FK__hunt_imbu__id_im__6EF57B66");

                entity.HasOne(d => d.IdImbuementLevelNavigation)
                    .WithMany(p => p.HuntImbuements)
                    .HasForeignKey(d => d.IdImbuementLevel)
                    .HasConstraintName("FK__hunt_imbu__id_im__6FE99F9F");

                entity.HasOne(d => d.IdImbuementTypeNavigation)
                    .WithMany(p => p.HuntImbuements)
                    .HasForeignKey(d => d.IdImbuementType)
                    .HasConstraintName("FK__hunt_imbu__id_im__70DDC3D8");
            });

            modelBuilder.Entity<HuntItem>(entity =>
            {
                entity.ToTable("hunt_item");

                entity.HasIndex(e => new { e.IdHunt, e.IdItem }, "UQ__hunt_ite__14D7D3B6911CC7B4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntItems)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk_hunt_item");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.HuntItems)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK__hunt_item__id_it__571DF1D5");
            });

            modelBuilder.Entity<HuntLoot>(entity =>
            {
                entity.ToTable("hunt_loot");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntLoots)
                    .HasForeignKey(d => d.IdHunt)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_hunt_loot");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.HuntLoots)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK__hunt_loot__id_it__7E37BEF6");
            });

            modelBuilder.Entity<HuntMonster>(entity =>
            {
                entity.ToTable("hunt_monster");

                entity.HasIndex(e => new { e.IdHunt, e.IdMonster }, "UQ__hunt_mon__E736D91EBC83B481")
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
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_hunt_monster");

                entity.HasOne(d => d.IdMonsterNavigation)
                    .WithMany(p => p.HuntMonsters)
                    .HasForeignKey(d => d.IdMonster)
                    .HasConstraintName("FK__hunt_mons__id_mo__3D5E1FD2");
            });

            modelBuilder.Entity<HuntPrey>(entity =>
            {
                entity.ToTable("hunt_prey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdMonster).HasColumnName("id_monster");

                entity.Property(e => e.IdPrey).HasColumnName("id_prey");

                entity.Property(e => e.ReccStar).HasColumnName("recc_star");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.HuntPreys)
                    .HasForeignKey(d => d.IdHunt)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_hunt_prey");

                entity.HasOne(d => d.IdMonsterNavigation)
                    .WithMany(p => p.HuntPreys)
                    .HasForeignKey(d => d.IdMonster)
                    .HasConstraintName("FK__hunt_prey__id_mo__797309D9");

                entity.HasOne(d => d.IdPreyNavigation)
                    .WithMany(p => p.HuntPreys)
                    .HasForeignKey(d => d.IdPrey)
                    .HasConstraintName("FK__hunt_prey__id_pr__787EE5A0");
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

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<ImbuementDesc>(entity =>
            {
                entity.ToTable("imbuement_desc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdImbuementLevel).HasColumnName("id_imbuement_level");

                entity.Property(e => e.IdImbuementType).HasColumnName("id_imbuement_type");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("value");

                entity.HasOne(d => d.IdImbuementLevelNavigation)
                    .WithMany(p => p.ImbuementDescs)
                    .HasForeignKey(d => d.IdImbuementLevel)
                    .HasConstraintName("FK__imbuement__id_im__619B8048");

                entity.HasOne(d => d.IdImbuementTypeNavigation)
                    .WithMany(p => p.ImbuementDescs)
                    .HasForeignKey(d => d.IdImbuementType)
                    .HasConstraintName("FK__imbuement__id_im__628FA481");
            });

            modelBuilder.Entity<ImbuementItem>(entity =>
            {
                entity.ToTable("imbuement_item");

                entity.HasIndex(e => new { e.IdImbuement, e.IdItem }, "UQ__imbuemen__F46AD8431AB5462E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdImbuement).HasColumnName("id_imbuement");

                entity.Property(e => e.IdImbuementLevel).HasColumnName("id_imbuement_level");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.IdImbuementNavigation)
                    .WithMany(p => p.ImbuementItems)
                    .HasForeignKey(d => d.IdImbuement)
                    .HasConstraintName("FK__imbuement__id_im__68487DD7");

                entity.HasOne(d => d.IdImbuementLevelNavigation)
                    .WithMany(p => p.ImbuementItems)
                    .HasForeignKey(d => d.IdImbuementLevel)
                    .HasConstraintName("FK__imbuement__id_im__693CA210");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.ImbuementItems)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK__imbuement__id_it__6A30C649");
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

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Vocation).HasColumnName("vocation");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("fk_hunt_player");
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
