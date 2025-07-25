﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VisionOfChosen_BE.Infra.Context;

#nullable disable

namespace VisionOfChosen_BE.Migrations
{
    [DbContext(typeof(VisionOfChosen_Context))]
    [Migration("20250716204753_create-scan-detail-table")]
    partial class createscandetailtable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.7");

            modelBuilder.Entity("VisionOfChosen_BE.Infra.Models.AiChatHistory", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("message");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("role");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("session_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT")
                        .HasColumnName("timestamp");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.Property<string>("created_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("TEXT");

                    b.Property<bool>("deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("deleted_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("deleted_on")
                        .HasColumnType("TEXT");

                    b.Property<string>("modified_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modified_on")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("ai_chat_history");
                });

            modelBuilder.Entity("VisionOfChosen_BE.Infra.Models.Drift", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AfterStateJson")
                        .HasColumnType("TEXT");

                    b.Property<string>("AiAction")
                        .HasColumnType("TEXT");

                    b.Property<string>("AiExplanation")
                        .HasColumnType("TEXT");

                    b.Property<string>("BeforeStateJson")
                        .HasColumnType("TEXT");

                    b.Property<string>("DriftCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResourceName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResourceType")
                        .HasColumnType("TEXT");

                    b.Property<string>("RiskLevel")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScanDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("created_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("TEXT");

                    b.Property<bool>("deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("deleted_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("deleted_on")
                        .HasColumnType("TEXT");

                    b.Property<string>("modified_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modified_on")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("ScanDetailId");

                    b.ToTable("Drifts");
                });

            modelBuilder.Entity("VisionOfChosen_BE.Infra.Models.Event", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Changer")
                        .HasColumnType("TEXT")
                        .HasColumnName("changer");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Service")
                        .HasColumnType("TEXT")
                        .HasColumnName("service");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("status");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT")
                        .HasColumnName("time");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.Property<string>("created_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("TEXT");

                    b.Property<bool>("deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("deleted_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("deleted_on")
                        .HasColumnType("TEXT");

                    b.Property<string>("modified_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modified_on")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("event", t =>
                        {
                            t.HasComment("Bảng thống kê");
                        });
                });

            modelBuilder.Entity("VisionOfChosen_BE.Infra.Models.Scan", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AddedResources")
                        .HasColumnType("INTEGER")
                        .HasColumnName("added_resources");

                    b.Property<int>("ChangedResources")
                        .HasColumnType("INTEGER")
                        .HasColumnName("changed_resources");

                    b.Property<int>("DestroyedResources")
                        .HasColumnType("INTEGER")
                        .HasColumnName("destroyed_resources");

                    b.Property<string>("Directory")
                        .HasColumnType("TEXT")
                        .HasColumnName("directory");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.Property<DateTime>("ScanTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("scan_time");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT")
                        .HasColumnName("status");

                    b.Property<string>("created_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("TEXT");

                    b.Property<bool>("deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("deleted_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("deleted_on")
                        .HasColumnType("TEXT");

                    b.Property<string>("modified_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modified_on")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("scan");
                });

            modelBuilder.Entity("VisionOfChosen_BE.Infra.Models.ScanDetail", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<int>("DriftCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RiskLevel")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScanDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalResources")
                        .HasColumnType("INTEGER");

                    b.Property<string>("created_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("TEXT");

                    b.Property<bool>("deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("deleted_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("deleted_on")
                        .HasColumnType("TEXT");

                    b.Property<string>("modified_by")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modified_on")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("ScanDetails");
                });

            modelBuilder.Entity("VisionOfChosen_BE.Infra.Models.Drift", b =>
                {
                    b.HasOne("VisionOfChosen_BE.Infra.Models.ScanDetail", "ScanDetail")
                        .WithMany("Drifts")
                        .HasForeignKey("ScanDetailId");

                    b.Navigation("ScanDetail");
                });

            modelBuilder.Entity("VisionOfChosen_BE.Infra.Models.ScanDetail", b =>
                {
                    b.Navigation("Drifts");
                });
#pragma warning restore 612, 618
        }
    }
}
