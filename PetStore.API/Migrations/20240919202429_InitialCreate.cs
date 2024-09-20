﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetStore.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "petstore");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "petstore",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "petstore",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    CustomerName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CustomerSurname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ShippingAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IPInfoAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OrderStatus = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ExternalReferenceId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Toy",
                schema: "petstore",
                columns: table => new
                {
                    ToyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toy", x => x.ToyId);
                    table.ForeignKey(
                        name: "Toy_Category",
                        column: x => x.CategoryId,
                        principalSchema: "petstore",
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "petstore",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    ToyId = table.Column<int>(type: "integer", nullable: true),
                    DatePosted = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Author = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "Comment_Toy",
                        column: x => x.ToyId,
                        principalSchema: "petstore",
                        principalTable: "Toy",
                        principalColumn: "ToyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "petstore",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ToyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "OrderItem_Order",
                        column: x => x.OrderId,
                        principalSchema: "petstore",
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "OrderItem_Toy",
                        column: x => x.ToyId,
                        principalSchema: "petstore",
                        principalTable: "Toy",
                        principalColumn: "ToyId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "fki_Comment_Toy",
                schema: "petstore",
                table: "Comment",
                column: "ToyId");

            migrationBuilder.CreateIndex(
                name: "fki_OrderItem_Order",
                schema: "petstore",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "fki_OrderItem_Toy",
                schema: "petstore",
                table: "OrderItem",
                column: "ToyId");

            migrationBuilder.CreateIndex(
                name: "fki_OrderItem_Category",
                schema: "petstore",
                table: "Toy",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment",
                schema: "petstore");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "petstore");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "petstore");

            migrationBuilder.DropTable(
                name: "Toy",
                schema: "petstore");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "petstore");
        }
    }
}