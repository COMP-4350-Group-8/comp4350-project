﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SailMapper.Data;

#nullable disable

namespace SailMapper.Migrations
{
    [DbContext(typeof(SailDBContext))]
    partial class SailDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("BoatRace", b =>
                {
                    b.Property<int>("ParticipantsId")
                        .HasColumnType("int");

                    b.Property<int>("RacesId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantsId", "RacesId");

                    b.HasIndex("RacesId");

                    b.ToTable("BoatRace");
                });

            modelBuilder.Entity("SailMapper.Classes.Boat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Class")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RatingId")
                        .HasColumnType("int");

                    b.Property<string>("SailNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Skipper")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.ToTable("Boats");
                });

            modelBuilder.Entity("SailMapper.Classes.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SailMapper.Classes.CourseMark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("GateId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsStartLine")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("Latitude")
                        .HasColumnType("float");

                    b.Property<float>("Longitude")
                        .HasColumnType("float");

                    b.Property<bool?>("Rounding")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("GateId");

                    b.ToTable("CourseMarks");
                });

            modelBuilder.Entity("SailMapper.Classes.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RegattaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("RegattaId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("SailMapper.Classes.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Adjustment")
                        .HasColumnType("int");

                    b.Property<int>("BaseRating")
                        .HasColumnType("int");

                    b.Property<int>("CurrentRating")
                        .HasColumnType("int");

                    b.Property<int>("SpinnakerAdjustment")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("SailMapper.Classes.Regatta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Regattas");
                });

            modelBuilder.Entity("SailMapper.Classes.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("CorrectedTime")
                        .HasColumnType("time(6)");

                    b.Property<TimeSpan>("ElapsedTime")
                        .HasColumnType("time(6)");

                    b.Property<int>("FinishPosition")
                        .HasColumnType("int");

                    b.Property<string>("FinishType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoatId");

                    b.HasIndex("RaceId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("SailMapper.Classes.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<float>("Distance")
                        .HasColumnType("float");

                    b.Property<DateTime>("Finished")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GpxData")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Started")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("BoatId");

                    b.HasIndex("RaceId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("BoatRace", b =>
                {
                    b.HasOne("SailMapper.Classes.Boat", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SailMapper.Classes.Race", null)
                        .WithMany()
                        .HasForeignKey("RacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SailMapper.Classes.Boat", b =>
                {
                    b.HasOne("SailMapper.Classes.Rating", "Rating")
                        .WithMany("Boats")
                        .HasForeignKey("RatingId");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("SailMapper.Classes.CourseMark", b =>
                {
                    b.HasOne("SailMapper.Classes.Course", "Course")
                        .WithMany("courseMarks")
                        .HasForeignKey("CourseId");

                    b.HasOne("SailMapper.Classes.CourseMark", "Gate")
                        .WithMany()
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Course");

                    b.Navigation("Gate");
                });

            modelBuilder.Entity("SailMapper.Classes.Race", b =>
                {
                    b.HasOne("SailMapper.Classes.Course", "Course")
                        .WithMany("races")
                        .HasForeignKey("CourseId");

                    b.HasOne("SailMapper.Classes.Regatta", "Regatta")
                        .WithMany("Races")
                        .HasForeignKey("RegattaId");

                    b.Navigation("Course");

                    b.Navigation("Regatta");
                });

            modelBuilder.Entity("SailMapper.Classes.Result", b =>
                {
                    b.HasOne("SailMapper.Classes.Boat", "Boat")
                        .WithMany("Results")
                        .HasForeignKey("BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SailMapper.Classes.Race", "Race")
                        .WithMany("Results")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boat");

                    b.Navigation("Race");
                });

            modelBuilder.Entity("SailMapper.Classes.Track", b =>
                {
                    b.HasOne("SailMapper.Classes.Boat", "Boat")
                        .WithMany("Tracks")
                        .HasForeignKey("BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SailMapper.Classes.Race", "Race")
                        .WithMany("Tracks")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boat");

                    b.Navigation("Race");
                });

            modelBuilder.Entity("SailMapper.Classes.Boat", b =>
                {
                    b.Navigation("Results");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("SailMapper.Classes.Course", b =>
                {
                    b.Navigation("courseMarks");

                    b.Navigation("races");
                });

            modelBuilder.Entity("SailMapper.Classes.Race", b =>
                {
                    b.Navigation("Results");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("SailMapper.Classes.Rating", b =>
                {
                    b.Navigation("Boats");
                });

            modelBuilder.Entity("SailMapper.Classes.Regatta", b =>
                {
                    b.Navigation("Races");
                });
#pragma warning restore 612, 618
        }
    }
}
