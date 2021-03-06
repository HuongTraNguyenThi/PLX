// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PLX.Persistence.EF.Context;

namespace PLX.Persistence.EF.Migrations
{
    [DbContext(typeof(PLXDbContext))]
    [Migration("20210708171112_AddColumnTaleLinkedCard")]
    partial class AddColumnTaleLinkedCard
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PLX.Persistence.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CardID")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<int>("CustomerTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.Property<string>("TaxCode")
                        .HasColumnType("text");

                    b.Property<int>("WardId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTypeId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("WardId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("PLX.Persistence.Model.CustomerLog", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp");

                    b.HasKey("CustomerId", "Time");

                    b.ToTable("CustomerLog");
                });

            modelBuilder.Entity("PLX.Persistence.Model.CustomerQuestion", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.HasKey("CustomerId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("CustomerQuestion");
                });

            modelBuilder.Entity("PLX.Persistence.Model.CustomerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CustomerType");
                });

            modelBuilder.Entity("PLX.Persistence.Model.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("PLX.Persistence.Model.LinkedCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("LinkedCard");
                });

            modelBuilder.Entity("PLX.Persistence.Model.LogAPI", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApiName")
                        .HasColumnType("text");

                    b.Property<string>("Channel")
                        .HasColumnType("text");

                    b.Property<string>("ContentRequest")
                        .HasColumnType("text");

                    b.Property<string>("Device")
                        .HasColumnType("text");

                    b.Property<string>("RequestId")
                        .HasColumnType("text");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("ResponseTime")
                        .HasColumnType("timestamp");

                    b.Property<string>("ResultCode")
                        .HasColumnType("text");

                    b.Property<string>("ResultMessage")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LogAPI");
                });

            modelBuilder.Entity("PLX.Persistence.Model.OTP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp");

                    b.Property<string>("OTPCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TransactionType")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OTP");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Province");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Result");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LicensePlate")
                        .IsUnique();

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("PLX.Persistence.Model.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("VehicleType");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Ward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Ward");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Customer", b =>
                {
                    b.HasOne("PLX.Persistence.Model.CustomerType", "CustomerType")
                        .WithMany()
                        .HasForeignKey("CustomerTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PLX.Persistence.Model.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PLX.Persistence.Model.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PLX.Persistence.Model.Ward", "Ward")
                        .WithMany()
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerType");

                    b.Navigation("District");

                    b.Navigation("Province");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("PLX.Persistence.Model.CustomerLog", b =>
                {
                    b.HasOne("PLX.Persistence.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PLX.Persistence.Model.CustomerQuestion", b =>
                {
                    b.HasOne("PLX.Persistence.Model.Customer", "Customer")
                        .WithMany("Questions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PLX.Persistence.Model.Question", "Question")
                        .WithMany("CustomerQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("PLX.Persistence.Model.District", b =>
                {
                    b.HasOne("PLX.Persistence.Model.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("PLX.Persistence.Model.LinkedCard", b =>
                {
                    b.HasOne("PLX.Persistence.Model.Customer", "Customer")
                        .WithMany("LinkedCards")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Vehicle", b =>
                {
                    b.HasOne("PLX.Persistence.Model.Customer", "Customer")
                        .WithMany("Vehicles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PLX.Persistence.Model.VehicleType", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Ward", b =>
                {
                    b.HasOne("PLX.Persistence.Model.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Customer", b =>
                {
                    b.Navigation("LinkedCards");

                    b.Navigation("Questions");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("PLX.Persistence.Model.District", b =>
                {
                    b.Navigation("Wards");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Province", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("PLX.Persistence.Model.Question", b =>
                {
                    b.Navigation("CustomerQuestions");
                });
#pragma warning restore 612, 618
        }
    }
}
