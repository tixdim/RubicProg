using Microsoft.EntityFrameworkCore;
using RubicProg.DataAccess.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RubicProg.DataAccess.Core.Interfaces.DBContext
{
    public interface IDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<UserRto> Users { get; set; }
/*        DbSet<ProfileUserRto> ProfileUsers { get; set; }*/
        DbSet<WorkoutRto> Workouts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
