﻿using Domain.User;
using Domain.User.Login;
using Domain.User.register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ISender sender;

        public AuthController(ISender sender)
        {
            this.sender = sender;
        }

        /// <summary>
        ///     Register a new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Register
        ///     {
        ///        "firstName": "MIM",
        ///        "lastName": "MIM",
        ///        "username": "MIM",
        ///        "password": "Super Secure Password"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns created user info</response>
        /// <response code="400">Invalid form data</response>
        /// <response code="409">Username is not available</response>
        /// <response code="500">Register failure</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [MapToApiVersion("1")]
        [HttpPost("Register", Name = nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var res = await sender.Send(new RegisterUserCommand(request.Username, request.Password, request.FirstName, request.LastName));
            return CreatedAtAction(nameof(Login), res);
        }

        /// <summary>
        ///     Login user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Login
        ///     {
        ///        "username": "MIM",
        ///        "password": "Super Secure Password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns JWT access token</response>
        /// <response code="401">Incorrect username or password</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1")]
        [HttpPost("Login", Name = nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var res = await sender.Send(new LoginUserCommand(request.Username, request.Password));
            return res is null ?
                Unauthorized() :
                Ok(res);
        }

        /// <summary>
        ///     Login user v2
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Login
        ///     {
        ///        "username": "MIM",
        ///        "password": "Super Secure Password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns JWT access token</response>
        /// <response code="401">Incorrect username or password</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("2")]
        [HttpPost("Login", Name = nameof(LoginV2))]
        public async Task<IActionResult> LoginV2([FromBody] LoginUserRequest request)
        {
            var res = await sender.Send(new LoginUserCommand(request.Username, request.Password));
            return res is null ?
                Unauthorized() :
                Ok(res);
        }

    }
}
