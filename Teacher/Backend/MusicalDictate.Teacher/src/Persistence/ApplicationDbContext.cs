using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Identity;
using Persistence.Interceptors;

namespace Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly IMediator _mediator;

    public ApplicationDbContext(DbContextOptions options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, IMediator mediator) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _mediator = mediator;
    }

    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();
    public DbSet<StudentSolution> StudentSolutions => Set<StudentSolution>();
    public DbSet<Sheet> Sheets => Set<Sheet>();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}