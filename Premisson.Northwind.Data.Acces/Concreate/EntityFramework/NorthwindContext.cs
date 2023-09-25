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
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4MQ6U6N\SQLEXPRESS;Database=PermissionDB;integrated security=true;Trusted_Connection=true");
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
    }
}
