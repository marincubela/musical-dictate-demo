using Application.Assignments.Queries.GetAssignment;
using Application.Assignments.Queries.GetAssignments;
using Application.Assignments.Queries.GetAssignmentsByStudentGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers;

[Authorize(Roles = "Student")]
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
}