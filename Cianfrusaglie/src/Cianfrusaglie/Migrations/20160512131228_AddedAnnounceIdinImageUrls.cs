using Microsoft.Data.Entity.Migrations;

namespace Cianfrusaglie.Migrations {
    public partial class AddedAnnounceIdinImageUrls : Migration {
        protected override void Up( MigrationBuilder migrationBuilder ) {
            migrationBuilder.DropForeignKey( "FK_AnnounceCategory_Announce_AnnounceId", "AnnounceCategory" );
            migrationBuilder.DropForeignKey( "FK_AnnounceCategory_Category_CategoryId", "AnnounceCategory" );
            migrationBuilder.DropForeignKey( "FK_AnnounceFormFieldsValues_Announce_AnnounceId",
                "AnnounceFormFieldsValues" );
            migrationBuilder.DropForeignKey( "FK_AnnounceFormFieldsValues_FormField_FormFieldId",
                "AnnounceFormFieldsValues" );
            migrationBuilder.DropForeignKey( "FK_AnnounceGat_Announce_AnnounceId", "AnnounceGat" );
            migrationBuilder.DropForeignKey( "FK_AnnounceGat_Gat_GatId", "AnnounceGat" );
            migrationBuilder.DropForeignKey( "FK_CategoryFormField_Category_CategoryId", "CategoryFormField" );
            migrationBuilder.DropForeignKey( "FK_CategoryFormField_FormField_FormFieldId", "CategoryFormField" );
            migrationBuilder.DropForeignKey( "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", "AspNetRoleClaims" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserClaim<string>_User_UserId", "AspNetUserClaims" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserLogin<string>_User_UserId", "AspNetUserLogins" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserRole<string>_IdentityRole_RoleId", "AspNetUserRoles" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserRole<string>_User_UserId", "AspNetUserRoles" );
            migrationBuilder.AddForeignKey( "FK_AnnounceCategory_Announce_AnnounceId", "AnnounceCategory", "AnnounceId",
                "Announce", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_AnnounceCategory_Category_CategoryId", "AnnounceCategory", "CategoryId",
                "Category", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_AnnounceFormFieldsValues_Announce_AnnounceId",
                "AnnounceFormFieldsValues", "AnnounceId", "Announce", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_AnnounceFormFieldsValues_FormField_FormFieldId",
                "AnnounceFormFieldsValues", "FormFieldId", "FormField", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_AnnounceGat_Announce_AnnounceId", "AnnounceGat", "AnnounceId",
                "Announce", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_AnnounceGat_Gat_GatId", "AnnounceGat", "GatId", "Gat",
                principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_CategoryFormField_Category_CategoryId", "CategoryFormField",
                "CategoryId", "Category", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_CategoryFormField_FormField_FormFieldId", "CategoryFormField",
                "FormFieldId", "FormField", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", "AspNetRoleClaims",
                "RoleId", "AspNetRoles", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_IdentityUserClaim<string>_User_UserId", "AspNetUserClaims", "UserId",
                "AspNetUsers", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_IdentityUserLogin<string>_User_UserId", "AspNetUserLogins", "UserId",
                "AspNetUsers", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_IdentityUserRole<string>_IdentityRole_RoleId", "AspNetUserRoles",
                "RoleId", "AspNetRoles", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
            migrationBuilder.AddForeignKey( "FK_IdentityUserRole<string>_User_UserId", "AspNetUserRoles", "UserId",
                "AspNetUsers", principalColumn: "Id", onDelete: ReferentialAction.Cascade );
        }

        protected override void Down( MigrationBuilder migrationBuilder ) {
            migrationBuilder.DropForeignKey( "FK_AnnounceCategory_Announce_AnnounceId", "AnnounceCategory" );
            migrationBuilder.DropForeignKey( "FK_AnnounceCategory_Category_CategoryId", "AnnounceCategory" );
            migrationBuilder.DropForeignKey( "FK_AnnounceFormFieldsValues_Announce_AnnounceId",
                "AnnounceFormFieldsValues" );
            migrationBuilder.DropForeignKey( "FK_AnnounceFormFieldsValues_FormField_FormFieldId",
                "AnnounceFormFieldsValues" );
            migrationBuilder.DropForeignKey( "FK_AnnounceGat_Announce_AnnounceId", "AnnounceGat" );
            migrationBuilder.DropForeignKey( "FK_AnnounceGat_Gat_GatId", "AnnounceGat" );
            migrationBuilder.DropForeignKey( "FK_CategoryFormField_Category_CategoryId", "CategoryFormField" );
            migrationBuilder.DropForeignKey( "FK_CategoryFormField_FormField_FormFieldId", "CategoryFormField" );
            migrationBuilder.DropForeignKey( "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", "AspNetRoleClaims" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserClaim<string>_User_UserId", "AspNetUserClaims" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserLogin<string>_User_UserId", "AspNetUserLogins" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserRole<string>_IdentityRole_RoleId", "AspNetUserRoles" );
            migrationBuilder.DropForeignKey( "FK_IdentityUserRole<string>_User_UserId", "AspNetUserRoles" );
            migrationBuilder.AddForeignKey( "FK_AnnounceCategory_Announce_AnnounceId", "AnnounceCategory", "AnnounceId",
                "Announce", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_AnnounceCategory_Category_CategoryId", "AnnounceCategory", "CategoryId",
                "Category", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_AnnounceFormFieldsValues_Announce_AnnounceId",
                "AnnounceFormFieldsValues", "AnnounceId", "Announce", principalColumn: "Id",
                onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_AnnounceFormFieldsValues_FormField_FormFieldId",
                "AnnounceFormFieldsValues", "FormFieldId", "FormField", principalColumn: "Id",
                onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_AnnounceGat_Announce_AnnounceId", "AnnounceGat", "AnnounceId",
                "Announce", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_AnnounceGat_Gat_GatId", "AnnounceGat", "GatId", "Gat",
                principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_CategoryFormField_Category_CategoryId", "CategoryFormField",
                "CategoryId", "Category", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_CategoryFormField_FormField_FormFieldId", "CategoryFormField",
                "FormFieldId", "FormField", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", "AspNetRoleClaims",
                "RoleId", "AspNetRoles", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_IdentityUserClaim<string>_User_UserId", "AspNetUserClaims", "UserId",
                "AspNetUsers", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_IdentityUserLogin<string>_User_UserId", "AspNetUserLogins", "UserId",
                "AspNetUsers", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_IdentityUserRole<string>_IdentityRole_RoleId", "AspNetUserRoles",
                "RoleId", "AspNetRoles", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
            migrationBuilder.AddForeignKey( "FK_IdentityUserRole<string>_User_UserId", "AspNetUserRoles", "UserId",
                "AspNetUsers", principalColumn: "Id", onDelete: ReferentialAction.Restrict );
        }
    }
}