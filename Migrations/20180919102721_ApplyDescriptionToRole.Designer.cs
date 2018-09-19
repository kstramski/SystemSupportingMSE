﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Migrations
{
    [DbContext(typeof(SportEventsDbContext))]
    [Migration("20180919102721_ApplyDescriptionToRole")]
    partial class ApplyDescriptionToRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SystemSupportingMSE.Core.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SystemSupportingMSE.Core.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Captain");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("SystemSupportingMSE.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<DateTime>("DateOfRegistration");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SystemSupportingMSE.Core.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("SystemSupportingMSE.Core.Models.UserTeam", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("TeamId");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("UserTeams");
                });

            modelBuilder.Entity("SystemSupportingMSE.Core.Models.UserRole", b =>
                {
                    b.HasOne("SystemSupportingMSE.Core.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SystemSupportingMSE.Core.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SystemSupportingMSE.Core.Models.UserTeam", b =>
                {
                    b.HasOne("SystemSupportingMSE.Core.Models.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SystemSupportingMSE.Core.Models.User", "User")
                        .WithMany("Teams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
