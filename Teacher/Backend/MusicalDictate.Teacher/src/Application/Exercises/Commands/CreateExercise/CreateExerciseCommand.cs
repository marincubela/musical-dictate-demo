using System.Text.Json;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Exercises.Commands.CreateExercise;

public class CreateExerciseCommand : IRequest<CreateExerciseDto>
{
    public string Title { get; set; }
    public IFormFile Sheet { get; set; }
    public string Parts { get; set; }
}

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, CreateExerciseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateExerciseCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<CreateExerciseDto> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .SingleOrDefaultAsync(teacher => teacher.Id == _currentUserService.UserId, cancellationToken);

        Sheet? sheet = null;
        await using (var memoryStream = new MemoryStream())
        {
            await request.Sheet.CopyToAsync(memoryStream, cancellationToken);

            // Upload the file if less than 2 MB
            if (memoryStream.Length < 2097152)
            {
                sheet = Sheet.Create(memoryStream.ToArray());
            }
        }

        var parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(request.Parts);

        var exercise = _context.Exercises.Add(Exercise.Create(request.Title, teacher!, parts, sheet));

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateExerciseDto>(exercise.Entity);
    }
}