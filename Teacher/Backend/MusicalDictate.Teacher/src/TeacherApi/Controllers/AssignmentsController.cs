using Application.Assignments.Commands.CreateAssignment;
using Application.Assignments.Commands.DeleteAssignment;
using Application.Assignments.Commands.UpdateAssignment;
using Application.Assignments.Queries.GetAssignment;
using Application.Assignments.Queries.GetAssignments;
using Application.Assignments.Queries.GetAssignmentsByStudentGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherApi.Controllers;

[Authorize(Roles = "Teacher")]
public class AssignmentsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetAssignmentDto>> GetAssignment(string id)
    {
        return Ok(await Mediator.Send(new GetAssignmentQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAssignmentDto>>> GetAssignments([FromQuery] GetAssignmentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("group")]
    public async Task<ActionResult<IEnumerable<GetAssignmentsByStudentGroupDto>>> GetAssignmentsByStudentGroup([FromQuery] GetAssignmentsByStudentGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<ActionResult<GetAssignmentDto>> CreateAssignment([FromBody] CreateAssignmentCommand command)
    {
        var exerciseDto = await Mediator.Send(command);

        return new CreatedAtActionResult(nameof(GetAssignment),
            "Assignments",
            new {exerciseDto.Id},
            exerciseDto);
    }

    [HttpPut]
    public async Task<ActionResult> Update(string id, UpdateAssignmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment([FromQuery] DeleteAssignmentCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }
}