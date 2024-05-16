﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240516212347_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.DocumentationAggregate.Documentation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("ReadOnly")
                        .HasColumnType("bit");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Documentations", (string)null);
                });

            modelBuilder.Entity("Domain.DocumentationAggregate.Documentation", b =>
                {
                    b.OwnsMany("Domain.DocumentationAggregate.Entities.DocumentationHeadingContent", "DocumentationHeadingContents", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("DocumentationHeadingContentId");

                            b1.Property<Guid>("DocumentationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Content")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ContentType")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Position")
                                .HasColumnType("int");

                            b1.HasKey("Id", "DocumentationId");

                            b1.HasIndex("DocumentationId");

                            b1.ToTable("DocumentationHeadingContents", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("DocumentationId");
                        });

                    b.OwnsOne("Domain.DocumentationAggregate.ValueObjects.DocumentationCategory", "Category", b1 =>
                        {
                            b1.Property<Guid>("DocumentationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("DocumentationId");

                            b1.ToTable("Documentations");

                            b1.WithOwner()
                                .HasForeignKey("DocumentationId");
                        });

                    b.Navigation("Category")
                        .IsRequired();

                    b.Navigation("DocumentationHeadingContents");
                });
#pragma warning restore 612, 618
        }
    }
}
