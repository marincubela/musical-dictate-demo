using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Exercise> Exercises { get; }
    public DbSet<Assignment> Assignments { get; }
    public DbSet<Student> Students { get; }
    public DbSet<Teacher> Teachers { get; }
    public DbSet<StudentGroup> StudentGroups { get; }
    public DbSet<Domain.Entities.StudentSolution> StudentSolutions { get; }
    public DbSet<Sheet> Sheets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}