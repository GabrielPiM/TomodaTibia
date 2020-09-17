using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using TomodaTibiaModels.DB;

namespace TomodaTibiaAPI.DBContext
{
    public partial class TomodaTibiaContext : DbContext
    {

       
   
        public TomodaTibiaContext(DbContextOptions<TomodaTibiaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Equipamento> Equipamento { get; set; }
        public virtual DbSet<Hunt> Hunt { get; set; }
        public virtual DbSet<HuntMonstro> HuntMonstro { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Monstro> Monstro { get; set; }
        public virtual DbSet<OutroItem> OutroItem { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Vocacao> Vocacao { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("autor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeMainChar)
                    .IsRequired()
                    .HasColumnName("nome_main_char")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnName("senha")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UrlSocial)
                    .HasColumnName("url_social")
                    .HasMaxLength(2048)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Equipamento>(entity =>
            {
                entity.ToTable("equipamento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ammo)
                    .HasColumnName("ammo")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_ammo.gif')");

                entity.Property(e => e.Amulet)
                    .HasColumnName("amulet")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_amulet.gif')");

                entity.Property(e => e.Armor)
                    .HasColumnName("armor")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_armor.gif')");

                entity.Property(e => e.Bag)
                    .HasColumnName("bag")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_bag.gif')");

                entity.Property(e => e.Boots)
                    .HasColumnName("boots")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_boots.gif')");

                entity.Property(e => e.Helmet)
                    .HasColumnName("helmet")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_helmet.gif')");

                entity.Property(e => e.IdPlayer).HasColumnName("id_player");

                entity.Property(e => e.Legs)
                    .HasColumnName("legs")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_legs.gif')");

                entity.Property(e => e.Ring)
                    .HasColumnName("ring")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_ring.gif')");

                entity.Property(e => e.WeaponLeft)
                    .HasColumnName("weapon_left")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_weapon_left.gif')");

                entity.Property(e => e.WeaponRight)
                    .HasColumnName("weapon_right")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no_weapon_right.gif')");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.Equipamento)
                    .HasForeignKey(d => d.IdPlayer)
                    .HasConstraintName("FK__equipamen__id_pl__09A971A2");
            });

            modelBuilder.Entity<Hunt>(entity =>
            {
                entity.ToTable("hunt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DescHunt)
                    .HasColumnName("desc_hunt")
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Sem descrição.')");

                entity.Property(e => e.IdAutor).HasColumnName("id_autor");

                entity.Property(e => e.IsValid)
                    .HasColumnName("is_valid")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NivelMinReq).HasColumnName("nivel_min_req");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Premium)
                    .HasColumnName("premium")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QtdPlayer)
                    .HasColumnName("qtd_player")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.VideoTutorial)
                    .HasColumnName("video_tutorial")
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Sem video tutorial.')");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Hunt)
                    .HasForeignKey(d => d.IdAutor)
                    .HasConstraintName("FK__hunt__id_autor__778AC167");
            });

            modelBuilder.Entity<HuntMonstro>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("hunt_monstro");

                entity.HasIndex(e => new { e.IdHunt, e.IdMonstro })
                    .HasName("UQ__hunt_mon__A736D1B912FD4F2E")
                    .IsUnique();

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdMonstro).HasColumnName("id_monstro");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("FK__hunt_mons__id_hu__01142BA1");

                entity.HasOne(d => d.IdMonstroNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMonstro)
                    .HasConstraintName("FK__hunt_mons__id_mo__02084FDA");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Monstro>(entity =>
            {
                entity.ToTable("monstro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OutroItem>(entity =>
            {
                entity.ToTable("outro_item");

                entity.HasIndex(e => new { e.IdHunt, e.IdItem })
                    .HasName("UQ__outro_it__14D7D3B655F4EE38")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.Qtd)
                    .HasColumnName("qtd")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.OutroItem)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("FK__outro_ite__id_hu__160F4887");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.OutroItem)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK__outro_ite__id_it__17036CC0");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHunt).HasColumnName("id_hunt");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.Vocacao).HasColumnName("vocacao");

                entity.HasOne(d => d.IdHuntNavigation)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.IdHunt)
                    .HasConstraintName("FK__player__id_hunt__06CD04F7");
            });

            modelBuilder.Entity<Vocacao>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vocacao");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NomeVocacao)
                    .IsRequired()
                    .HasColumnName("nome_vocacao")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
