﻿using BookShop.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.ViewModels;

namespace BookShop.Models
{
    public class BookShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=(local);Database=BookShopDB;Trusted_Connection=True");

            optionsBuilder.UseSqlServer(@"Server=(local);Database=BookShopDB;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Author_BookMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new Order_BookMap());
            modelBuilder.ApplyConfiguration(new Book_TranslatorMap());
            modelBuilder.ApplyConfiguration(new Book_CategoryMap());
            modelBuilder.Query<ReadAllBook>().ToView("ReadAllBooks");
            modelBuilder.Entity<Book>().HasQueryFilter(b =>(bool)!b.Delete);
            modelBuilder.Entity<Book>().Property(b => b.Delete).HasDefaultValueSql("0");
            //modelBuilder.Entity<Book>().Property(b => b.PublishDate).HasDefaultValueSql("CONVERT(datetime,GetDate())");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Provice> Provices { get; set; }
        public DbSet<Author_Book> Author_Books { get; set; }
        public DbSet<Order_Book> Order_Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Translator> Translator { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book_Category> Book_Categories { get; set; }
        public DbSet<Book_Translator> Book_Translators { get; set; }
        public DbQuery<ReadAllBook> ReadAllBooks { get; set; }


        [DbFunction("GetAllAuthor","dbo")]
        public static string GetAllAuthors(int BookID)
        {
            throw new NotImplementedException();
        }

        [DbFunction("GetAllTranslators","dbo")]
        public static string GetAllTranslators(int BookID)
        {
            throw new NotImplementedException();
        }

        [DbFunction("GetAllCategories", "dbo")]
        public static string GetAllCategories(int BookID)
        {
            throw new NotImplementedException();
        }
    }
}
