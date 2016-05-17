using System;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace Cianfrusaglie.Migrations {
    public partial class Initial_Redo6 : Migration {
        protected override void Up( MigrationBuilder migrationBuilder ) {
            migrationBuilder.CreateTable( "Category",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        Name = table.Column< string >( nullable: false ),
                        OverCategoryId = table.Column< int >( nullable: true )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_Category", x => x.Id );
                        table.ForeignKey( "FK_Category_Category_OverCategoryId", x => x.OverCategoryId, "Category",
                            "Id", onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateTable( "FormField",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        Name = table.Column< string >( nullable: false ),
                        Type = table.Column< int >( nullable: false )
                    },
                constraints: table => { table.PrimaryKey( "PK_FormField", x => x.Id ); } );
            migrationBuilder.CreateTable( "Gat",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        Text = table.Column< string >( nullable: false )
                    },
                constraints: table => { table.PrimaryKey( "PK_Gat", x => x.Id ); } );
            migrationBuilder.CreateTable( "AspNetUsers",
                table =>
                    new {
                        Id = table.Column< string >( nullable: false ),
                        AccessFailedCount = table.Column< int >( nullable: false ),
                        BirthDate = table.Column< DateTime >( nullable: false ),
                        ConcurrencyStamp = table.Column< string >( nullable: true ),
                        Email = table.Column< string >( nullable: true ),
                        EmailConfirmed = table.Column< bool >( nullable: false ),
                        Latitude = table.Column< double >( nullable: false ),
                        LockoutEnabled = table.Column< bool >( nullable: false ),
                        LockoutEnd = table.Column< DateTimeOffset >( nullable: true ),
                        Longitude = table.Column< double >( nullable: false ),
                        NormalizedEmail = table.Column< string >( nullable: true ),
                        NormalizedUserName = table.Column< string >( nullable: true ),
                        PasswordHash = table.Column< string >( nullable: true ),
                        PhoneNumber = table.Column< string >( nullable: true ),
                        PhoneNumberConfirmed = table.Column< bool >( nullable: false ),
                        RememberMe = table.Column< bool >( nullable: false ),
                        SecurityStamp = table.Column< string >( nullable: true ),
                        TwoFactorEnabled = table.Column< bool >( nullable: false ),
                        UserId = table.Column< string >( nullable: true ),
                        UserName = table.Column< string >( nullable: true )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_User", x => x.Id );
                        table.ForeignKey( "FK_User_User_UserId", x => x.UserId, "AspNetUsers", "Id",
                            onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateTable( "AspNetRoles",
                table =>
                    new {
                        Id = table.Column< string >( nullable: false ),
                        ConcurrencyStamp = table.Column< string >( nullable: true ),
                        Name = table.Column< string >( nullable: true ),
                        NormalizedName = table.Column< string >( nullable: true )
                    },
                constraints: table => { table.PrimaryKey( "PK_IdentityRole", x => x.Id ); } );
            migrationBuilder.CreateTable( "CategoryFormField",
                table =>
                    new {
                        FormFieldId = table.Column< int >( nullable: false ),
                        CategoryId = table.Column< int >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_CategoryFormField", x => new {x.FormFieldId, x.CategoryId} );
                        table.ForeignKey( "FK_CategoryFormField_Category_CategoryId", x => x.CategoryId, "Category",
                            "Id", onDelete: ReferentialAction.Cascade );
                        table.ForeignKey( "FK_CategoryFormField_FormField_FormFieldId", x => x.FormFieldId,
                            "FormField", "Id", onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "FieldDefaultValue",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        FormFieldId = table.Column< int >( nullable: false ),
                        Value = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_FieldDefaultValue", x => x.Id );
                        table.ForeignKey( "FK_FieldDefaultValue_FormField_FormFieldId", x => x.FormFieldId,
                            "FormField", "Id", onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateTable( "Announce",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        AuthorId = table.Column< string >( nullable: false ),
                        Closed = table.Column< bool >( nullable: false ),
                        DeadLine = table.Column< DateTime >( nullable: true ),
                        Description = table.Column< string >( nullable: true ),
                        Discriminator = table.Column< string >( nullable: false ),
                        Latitude = table.Column< double >( nullable: false ),
                        Longitude = table.Column< double >( nullable: false ),
                        MeterRange = table.Column< int >( nullable: false ),
                        Price = table.Column< int >( nullable: false ),
                        PriceRange = table.Column< int >( nullable: false ),
                        PublishDate = table.Column< DateTime >( nullable: false ),
                        Title = table.Column< string >( nullable: false ),
                        ObjectText = table.Column< string >( nullable: true )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_Announce", x => x.Id );
                        table.ForeignKey( "FK_Announce_User_AuthorId", x => x.AuthorId, "AspNetUsers", "Id",
                            onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateTable( "Message",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        DateTime = table.Column< DateTime >( nullable: false ),
                        ReceiverId = table.Column< string >( nullable: false ),
                        SenderId = table.Column< string >( nullable: false ),
                        Text = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_Message", x => x.Id );
                        table.ForeignKey( "FK_Message_User_ReceiverId", x => x.ReceiverId, "AspNetUsers", "Id",
                            onDelete: ReferentialAction.Restrict );
                        table.ForeignKey( "FK_Message_User_SenderId", x => x.SenderId, "AspNetUsers", "Id",
                            onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateTable( "AspNetUserClaims",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        ClaimType = table.Column< string >( nullable: true ),
                        ClaimValue = table.Column< string >( nullable: true ),
                        UserId = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_IdentityUserClaim<string>", x => x.Id );
                        table.ForeignKey( "FK_IdentityUserClaim<string>_User_UserId", x => x.UserId, "AspNetUsers",
                            "Id", onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "AspNetUserLogins",
                table =>
                    new {
                        LoginProvider = table.Column< string >( nullable: false ),
                        ProviderKey = table.Column< string >( nullable: false ),
                        ProviderDisplayName = table.Column< string >( nullable: true ),
                        UserId = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_IdentityUserLogin<string>", x => new {x.LoginProvider, x.ProviderKey} );
                        table.ForeignKey( "FK_IdentityUserLogin<string>_User_UserId", x => x.UserId, "AspNetUsers",
                            "Id", onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "AspNetRoleClaims",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        ClaimType = table.Column< string >( nullable: true ),
                        ClaimValue = table.Column< string >( nullable: true ),
                        RoleId = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_IdentityRoleClaim<string>", x => x.Id );
                        table.ForeignKey( "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", x => x.RoleId,
                            "AspNetRoles", "Id", onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "AspNetUserRoles",
                table =>
                    new {
                        UserId = table.Column< string >( nullable: false ),
                        RoleId = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_IdentityUserRole<string>", x => new {x.UserId, x.RoleId} );
                        table.ForeignKey( "FK_IdentityUserRole<string>_IdentityRole_RoleId", x => x.RoleId,
                            "AspNetRoles", "Id", onDelete: ReferentialAction.Cascade );
                        table.ForeignKey( "FK_IdentityUserRole<string>_User_UserId", x => x.UserId, "AspNetUsers",
                            "Id", onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "AnnounceCategory",
                table =>
                    new {
                        AnnounceId = table.Column< int >( nullable: false ),
                        CategoryId = table.Column< int >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_AnnounceCategory", x => new {x.AnnounceId, x.CategoryId} );
                        table.ForeignKey( "FK_AnnounceCategory_Announce_AnnounceId", x => x.AnnounceId, "Announce",
                            "Id", onDelete: ReferentialAction.Cascade );
                        table.ForeignKey( "FK_AnnounceCategory_Category_CategoryId", x => x.CategoryId, "Category",
                            "Id", onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "AnnounceFormFieldsValues",
                table =>
                    new {
                        FormFieldId = table.Column< int >( nullable: false ),
                        AnnounceId = table.Column< int >( nullable: false ),
                        Value = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_AnnounceFormFieldsValues", x => new {x.FormFieldId, x.AnnounceId} );
                        table.ForeignKey( "FK_AnnounceFormFieldsValues_Announce_AnnounceId", x => x.AnnounceId,
                            "Announce", "Id", onDelete: ReferentialAction.Cascade );
                        table.ForeignKey( "FK_AnnounceFormFieldsValues_FormField_FormFieldId", x => x.FormFieldId,
                            "FormField", "Id", onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "AnnounceGat",
                table =>
                    new {
                        GatId = table.Column< int >( nullable: false ),
                        AnnounceId = table.Column< int >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_AnnounceGat", x => new {x.GatId, x.AnnounceId} );
                        table.ForeignKey( "FK_AnnounceGat_Announce_AnnounceId", x => x.AnnounceId, "Announce", "Id",
                            onDelete: ReferentialAction.Cascade );
                        table.ForeignKey( "FK_AnnounceGat_Gat_GatId", x => x.GatId, "Gat", "Id",
                            onDelete: ReferentialAction.Cascade );
                    } );
            migrationBuilder.CreateTable( "FeedBack",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        AnnounceId = table.Column< int >( nullable: false ),
                        AuthorId = table.Column< string >( nullable: false ),
                        DateTime = table.Column< DateTime >( nullable: false ),
                        ReceiverId = table.Column< string >( nullable: false ),
                        Text = table.Column< string >( nullable: true ),
                        Vote = table.Column< int >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_FeedBack", x => x.Id );
                        table.ForeignKey( "FK_FeedBack_Announce_AnnounceId", x => x.AnnounceId, "Announce", "Id",
                            onDelete: ReferentialAction.Restrict );
                        table.ForeignKey( "FK_FeedBack_User_AuthorId", x => x.AuthorId, "AspNetUsers", "Id",
                            onDelete: ReferentialAction.Restrict );
                        table.ForeignKey( "FK_FeedBack_User_ReceiverId", x => x.ReceiverId, "AspNetUsers", "Id",
                            onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateTable( "ImageUrl",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        AnnounceId = table.Column< int >( nullable: false ),
                        Url = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_ImageUrl", x => x.Id );
                        table.ForeignKey( "FK_ImageUrl_Announce_AnnounceId", x => x.AnnounceId, "Announce", "Id",
                            onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateTable( "Interested",
                table =>
                    new {
                        Id =
                            table.Column< int >( nullable: false ).Annotation( "SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn ),
                        AnnounceId = table.Column< int >( nullable: false ),
                        ChooseDate = table.Column< DateTime >( nullable: true ),
                        DateTime = table.Column< DateTime >( nullable: false ),
                        UserId = table.Column< int >( nullable: false ),
                        UserId1 = table.Column< string >( nullable: false )
                    }, constraints: table => {
                        table.PrimaryKey( "PK_Interested", x => x.Id );
                        table.ForeignKey( "FK_Interested_Announce_AnnounceId", x => x.AnnounceId, "Announce", "Id",
                            onDelete: ReferentialAction.Restrict );
                        table.ForeignKey( "FK_Interested_User_UserId1", x => x.UserId1, "AspNetUsers", "Id",
                            onDelete: ReferentialAction.Restrict );
                    } );
            migrationBuilder.CreateIndex( "IX_User_Email", "AspNetUsers", "Email", unique: true );
            migrationBuilder.CreateIndex( "EmailIndex", "AspNetUsers", "NormalizedEmail" );
            migrationBuilder.CreateIndex( "UserNameIndex", "AspNetUsers", "NormalizedUserName" );
            migrationBuilder.CreateIndex( "IX_User_UserName", "AspNetUsers", "UserName", unique: true );
            migrationBuilder.CreateIndex( "RoleNameIndex", "AspNetRoles", "NormalizedName" );
        }

        protected override void Down( MigrationBuilder migrationBuilder ) {
            migrationBuilder.DropTable( "AnnounceCategory" );
            migrationBuilder.DropTable( "AnnounceFormFieldsValues" );
            migrationBuilder.DropTable( "AnnounceGat" );
            migrationBuilder.DropTable( "CategoryFormField" );
            migrationBuilder.DropTable( "FeedBack" );
            migrationBuilder.DropTable( "FieldDefaultValue" );
            migrationBuilder.DropTable( "ImageUrl" );
            migrationBuilder.DropTable( "Interested" );
            migrationBuilder.DropTable( "Message" );
            migrationBuilder.DropTable( "AspNetRoleClaims" );
            migrationBuilder.DropTable( "AspNetUserClaims" );
            migrationBuilder.DropTable( "AspNetUserLogins" );
            migrationBuilder.DropTable( "AspNetUserRoles" );
            migrationBuilder.DropTable( "Gat" );
            migrationBuilder.DropTable( "Category" );
            migrationBuilder.DropTable( "FormField" );
            migrationBuilder.DropTable( "Announce" );
            migrationBuilder.DropTable( "AspNetRoles" );
            migrationBuilder.DropTable( "AspNetUsers" );
        }
    }
}