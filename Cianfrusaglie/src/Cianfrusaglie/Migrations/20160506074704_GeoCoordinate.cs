using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace Cianfrusaglie.Migrations
{
    public partial class GeoCoordinate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AnnounceCategory_Announce_AnnounceId", table: "AnnounceCategory");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceCategory_Category_CategoryId", table: "AnnounceCategory");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceFormFieldsValues_Announce_AnnounceId", table: "AnnounceFormFieldsValues");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceFormFieldsValues_FormField_FormFieldId", table: "AnnounceFormFieldsValues");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceGat_Announce_AnnounceId", table: "AnnounceGat");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceGat_Gat_GatId", table: "AnnounceGat");
            migrationBuilder.DropForeignKey(name: "FK_CategoryFormField_Category_CategoryId", table: "CategoryFormField");
            migrationBuilder.DropForeignKey(name: "FK_CategoryFormField_FormField_FormFieldId", table: "CategoryFormField");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_User_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_User_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_User_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "City", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "City", table: "Announce");
            migrationBuilder.CreateTable(
                name: "GeoCoordinateEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCoordinateEntity", x => x.Id);
                });
            migrationBuilder.AddColumn<int>(
                name: "GeoCoordinateId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "GeoCoordinateId",
                table: "Announce",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_Announce_GeoCoordinateEntity_GeoCoordinateId",
                table: "Announce",
                column: "GeoCoordinateId",
                principalTable: "GeoCoordinateEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceCategory_Announce_AnnounceId",
                table: "AnnounceCategory",
                column: "AnnounceId",
                principalTable: "Announce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceCategory_Category_CategoryId",
                table: "AnnounceCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceFormFieldsValues_Announce_AnnounceId",
                table: "AnnounceFormFieldsValues",
                column: "AnnounceId",
                principalTable: "Announce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceFormFieldsValues_FormField_FormFieldId",
                table: "AnnounceFormFieldsValues",
                column: "FormFieldId",
                principalTable: "FormField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceGat_Announce_AnnounceId",
                table: "AnnounceGat",
                column: "AnnounceId",
                principalTable: "Announce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceGat_Gat_GatId",
                table: "AnnounceGat",
                column: "GatId",
                principalTable: "Gat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFormField_Category_CategoryId",
                table: "CategoryFormField",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFormField_FormField_FormFieldId",
                table: "CategoryFormField",
                column: "FormFieldId",
                principalTable: "FormField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_User_GeoCoordinateEntity_GeoCoordinateId",
                table: "AspNetUsers",
                column: "GeoCoordinateId",
                principalTable: "GeoCoordinateEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_User_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_User_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_User_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Announce_GeoCoordinateEntity_GeoCoordinateId", table: "Announce");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceCategory_Announce_AnnounceId", table: "AnnounceCategory");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceCategory_Category_CategoryId", table: "AnnounceCategory");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceFormFieldsValues_Announce_AnnounceId", table: "AnnounceFormFieldsValues");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceFormFieldsValues_FormField_FormFieldId", table: "AnnounceFormFieldsValues");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceGat_Announce_AnnounceId", table: "AnnounceGat");
            migrationBuilder.DropForeignKey(name: "FK_AnnounceGat_Gat_GatId", table: "AnnounceGat");
            migrationBuilder.DropForeignKey(name: "FK_CategoryFormField_Category_CategoryId", table: "CategoryFormField");
            migrationBuilder.DropForeignKey(name: "FK_CategoryFormField_FormField_FormFieldId", table: "CategoryFormField");
            migrationBuilder.DropForeignKey(name: "FK_User_GeoCoordinateEntity_GeoCoordinateId", table: "AspNetUsers");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_User_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_User_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_User_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "GeoCoordinateId", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "GeoCoordinateId", table: "Announce");
            migrationBuilder.DropTable("GeoCoordinateEntity");
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Announce",
                nullable: false,
                defaultValue: "");
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceCategory_Announce_AnnounceId",
                table: "AnnounceCategory",
                column: "AnnounceId",
                principalTable: "Announce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceCategory_Category_CategoryId",
                table: "AnnounceCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceFormFieldsValues_Announce_AnnounceId",
                table: "AnnounceFormFieldsValues",
                column: "AnnounceId",
                principalTable: "Announce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceFormFieldsValues_FormField_FormFieldId",
                table: "AnnounceFormFieldsValues",
                column: "FormFieldId",
                principalTable: "FormField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceGat_Announce_AnnounceId",
                table: "AnnounceGat",
                column: "AnnounceId",
                principalTable: "Announce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_AnnounceGat_Gat_GatId",
                table: "AnnounceGat",
                column: "GatId",
                principalTable: "Gat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFormField_Category_CategoryId",
                table: "CategoryFormField",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFormField_FormField_FormFieldId",
                table: "CategoryFormField",
                column: "FormFieldId",
                principalTable: "FormField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_User_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_User_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_User_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
