﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nava.Data;

#nullable disable

namespace Nava.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231221160957_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Nava.Entities.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CommentedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserInteractionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserInteractionID");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("Nava.Entities.DownloadedMusic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("MusicID")
                        .HasColumnType("int");

                    b.Property<int>("UserInfoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MusicID");

                    b.HasIndex("UserInfoID");

                    b.ToTable("downloadedMusics");
                });

            modelBuilder.Entity("Nava.Entities.Music", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPlays")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("musics");
                });

            modelBuilder.Entity("Nava.Entities.PlayList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("playLists");
                });

            modelBuilder.Entity("Nava.Entities.PlayListMusic", b =>
                {
                    b.Property<int>("PlayListId")
                        .HasColumnType("int");

                    b.Property<int>("MusicId")
                        .HasColumnType("int");

                    b.HasKey("PlayListId", "MusicId");

                    b.HasIndex("MusicId");

                    b.ToTable("playListMusics");
                });

            modelBuilder.Entity("Nava.Entities.UserInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("userInfos");
                });

            modelBuilder.Entity("Nava.Entities.UserInteraction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("musicID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("musicID");

                    b.ToTable("userInteractions");
                });

            modelBuilder.Entity("Nava.Entities.Comment", b =>
                {
                    b.HasOne("Nava.Entities.UserInteraction", "UserInteraction")
                        .WithMany("Comments")
                        .HasForeignKey("UserInteractionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInteraction");
                });

            modelBuilder.Entity("Nava.Entities.DownloadedMusic", b =>
                {
                    b.HasOne("Nava.Entities.Music", "Music")
                        .WithMany("downloadedMusics")
                        .HasForeignKey("MusicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nava.Entities.UserInfo", "UserInfo")
                        .WithMany("DownloadedMusics")
                        .HasForeignKey("UserInfoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Nava.Entities.PlayList", b =>
                {
                    b.HasOne("Nava.Entities.UserInfo", "Owner")
                        .WithMany("PlayList")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Nava.Entities.PlayListMusic", b =>
                {
                    b.HasOne("Nava.Entities.Music", "Music")
                        .WithMany("PlayListMusics")
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nava.Entities.PlayList", "PlayList")
                        .WithMany("PlayListMusics")
                        .HasForeignKey("PlayListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("PlayList");
                });

            modelBuilder.Entity("Nava.Entities.UserInteraction", b =>
                {
                    b.HasOne("Nava.Entities.UserInfo", "User")
                        .WithMany("UserInteractions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nava.Entities.Music", "music")
                        .WithMany("UserInteractions")
                        .HasForeignKey("musicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("music");
                });

            modelBuilder.Entity("Nava.Entities.Music", b =>
                {
                    b.Navigation("PlayListMusics");

                    b.Navigation("UserInteractions");

                    b.Navigation("downloadedMusics");
                });

            modelBuilder.Entity("Nava.Entities.PlayList", b =>
                {
                    b.Navigation("PlayListMusics");
                });

            modelBuilder.Entity("Nava.Entities.UserInfo", b =>
                {
                    b.Navigation("DownloadedMusics");

                    b.Navigation("PlayList");

                    b.Navigation("UserInteractions");
                });

            modelBuilder.Entity("Nava.Entities.UserInteraction", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
