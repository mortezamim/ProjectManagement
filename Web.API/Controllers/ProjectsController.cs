﻿using Application.Helpers;
using Application.Project.Create;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ISender sender;

        public ProjectsController(ISender sender)
        {
            this.sender = sender;
        }

        /// <summary>
        ///     Create a new project
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Register
        ///     {
        ///        "Name": "Project 12.0.0.1",
        ///        "Description": "IDK, its a cool new project"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns created project</response>
        /// <response code="400">Invalid form data</response>
        /// <response code="401">Unauthorized access</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [MapToApiVersion("1")]
        [HttpPost(Name = nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            var userId = Utils.GetUserIdFromToken(User);

            var command = new CreateProjectCommand(userId, request.Name, request.Description);

            var res = await sender.Send(command);

            return Ok("Created");
        }

    }
}
