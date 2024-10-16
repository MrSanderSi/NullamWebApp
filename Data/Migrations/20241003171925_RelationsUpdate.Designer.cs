﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NullamWebApp.Data;

#nullable disable

namespace NullamWebApp.Data.Migrations
{
    [DbContext(typeof(NullamDbContext))]
    [Migration("20241003171925_RelationsUpdate")]
    partial class RelationsUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NullamWebApp.Data.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalLines")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("EventEnds")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("EventStarts")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.ParticipantCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistryCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ParticipantCompanies");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.ParticipantEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParticipantCompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ParticipantCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParticipantPersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ParticipantCompanyId")
                        .IsUnique()
                        .HasFilter("[ParticipantCompanyId] IS NOT NULL");

                    b.HasIndex("ParticipantPersonId")
                        .IsUnique()
                        .HasFilter("[ParticipantPersonId] IS NOT NULL");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.ParticipantPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("IdCode")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParticipantPeople");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.Event", b =>
                {
                    b.HasOne("NullamWebApp.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.ParticipantEntry", b =>
                {
                    b.HasOne("NullamWebApp.Data.Models.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NullamWebApp.Data.Models.ParticipantCompany", "Company")
                        .WithOne("ParticipantEntry")
                        .HasForeignKey("NullamWebApp.Data.Models.ParticipantEntry", "ParticipantCompanyId");

                    b.HasOne("NullamWebApp.Data.Models.ParticipantPerson", "Person")
                        .WithOne("ParticipantEntry")
                        .HasForeignKey("NullamWebApp.Data.Models.ParticipantEntry", "ParticipantPersonId");

                    b.Navigation("Company");

                    b.Navigation("Event");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.Event", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.ParticipantCompany", b =>
                {
                    b.Navigation("ParticipantEntry");
                });

            modelBuilder.Entity("NullamWebApp.Data.Models.ParticipantPerson", b =>
                {
                    b.Navigation("ParticipantEntry");
                });
#pragma warning restore 612, 618
        }
    }
}
