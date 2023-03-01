﻿// <auto-generated />
using System;
using FreshRecipes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FreshRecipes.Migrations
{
    [DbContext(typeof(FreshRecipesDBContext))]
    [Migration("20230301075139_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FreshRecipes.Models.Recipe", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("amount")
                        .HasColumnType("integer");

                    b.Property<string>("city")
                        .HasColumnType("text");

                    b.Property<string>("recipe")
                        .HasColumnType("text");

                    b.Property<string>("restaurant")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}
