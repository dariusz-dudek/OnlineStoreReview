// <auto-generated />
using System;
using Codecool.OnlineStore.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Codecool.OnlineStore.Data.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20220531175204_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesListCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsListProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesListCategoryId", "ProductsListProductId");

                    b.HasIndex("ProductsListProductId");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Credentials", b =>
                {
                    b.Property<int>("CredentialsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CredentialsId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CredentialsId");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<int?>("AssignedUserUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaidAt")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderID");

                    b.HasIndex("AssignedUserUserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DiscountExpiration")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.ProductShoppingCart", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("ProductId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ProductShoppingCarts");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"), 1L, 1);

                    b.Property<decimal>("CurrentRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("NumberOfRatings")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingCartId"), 1L, 1);

                    b.HasKey("ShoppingCartId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<int?>("CredentialsId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("CredentialsId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("CustomerRating", b =>
                {
                    b.Property<int>("CustomersUserId")
                        .HasColumnType("int");

                    b.Property<int>("RatingsRatingId")
                        .HasColumnType("int");

                    b.HasKey("CustomersUserId", "RatingsRatingId");

                    b.HasIndex("RatingsRatingId");

                    b.ToTable("CustomerRating");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Customer", b =>
                {
                    b.HasBaseType("Codecool.OnlineStore.Data.Entities.User");

                    b.Property<int?>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasIndex("ShoppingCartId");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("Codecool.OnlineStore.Data.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesListCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codecool.OnlineStore.Data.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsListProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Order", b =>
                {
                    b.HasOne("Codecool.OnlineStore.Data.Entities.User", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserUserId");

                    b.Navigation("AssignedUser");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("Codecool.OnlineStore.Data.Entities.Order", null)
                        .WithMany("ItemsList")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.ProductShoppingCart", b =>
                {
                    b.HasOne("Codecool.OnlineStore.Data.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codecool.OnlineStore.Data.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.User", b =>
                {
                    b.HasOne("Codecool.OnlineStore.Data.Entities.Credentials", "Credentials")
                        .WithMany()
                        .HasForeignKey("CredentialsId");

                    b.Navigation("Credentials");
                });

            modelBuilder.Entity("CustomerRating", b =>
                {
                    b.HasOne("Codecool.OnlineStore.Data.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codecool.OnlineStore.Data.Entities.Rating", null)
                        .WithMany()
                        .HasForeignKey("RatingsRatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Customer", b =>
                {
                    b.HasOne("Codecool.OnlineStore.Data.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Codecool.OnlineStore.Data.Entities.Order", b =>
                {
                    b.Navigation("ItemsList");
                });
#pragma warning restore 612, 618
        }
    }
}
