﻿using FoodApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoodApp.Infrastructure.Data 
{
    public class FoodAppDB  : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public FoodAppDB(DbContextOptions<FoodAppDB> options)
            :base(options) 
        {
            
        }
    }
}