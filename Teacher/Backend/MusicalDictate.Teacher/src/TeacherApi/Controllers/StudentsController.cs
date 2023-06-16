using Application.Students.Commands.CreateStudent;
using Application.Students.Queries.GetStudent;
using Application.Students.Queries.GetStudents;
using Application.Students.Queries.GetStudentsByGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherApi.Controllers;

[Authorize(Roles = "Teacher")]
public class StudentsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentDto>> GetStudent(string id)
    {
        return Ok(await Mediator.Send(new GetStudentQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetStudentDto>>> GetStudents()
    {
        return Ok(await Mediator.Send(new GetStudentsQuery()));
    }

    [HttpGet("group")]
    public async Task<ActionResult<IEnumerable<GetStudentDto>>> GetStudentsByGroup([FromQuery] GetStudentsByGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateStudent([FromBody] CreateStudentCommand command)
    {
        var token = await Mediator.Send(command);

        return Ok(token);
    }
}