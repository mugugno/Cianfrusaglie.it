using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace Cianfrusaglie.Migrations
{
    public partial class FeedBack : Migration
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
                name: "FeedBack",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnnounceId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    SenderId = table.Column<string>(nullable: false),
                    SenderId1 = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Vote = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedBack_Announce_AnnounceId",
                        column: x => x.AnnounceId,
                        principalTable: "Announce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedBack_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedBack_User_SenderId1",
                        column: x => x.SenderId1,
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
            migrationBuilder.DropTable("FeedBack");
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
