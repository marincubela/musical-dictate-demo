using Application.Auth.Commands.LoginGrader;
using Application.Auth.Commands.LoginStudent;
using Application.Auth.Commands.LoginTeacher;
using Application.Auth.Commands.RefreshToken;
using Application.Auth.Commands.Revoke;
using Application.Common.Models;
using Application.Students.Commands.CreateStudent;
using Application.Teachers.Commands.CreateTeacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost("login/student")]
    public async Task<ActionResult<Token>> LoginStudent([FromBody] LoginStudentCommand command)
    {
        var token = await Mediator.Send(command);

        return Ok(token);
    }

    [HttpPost("login/teacher")]
    public async Task<ActionResult<Token>> LoginTeacher([FromBody] LoginTeacherCommand command)
    {
        var token = await Mediator.Send(command);

        return Ok(token);
    }

    [HttpPost("login/grader")]
    public async Task<ActionResult<Token>> LoginGrader([FromBody] LoginGraderCommand command)
    {
        var token = await Mediator.Send(command);

        return Ok(token);
    }

    [HttpPost]
    [Route("register/student")]
    public async Task<ActionResult<Token>> Register(CreateStudentCommand command)
    {
        var token = await Mediator.Send(command);

        return Ok(token);
    }

    [HttpPost]
    [Route("register/teacher")]
    public async Task<ActionResult<Token>> Register(CreateTeacherCommand command)
    {
        var token = await Mediator.Send(command);

        return Ok(token);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult<Token>> RefreshToken(RefreshTokenCommand command)
    {
        var token = await Mediator.Send(command);

        return Ok(token);
    }

    [Authorize]
    [HttpPost]
    [Route("revoke")]
    public async Task<IActionResult> Revoke(RevokeCommand command)
    {
        await Mediator.Send(command);

        return Ok();
    }
}