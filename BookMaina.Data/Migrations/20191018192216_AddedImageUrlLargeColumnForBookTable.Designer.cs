﻿// <auto-generated />
using System;
using BookMania.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookMania.Data.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20191018192216_AddedImageUrlLargeColumnForBookTable")]
    partial class AddedImageUrlLargeColumnForBookTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookMania.Core.Entities.BookAggregate.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookAggregate.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("ImageUrlLarge");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PublishedDate");

                    b.Property<int>("PublisherId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookAggregate.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookAggregate.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookAuthor", b =>
                {
                    b.Property<int>("AuthorId");

                    b.Property<int>("BookId");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookCategory", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("BookId");

                    b.HasKey("CategoryId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("BookMania.Core.Entities.Favorite", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("UserId");

                    b.HasKey("BookId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("BookMania.Core.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("OrderDate");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookMania.Core.Entities.Review", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("UserId");

                    b.Property<int?>("Rating");

                    b.Property<string>("ReviewText")
                        .HasMaxLength(1000);

                    b.HasKey("BookId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BookMania.Core.Entities.UserAggregate.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BookMania.Core.Entities.UserAggregate.PaymentDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("CardNumberHash")
                        .IsRequired();

                    b.Property<byte[]>("CardSecurityKeyHash")
                        .IsRequired();

                    b.Property<bool>("IsDefault")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("Last4");

                    b.Property<string>("NameOnCard")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<byte[]>("Salt")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookAggregate.Book", b =>
                {
                    b.HasOne("BookMania.Core.Entities.BookAggregate.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookAuthor", b =>
                {
                    b.HasOne("BookMania.Core.Entities.BookAggregate.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookMania.Core.Entities.BookAggregate.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookMania.Core.Entities.BookCategory", b =>
                {
                    b.HasOne("BookMania.Core.Entities.BookAggregate.Book", "Book")
                        .WithMany("BookCategories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookMania.Core.Entities.BookAggregate.Category", "Category")
                        .WithMany("BookCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookMania.Core.Entities.Favorite", b =>
                {
                    b.HasOne("BookMania.Core.Entities.BookAggregate.Book", "Book")
                        .WithMany("Favorites")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookMania.Core.Entities.OrderAggregate.Order", b =>
                {
                    b.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsMany("BookMania.Core.Entities.OrderAggregate.OrderItem", "OrderItems", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("OrderId");

                            b1.Property<decimal>("Price")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<int>("Quantity");

                            b1.HasKey("Id");

                            b1.HasIndex("OrderId");

                            b1.ToTable("OrderItems");

                            b1.HasOne("BookMania.Core.Entities.OrderAggregate.Order")
                                .WithMany("OrderItems")
                                .HasForeignKey("OrderId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("BookMania.Core.Entities.OrderAggregate.BookItemSnapshot", "BookOrdered", b2 =>
                                {
                                    b2.Property<int>("OrderItemId")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<int>("BookId");

                                    b2.Property<string>("ImageUrl")
                                        .HasMaxLength(1000);

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasMaxLength(255);

                                    b2.HasKey("OrderItemId");

                                    b2.ToTable("OrderItems");

                                    b2.HasOne("BookMania.Core.Entities.OrderAggregate.OrderItem")
                                        .WithOne("BookOrdered")
                                        .HasForeignKey("BookMania.Core.Entities.OrderAggregate.BookItemSnapshot", "OrderItemId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("BookMania.Core.Entities.OrderAggregate.Address", "ShipToAddress", b1 =>
                        {
                            b1.Property<int>("OrderId");

                            b1.Property<string>("AddressLine1")
                                .IsRequired()
                                .HasMaxLength(255);

                            b1.Property<string>("AddressLine2")
                                .HasMaxLength(255);

                            b1.Property<string>("AddressLine3")
                                .HasMaxLength(255);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("CountryProvince")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("ZipOrPostCode")
                                .IsRequired()
                                .HasMaxLength(15);

                            b1.HasKey("OrderId");

                            b1.ToTable("ShipToAddress");

                            b1.HasOne("BookMania.Core.Entities.OrderAggregate.Order")
                                .WithOne("ShipToAddress")
                                .HasForeignKey("BookMania.Core.Entities.OrderAggregate.Address", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("BookMania.Core.Entities.Review", b =>
                {
                    b.HasOne("BookMania.Core.Entities.BookAggregate.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookMania.Core.Entities.UserAggregate.ApplicationUser", b =>
                {
                    b.OwnsOne("BookMania.Core.Entities.OrderAggregate.Address", "ShippingAddress", b1 =>
                        {
                            b1.Property<int>("ApplicationUserId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("AddressLine1");

                            b1.Property<string>("AddressLine2");

                            b1.Property<string>("AddressLine3");

                            b1.Property<string>("Country");

                            b1.Property<string>("CountryProvince");

                            b1.Property<string>("ZipOrPostCode");

                            b1.HasKey("ApplicationUserId");

                            b1.ToTable("AspNetUsers");

                            b1.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser")
                                .WithOne("ShippingAddress")
                                .HasForeignKey("BookMania.Core.Entities.OrderAggregate.Address", "ApplicationUserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("BookMania.Core.Entities.UserAggregate.PaymentDetails", b =>
                {
                    b.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser", "User")
                        .WithMany("PaymentOptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("BookMania.Core.Entities.OrderAggregate.Address", "BillingAddress", b1 =>
                        {
                            b1.Property<int>("PaymentDetailsId");

                            b1.Property<string>("AddressLine1")
                                .IsRequired()
                                .HasMaxLength(255);

                            b1.Property<string>("AddressLine2")
                                .HasMaxLength(255);

                            b1.Property<string>("AddressLine3")
                                .HasMaxLength(255);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("CountryProvince")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("ZipOrPostCode")
                                .IsRequired()
                                .HasMaxLength(15);

                            b1.HasKey("PaymentDetailsId");

                            b1.ToTable("BillingAddress");

                            b1.HasOne("BookMania.Core.Entities.UserAggregate.PaymentDetails")
                                .WithOne("BillingAddress")
                                .HasForeignKey("BookMania.Core.Entities.OrderAggregate.Address", "PaymentDetailsId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("BookMania.Core.Entities.UserAggregate.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
