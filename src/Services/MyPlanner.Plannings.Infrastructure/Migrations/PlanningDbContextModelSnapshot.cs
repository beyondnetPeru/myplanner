﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPlanner.Plannings.Infrastructure.Database;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    [DbContext(typeof(PlanningDbContext))]
    partial class PlanningDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("myplanner-plannings")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyPlanner.IntegrationEventLogEF.IntegrationEventLogEntry", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("TimesSent")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventId");

                    b.ToTable("IntegrationEventLog", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.ErrorTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("errors", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanCategoryTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("plancategory", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanItemTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("BallParkCostAmount")
                        .HasColumnType("float");

                    b.Property<int>("BallParkCostSymbol")
                        .HasColumnType("int");

                    b.Property<double>("BallParkTotalCostAmount")
                        .HasColumnType("float");

                    b.Property<double>("BallparkDependenciesCostAmount")
                        .HasColumnType("float");

                    b.Property<string>("BusinessFeatureComplexityLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessFeatureDefinition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessFeatureMoScoW")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessFeatureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BusinessFeaturePriority")
                        .HasColumnType("int");

                    b.Property<string>("ComponentsImpacted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyAssumptions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanCategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeModelTypeItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TechnicalDefinition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TechnicalDependencies")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("planitems", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeModelTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SizeModelTypeId");

                    b.ToTable("plans", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelItemTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FactorSelected")
                        .HasColumnType("int");

                    b.Property<bool>("IsStandard")
                        .HasColumnType("bit");

                    b.Property<int>("ProfileAvgRateSymbol")
                        .HasColumnType("int");

                    b.Property<double>("ProfileAvgRateValue")
                        .HasColumnType("float");

                    b.Property<string>("ProfileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SizeModelId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SizeModelTypeItemCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeModelTypeItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalCost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SizeModelId");

                    b.HasIndex("SizeModelTypeItemId");

                    b.ToTable("sizemodelitems", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeModelTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeModelTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("sizemodels", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTypeItemTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeModelTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SizeModelTypeId");

                    b.ToTable("sizemodeltypeitems", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTypeTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("sizemodeltypes", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Shared.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("requests", "myplanner-plannings");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanCategoryTable", b =>
                {
                    b.HasOne("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanTable", "Plan")
                        .WithMany("Categories")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanItemTable", b =>
                {
                    b.HasOne("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanTable", "Plan")
                        .WithMany("Items")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MyPlanner.Shared.Infrastructure.Database.AuditTable", "Audit", b1 =>
                        {
                            b1.Property<string>("PlanItemTableId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CreatedBy");

                            b1.Property<string>("TimeSpan")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TimeSpan");

                            b1.Property<DateTime?>("UpdatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("UpdatedAt");

                            b1.Property<string>("UpdatedBy")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("UpdatedBy");

                            b1.HasKey("PlanItemTableId");

                            b1.ToTable("planitems", "myplanner-plannings");

                            b1.WithOwner()
                                .HasForeignKey("PlanItemTableId");
                        });

                    b.Navigation("Audit")
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanTable", b =>
                {
                    b.HasOne("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTypeTable", "SizeModelType")
                        .WithMany()
                        .HasForeignKey("SizeModelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MyPlanner.Shared.Infrastructure.Database.AuditTable", "Audit", b1 =>
                        {
                            b1.Property<string>("PlanTableId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CreatedBy");

                            b1.Property<string>("TimeSpan")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TimeSpan");

                            b1.Property<DateTime?>("UpdatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("UpdatedAt");

                            b1.Property<string>("UpdatedBy")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("UpdatedBy");

                            b1.HasKey("PlanTableId");

                            b1.ToTable("plans", "myplanner-plannings");

                            b1.WithOwner()
                                .HasForeignKey("PlanTableId");
                        });

                    b.Navigation("Audit")
                        .IsRequired();

                    b.Navigation("SizeModelType");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelItemTable", b =>
                {
                    b.HasOne("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTable", "SizeModel")
                        .WithMany("Items")
                        .HasForeignKey("SizeModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTypeItemTable", "SizeModelTypeItem")
                        .WithMany()
                        .HasForeignKey("SizeModelTypeItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MyPlanner.Shared.Infrastructure.Database.AuditTable", "Audit", b1 =>
                        {
                            b1.Property<string>("SizeModelItemTableId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CreatedBy");

                            b1.Property<string>("TimeSpan")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TimeSpan");

                            b1.Property<DateTime?>("UpdatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("UpdatedAt");

                            b1.Property<string>("UpdatedBy")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("UpdatedBy");

                            b1.HasKey("SizeModelItemTableId");

                            b1.ToTable("sizemodelitems", "myplanner-plannings");

                            b1.WithOwner()
                                .HasForeignKey("SizeModelItemTableId");
                        });

                    b.Navigation("Audit")
                        .IsRequired();

                    b.Navigation("SizeModel");

                    b.Navigation("SizeModelTypeItem");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTable", b =>
                {
                    b.OwnsOne("MyPlanner.Shared.Infrastructure.Database.AuditTable", "Audit", b1 =>
                        {
                            b1.Property<string>("SizeModelTableId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CreatedBy");

                            b1.Property<string>("TimeSpan")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TimeSpan");

                            b1.Property<DateTime?>("UpdatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("UpdatedAt");

                            b1.Property<string>("UpdatedBy")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("UpdatedBy");

                            b1.HasKey("SizeModelTableId");

                            b1.ToTable("sizemodels", "myplanner-plannings");

                            b1.WithOwner()
                                .HasForeignKey("SizeModelTableId");
                        });

                    b.Navigation("Audit")
                        .IsRequired();
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTypeItemTable", b =>
                {
                    b.HasOne("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTypeTable", "SizeModelType")
                        .WithMany("Items")
                        .HasForeignKey("SizeModelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SizeModelType");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.PlanTable", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTable", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("MyPlanner.Plannings.Infrastructure.Database.Tables.SizeModelTypeTable", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}