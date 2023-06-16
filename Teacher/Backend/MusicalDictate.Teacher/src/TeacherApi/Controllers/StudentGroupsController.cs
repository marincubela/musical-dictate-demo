using Application.StudentGroups.Commands.CreateStudentGroup;
using Application.StudentGroups.Commands.DeleteStudentGroup;
using Application.StudentGroups.Commands.UpdateStudentGroup;
using Application.StudentGroups.Queries.GetMyStudentGroup;
using Application.StudentGroups.Queries.GetMyStudentGroups;
using Application.StudentGroups.Queries.GetStudentGroup;
using Application.StudentGroups.Queries.GetStudentGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherApi.Controllers;

[Authorize(Roles = "Teacher")]
public class StudentGroupsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentGroupDto>> GetStudentGroup(string id)
    {
        return Ok(await Mediator.Send(new GetStudentGroupQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetStudentGroupsDto>>> GetStudentGroups()
    {
        return Ok(await Mediator.Send(new GetStudentGroupsQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateStudentGroup([FromBody] CreateStudentGroupCommand command)
    {
        var id = await Mediator.Send(command);

        return new CreatedAtActionResult(nameof(GetStudentGroup),
            "StudentGroups",
            new {id},
            id);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateStudentGroup([FromBody] UpdateStudentGroupCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudentGroup(string id)
    {
        await Mediator.Send(new DeleteStudentGroupCommand(id));

        return NoContent();
    }
}