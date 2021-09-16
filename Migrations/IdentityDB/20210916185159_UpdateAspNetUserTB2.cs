using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations.IdentityDB
{
    public partial class UpdateAspNetUserTB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRole_AspNetUsers_UserId",
                table: "AppUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
               name: "FK_Customers_AspNetUsers_CustomerID",
               table: "Customers");


            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AppUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRole_AppUsers_UserId",
                table: "AppUserRole",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.AddForeignKey(
               name: "FK_Customers_AppUsers_CustomerID",
               table: "Customers",
               column: "CustomerID",
               principalTable: "AppUsers",
               principalColumn: "Id",
               onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AppUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AppUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AppUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRole_AppUsers_UserId",
                table: "AppUserRole");

            migrationBuilder.DropForeignKey(
               name: "FK_Customers_AppUsers_CustomerID",
               table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AppUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AppUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AppUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.RenameTable(
                name: "AppUsers",
                newName: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRole_AspNetUsers_UserId",
                table: "AppUserRole",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.AddForeignKey(
               name: "FK_Customers_AspNetUsers_CustomerID",
               table: "Customers",
               column: "UserId",
               principalTable: "AspNetUsers",
               principalColumn: "Id",
               onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AppUserRole_AspNetUsers_UserId",
        //        table: "AppUserRole");

        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
        //        table: "AspNetUserClaims");

        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
        //        table: "AspNetUserLogins");

        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
        //        table: "AspNetUserTokens");

        //    migrationBuilder.DropTable(
        //        name: "AspNetUsers");

        //    migrationBuilder.CreateTable(
        //        name: "AppUsers",
        //        columns: table => new
        //        {
        //            Id = table.Column<string>(nullable: false),
        //            UserName = table.Column<string>(maxLength: 256, nullable: true),
        //            NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
        //            Email = table.Column<string>(maxLength: 256, nullable: true),
        //            NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
        //            EmailConfirmed = table.Column<bool>(nullable: false),
        //            PasswordHash = table.Column<string>(nullable: true),
        //            SecurityStamp = table.Column<string>(nullable: true),
        //            ConcurrencyStamp = table.Column<string>(nullable: true),
        //            PhoneNumber = table.Column<string>(nullable: true),
        //            PhoneNumberConfirmed = table.Column<bool>(nullable: false),
        //            TwoFactorEnabled = table.Column<bool>(nullable: false),
        //            LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
        //            LockoutEnabled = table.Column<bool>(nullable: false),
        //            AccessFailedCount = table.Column<int>(nullable: false),
        //            FirstName = table.Column<string>(nullable: true),
        //            LastName = table.Column<string>(nullable: true),
        //            BirthDate = table.Column<DateTime>(nullable: false),
        //            Image = table.Column<string>(nullable: true),
        //            RegisterDate = table.Column<DateTime>(nullable: false),
        //            LastVisitDateTime = table.Column<DateTime>(nullable: false),
        //            IsActive = table.Column<bool>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AppUsers", x => x.Id);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "EmailIndex",
        //        table: "AppUsers",
        //        column: "NormalizedEmail");

        //    migrationBuilder.CreateIndex(
        //        name: "UserNameIndex",
        //        table: "AppUsers",
        //        column: "NormalizedUserName",
        //        unique: true,
        //        filter: "[NormalizedUserName] IS NOT NULL");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AppUserRole_AppUsers_UserId",
        //        table: "AppUserRole",
        //        column: "UserId",
        //        principalTable: "AppUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AspNetUserClaims_AppUsers_UserId",
        //        table: "AspNetUserClaims",
        //        column: "UserId",
        //        principalTable: "AppUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AspNetUserLogins_AppUsers_UserId",
        //        table: "AspNetUserLogins",
        //        column: "UserId",
        //        principalTable: "AppUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AspNetUserTokens_AppUsers_UserId",
        //        table: "AspNetUserTokens",
        //        column: "UserId",
        //        principalTable: "AppUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);
        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AppUserRole_AppUsers_UserId",
        //        table: "AppUserRole");

        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AspNetUserClaims_AppUsers_UserId",
        //        table: "AspNetUserClaims");

        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AspNetUserLogins_AppUsers_UserId",
        //        table: "AspNetUserLogins");

        //    migrationBuilder.DropForeignKey(
        //        name: "FK_AspNetUserTokens_AppUsers_UserId",
        //        table: "AspNetUserTokens");

        //    migrationBuilder.DropTable(
        //        name: "AppUsers");

        //    migrationBuilder.CreateTable(
        //        name: "AspNetUsers",
        //        columns: table => new
        //        {
        //            Id = table.Column<string>(nullable: false),
        //            AccessFailedCount = table.Column<int>(nullable: false),
        //            BirthDate = table.Column<DateTime>(nullable: false),
        //            ConcurrencyStamp = table.Column<string>(nullable: true),
        //            Email = table.Column<string>(maxLength: 256, nullable: true),
        //            EmailConfirmed = table.Column<bool>(nullable: false),
        //            FirstName = table.Column<string>(nullable: true),
        //            LastName = table.Column<string>(nullable: true),
        //            LockoutEnabled = table.Column<bool>(nullable: false),
        //            LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
        //            NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
        //            NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
        //            PasswordHash = table.Column<string>(nullable: true),
        //            PhoneNumber = table.Column<string>(nullable: true),
        //            PhoneNumberConfirmed = table.Column<bool>(nullable: false),
        //            SecurityStamp = table.Column<string>(nullable: true),
        //            TwoFactorEnabled = table.Column<bool>(nullable: false),
        //            UserName = table.Column<string>(maxLength: 256, nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetUsers", x => x.Id);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "EmailIndex",
        //        table: "AspNetUsers",
        //        column: "NormalizedEmail");

        //    migrationBuilder.CreateIndex(
        //        name: "UserNameIndex",
        //        table: "AspNetUsers",
        //        column: "NormalizedUserName",
        //        unique: true,
        //        filter: "[NormalizedUserName] IS NOT NULL");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AppUserRole_AspNetUsers_UserId",
        //        table: "AppUserRole",
        //        column: "UserId",
        //        principalTable: "AspNetUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
        //        table: "AspNetUserClaims",
        //        column: "UserId",
        //        principalTable: "AspNetUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
        //        table: "AspNetUserLogins",
        //        column: "UserId",
        //        principalTable: "AspNetUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
        //        table: "AspNetUserTokens",
        //        column: "UserId",
        //        principalTable: "AspNetUsers",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);
        //}
    }
}
