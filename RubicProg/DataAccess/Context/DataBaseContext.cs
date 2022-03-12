using Microsoft.EntityFrameworkCore;
using RubicProg.DataAccess.Core.Interfaces.DBContext;
using RubicProg.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubicProg.DataAccess.Context
{
    public class DataBaseContext : DbContext, IDbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<UserRto> Users { get; set; }
        public DbSet<ProfileUserRto> ProfileUsers { get; set; }
        public DbSet<WorkoutRto> Workouts { get; set; }
    }
}
