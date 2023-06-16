using Application.Exercises.Commands.CreateExercise;
using Application.Exercises.Commands.DeleteExercise;
using Application.Exercises.Commands.UpdateExercise;
using Application.Exercises.Queries.GetExercise;
using Application.Exercises.Queries.GetExercises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ExercisesController : ApiControllerBase
{
    [Authorize(Roles = "Student,Teacher")]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetExerciseDto>> GetExercise(string id)
    {
        return Ok(await Mediator.Send(new GetExerciseQuery(id)));
    }

    [Authorize(Roles = "Student,Teacher")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetExerciseDto>>> GetExercises([FromQuery] GetExercisesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<ActionResult<GetExerciseDto>> CreateExercise([FromForm] CreateExerciseCommand command)
    {
        var exerciseDto = await Mediator.Send(command);

        return new CreatedAtActionResult(nameof(GetExercise),
            "Exercises",
            new {exerciseDto.Id},
            exerciseDto);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut]
    public async Task<ActionResult> Update([FromForm] UpdateExerciseCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExercise(string id)
    {
        await Mediator.Send(new DeleteExerciseCommand(id));

        return NoContent();
    }
}