using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Cianfrusaglie.Migrations
{
    public partial class FeedBack_keyfix : Migration
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
            migrationBuilder.DropForeignKey(name: "FK_FeedBack_User_SenderId1", table: "FeedBack");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_User_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_User_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_User_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "SenderId1", table: "FeedBack");
            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "FeedBack",
                nullable: true);
            migrationBuilder.AlterColumn<int>(
                name: "AnnounceId",
                table: "FeedBack",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "FeedBack",
                nullable: true);
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
                name: "FK_FeedBack_User_ReceiverId",
                table: "FeedBack",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
            migrationBuilder.DropForeignKey(name: "FK_FeedBack_User_ReceiverId", table: "FeedBack");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_User_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_User_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_User_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "ReceiverId", table: "FeedBack");
            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "FeedBack",
                nullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "AnnounceId",
                table: "FeedBack",
                nullable: false);
            migrationBuilder.AddColumn<string>(
                name: "SenderId1",
                table: "FeedBack",
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
                name: "FK_FeedBack_User_SenderId1",
                table: "FeedBack",
                column: "SenderId1",
                principalTable: "AspNetUsers",
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
