using Application.StudentGroups.Commands.CreateStudentGroup;
using Application.StudentGroups.Commands.DeleteStudentGroup;
using Application.StudentGroups.Commands.UpdateStudentGroup;
using Application.StudentGroups.Queries.GetMyStudentGroup;
using Application.StudentGroups.Queries.GetMyStudentGroups;
using Application.StudentGroups.Queries.GetStudentGroup;
using Application.StudentGroups.Queries.GetStudentGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers;

[Authorize(Roles = "Student")]
public class StudentGroupsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentGroupDto>> GetStudentGroup(string id)
    {
        return Ok(await Mediator.Send(new GetStudentGroupQuery(id)));
    }

    [HttpGet("my/{id}")]
    public async Task<ActionResult<GetMyStudentGroupDto>> GetMyStudentGroup(string id)
    {
        return Ok(await Mediator.Send(new GetMyStudentGroupQuery(id)));
    }

    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<GetMyStudentGroupsDto>>> GetMyStudentGroups()
    {
        return Ok(await Mediator.Send(new GetMyStudentGroupsQuery()));
    }
}