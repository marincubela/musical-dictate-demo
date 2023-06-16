using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Identity;

namespace Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var teacherRole = new IdentityRole("Teacher");
        var studentRole = new IdentityRole("Student");
        var graderRole = new IdentityRole("Grader");

        if (_roleManager.Roles.All(r => r.Name != teacherRole.Name))
            await _roleManager.CreateAsync(teacherRole);

        if (_roleManager.Roles.All(r => r.Name != studentRole.Name))
            await _roleManager.CreateAsync(studentRole);

        if (_roleManager.Roles.All(r => r.Name != graderRole.Name))
            await _roleManager.CreateAsync(graderRole);

        // Default users
        var teacher = new ApplicationUser {UserType = teacherRole.Name!, UserName = "teacher@mail.com", Email = "teacher@mail.com"};

        if (_userManager.Users.All(u => u.UserName != teacher.UserName))
        {
            await _userManager.CreateAsync(teacher, "Teacher123!");
            if (!string.IsNullOrWhiteSpace(teacherRole.Name))
            {
                await _userManager.AddToRolesAsync(teacher, new[] {teacherRole.Name});
            }

            _context.Teachers.Add(Teacher.Create(teacher.Id, "Marijana", "Miloš"));
        }

        var student = new ApplicationUser {UserType = studentRole.Name!, UserName = "student@mail.com", Email = "student@mail.com"};

        if (_userManager.Users.All(u => u.UserName != student.UserName))
        {
            await _userManager.CreateAsync(student, "Student123!");
            if (!string.IsNullOrWhiteSpace(studentRole.Name))
            {
                await _userManager.AddToRolesAsync(student, new[] {studentRole.Name});
            }

            _context.Students.Add(Student.Create(student.Id, "0036123456", "Ivo", "Ivić", "IV.b"));
        }

        var grader = new ApplicationUser {UserType = graderRole.Name!, UserName = "grader@mail.com", Email = "grader@mail.com"};

        if (_userManager.Users.All(u => u.UserName != grader.UserName))
        {
            await _userManager.CreateAsync(grader, "Grader123!");
            if (!string.IsNullOrWhiteSpace(graderRole.Name))
            {
                await _userManager.AddToRolesAsync(student, new[] {graderRole.Name});
            }
        }

        // Default data
        // Seed, if necessary

        await _context.SaveChangesAsync();
    }
}