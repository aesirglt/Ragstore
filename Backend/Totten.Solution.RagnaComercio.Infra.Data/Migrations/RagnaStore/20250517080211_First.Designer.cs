﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;

#nullable disable

namespace Totten.Solution.RagnaComercio.Infra.Data.Migrations.RagnaStore
{
    [DbContext(typeof(RagnaStoreContext))]
    [Migration("20250517080211_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation.Agent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Agents", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("89300f33-33d7-4878-af08-1f7b694eca3f"),
                            CreatedAt = new DateTime(2025, 5, 17, 8, 2, 10, 945, DateTimeKind.Utc).AddTicks(4375),
                            IsActive = true,
                            Name = "openkore",
                            ServerId = new Guid("89300f33-33d7-4878-af08-1f7b694eca3f"),
                            UpdatedAt = new DateTime(2025, 5, 17, 8, 2, 10, 945, DateTimeKind.Utc).AddTicks(4375)
                        });
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation.Callback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<int>("StoreType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.HasIndex("UserId");

                    b.ToTable("Callbacks", (string)null);
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation.CallbackSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CallbackId")
                        .HasColumnType("uuid");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Destination")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Sended")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CallbackId");

                    b.ToTable("CallbacksSchedule", (string)null);
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.Servers.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SiteUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Servers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("89300f33-33d7-4878-af08-1f7b694eca3f"),
                            CreatedAt = new DateTime(2025, 5, 17, 8, 2, 10, 944, DateTimeKind.Utc).AddTicks(9640),
                            DisplayName = "Latam RO",
                            IsActive = false,
                            Name = "latamro",
                            SiteUrl = "https://ro.gnjoylatam.com/",
                            UpdatedAt = new DateTime(2025, 5, 17, 8, 2, 10, 944, DateTimeKind.Utc).AddTicks(9642)
                        });
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DiscordUser")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ReceivePriceAlerts")
                        .HasColumnType("boolean");

                    b.Property<long>("SearchCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation.Agent", b =>
                {
                    b.HasOne("Totten.Solution.RagnaComercio.Domain.Features.Servers.Server", "Server")
                        .WithMany("Agents")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation.Callback", b =>
                {
                    b.HasOne("Totten.Solution.RagnaComercio.Domain.Features.Servers.Server", "Server")
                        .WithMany("Callbacks")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Totten.Solution.RagnaComercio.Domain.Features.Users.User", "User")
                        .WithMany("Callbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation.CallbackSchedule", b =>
                {
                    b.HasOne("Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation.Callback", "Callback")
                        .WithMany("CallbackSchedules")
                        .HasForeignKey("CallbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Callback");
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation.Callback", b =>
                {
                    b.Navigation("CallbackSchedules");
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.Servers.Server", b =>
                {
                    b.Navigation("Agents");

                    b.Navigation("Callbacks");
                });

            modelBuilder.Entity("Totten.Solution.RagnaComercio.Domain.Features.Users.User", b =>
                {
                    b.Navigation("Callbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
