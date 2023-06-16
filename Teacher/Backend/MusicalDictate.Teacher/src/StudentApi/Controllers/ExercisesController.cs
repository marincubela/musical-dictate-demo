using Application.Exercises.Queries.GetExercise;
using Application.Exercises.Queries.GetExercises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers;

[Authorize(Roles = "Student")]
public class ExercisesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetExerciseDto>> GetExercise(string id)
    {
        return Ok(await Mediator.Send(new GetExerciseQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetExerciseDto>>> GetExercises([FromQuery] GetExercisesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}