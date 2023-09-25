using Microsoft.EntityFrameworkCore;
using Premisson.Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Premisson.Northwind.Data.Acces.Concreate.EntityFramework
{
    public class NorthwindContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(@"workstation id=hrproject.mssql.somee.com;packet size=4096;
            user id=skarakayaa_SQLLogin_1;pwd=ftcg31yki6;data source=hrproject.mssql.somee.com;persist security info=False;initial catalog=hrproject");
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-4MQ6U6N\SQLEXPRESS;Database=PermissionDB;Trusted_Connection=true");
        //}
        public DbSet<Dayoff> Dayoffs { get; set; }
        public DbSet<DayoffType> DayoffTypes { get; set; }
        public DbSet<Deparment> Deparments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayoffType>().HasData(
                new DayoffType { Id = 1, Name = "Yıllık İzin", IsDelete = false },
                new DayoffType { Id = 2, Name = "Hastalık İzin", IsDelete = false },
                new DayoffType { Id = 3, Name = "Mazeret İzin", IsDelete = false }
            );

            modelBuilder.Entity<Deparment>().HasData(
                new Deparment { Id = 1, Name = "Muhasebe Birimi", IsDelete = false, CreatedAt = DateTime.Now },
                new Deparment { Id = 2, Name = "Yazılım Birimi", IsDelete = false, CreatedAt = DateTime.Now },
                new Deparment { Id = 3, Name = "Satış Birimi", IsDelete = false, CreatedAt = DateTime.Now }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", IsDelete = false },
                new Role { Id = 2, Name = "Yönetici", IsDelete = false },
                new Role { Id = 3, Name = "Personel", IsDelete = false }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@corporate.com", IsActive = true, IsDelete = false, Name = "Admin", Password = "1234", RoleId = 1, Surname = "Admin", CreatedAt = DateTime.Now }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
