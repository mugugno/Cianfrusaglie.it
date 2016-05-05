using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160505132238_Interested_coosedate_nullable2")]
    partial class Interested_coosedate_nullable2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cianfrusaglie.Models.Announce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<bool>("Closed");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime>("PublishDate");

                    b.Property<int>("Range");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.AnnounceCategory", b =>
                {
                    b.Property<int>("AnnounceId");

                    b.Property<int>("CategoryId");

                    b.HasKey("AnnounceId", "CategoryId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.AnnounceFormFieldsValues", b =>
                {
                    b.Property<int>("FormFieldId");

                    b.Property<int>("AnnounceId");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("FormFieldId", "AnnounceId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.AnnounceGat", b =>
                {
                    b.Property<int>("GatId");

                    b.Property<int>("AnnounceId");

                    b.HasKey("GatId", "AnnounceId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<int?>("OverCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();
                });

            modelBuilder.Entity("Cianfrusaglie.Models.CategoryFormField", b =>
                {
                    b.Property<int>("FormFieldId");

                    b.Property<int>("CategoryId");

                    b.HasKey("FormFieldId", "CategoryId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.FeedBack", b =>
                {
                    b.Property<int>("AnnounceId");

                    b.Property<int>("SenderId");

                    b.Property<int>("ReceiverId");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Text")
                        .HasAnnotation("MaxLength", 99);

                    b.Property<int>("Vote");

                    b.HasKey("AnnounceId", "SenderId", "ReceiverId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.FormField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Gat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Interested", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnnounceId")
                        .IsRequired();

                    b.Property<DateTime?>("ChooseDate");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("ReceiverId")
                        .IsRequired();

                    b.Property<string>("SenderId")
                        .IsRequired();

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("ReceiverId");

                    b.Property<bool>("RememberMe");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("SenderId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Announce", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.AnnounceCategory", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.Announce")
                        .WithMany()
                        .HasForeignKey("AnnounceId");

                    b.HasOne("Cianfrusaglie.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.AnnounceFormFieldsValues", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.Announce")
                        .WithMany()
                        .HasForeignKey("AnnounceId");

                    b.HasOne("Cianfrusaglie.Models.FormField")
                        .WithMany()
                        .HasForeignKey("FormFieldId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.AnnounceGat", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.Announce")
                        .WithMany()
                        .HasForeignKey("AnnounceId");

                    b.HasOne("Cianfrusaglie.Models.Gat")
                        .WithMany()
                        .HasForeignKey("GatId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Category", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.Category")
                        .WithMany()
                        .HasForeignKey("OverCategoryId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.CategoryFormField", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Cianfrusaglie.Models.FormField")
                        .WithMany()
                        .HasForeignKey("FormFieldId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.FeedBack", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.Announce")
                        .WithMany()
                        .HasForeignKey("AnnounceId");

                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .HasPrincipalKey("ReceiverId");

                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .HasPrincipalKey("SenderId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Interested", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.Announce")
                        .WithMany()
                        .HasForeignKey("AnnounceId");

                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.Message", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("ReceiverId");

                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("Cianfrusaglie.Models.User", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Cianfrusaglie.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
