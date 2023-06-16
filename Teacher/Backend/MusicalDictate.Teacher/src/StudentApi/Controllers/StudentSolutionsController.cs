using Application.StudentSolutions.Commands.CreateStudentSolution;
using Application.StudentSolutions.Commands.UpdateStudentSolutionResult;
using Application.StudentSolutions.Queries.GetMyStudentSolutions;
using Application.StudentSolutions.Queries.GetStudentSolution;
using Application.StudentSolutions.Queries.GetStudentSolutionsByAssignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers;

[Authorize(Roles = "Student")]
public class StudentSolutionsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentSolutionDto>> GetStudentSolution(string id)
    {
        return Ok(await Mediator.Send(new GetStudentSolutionQuery(id)));
    }

    [HttpGet("my/{id}")]
    public async Task<ActionResult<IEnumerable<GetMyStudentSolutionsDto>>> GetMyStudentSolutions(string id)
    {
        return Ok(await Mediator.Send(new GetMyStudentSolutionsQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetStudentSolutionsByAssignmentDto>>> GetStudentSolutionsByAssignment([FromQuery] GetStudentSolutionsByAssignmentQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> CreateStudentSolution([FromForm] CreateStudentSolutionCommand command)
    {
        var id = await Mediator.Send(command);
        
        return new CreatedAtActionResult(nameof(GetStudentSolution),
            "StudentSolutions",
            new {id},
            id);
    }
    
    [HttpPut("result")]
    [DisableCors]
    public async Task<IActionResult> UpdateStudentSolution([FromBody] UpdateStudentSolutionResultCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }
}