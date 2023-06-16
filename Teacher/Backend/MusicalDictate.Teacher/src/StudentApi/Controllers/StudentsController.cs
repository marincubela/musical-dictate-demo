using Application.Students.Queries.GetStudent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers;

[Authorize(Roles = "Student")]
public class StudentsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentDto>> GetStudent(string id)
    {
        return Ok(await Mediator.Send(new GetStudentQuery(id)));
    }
}