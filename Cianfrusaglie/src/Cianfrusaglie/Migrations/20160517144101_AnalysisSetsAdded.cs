using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace Cianfrusaglie.Migrations
{
    public partial class AnalysisSetsAdded : Migration
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
            migrationBuilder.CreateTable(
                name: "UserCategoryPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategoryPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCategoryPreferences_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategoryPreferences_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "UserGatHistogram",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    GatId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGatHistogram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGatHistogram_Gat_GatId",
                        column: x => x.GatId,
                        principalTable: "Gat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGatHistogram_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
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
            migrationBuilder.DropTable("UserCategoryPreferences");
            migrationBuilder.DropTable("UserGatHistogram");
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
