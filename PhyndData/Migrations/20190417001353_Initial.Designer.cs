﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhyndData;

namespace PhyndData.Migrations
{
    [DbContext(typeof(PhyndContext))]
    [Migration("20190417001353_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("PhyndData.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCompleted");

                    b.Property<bool?>("WasWon");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PhyndData.Entities.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GameId");

                    b.Property<int>("Player");

                    b.Property<int>("Position");

                    b.Property<int>("Progress");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("PhyndData.Entities.Weight", b =>
                {
                    b.Property<string>("Scenario")
                        .HasMaxLength(9);

                    b.Property<int>("NextMove");

                    b.Property<int>("Attempts");

                    b.Property<float>("Rank");

                    b.HasKey("Scenario", "NextMove");

                    b.ToTable("Weights");
                });

            modelBuilder.Entity("PhyndData.Entities.Move", b =>
                {
                    b.HasOne("PhyndData.Entities.Game", "Game")
                        .WithMany("Moves")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
