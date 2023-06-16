using Application.StudentSolutions.Commands.CreateStudentSolution;
using Application.StudentSolutions.Commands.UpdateStudentSolutionResult;
using Application.StudentSolutions.Queries.GetMyStudentSolutions;
using Application.StudentSolutions.Queries.GetStudentSolution;
using Application.StudentSolutions.Queries.GetStudentSolutionsByAssignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TeacherApi.Controllers;

[Authorize(Roles = "Teacher")]
public class StudentSolutionsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentSolutionDto>> GetStudentSolution(string id)
    {
        return Ok(await Mediator.Send(new GetStudentSolutionQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetStudentSolutionsByAssignmentDto>>> GetStudentSolutionsByAssignment([FromQuery] GetStudentSolutionsByAssignmentQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPut("result")]
    public async Task<IActionResult> UpdateStudentSolution([FromBody] UpdateStudentSolutionResultCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }
}