using Application.Helpers;
using Application.Project.Create;
using Application.Project.Delete;
using Application.Projects;
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
        ///     POST /
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
        [MapToApiVersion("1")]
        [HttpPost(Name = nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            var userId = Utils.GetUserIdFromToken(User);

            var command = new CreateProjectCommand(userId, request.Name, request.Description);

            var res = await sender.Send(command);

            if (res is null)
                return BadRequest();

            return Created();
        }

        /// <summary>
        ///     Fetch user projects list
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /
        ///     {
        ///        "sortColumn": "Name || Description",
        ///        "sortOrder": "desc || asc",
        ///        "page": 1,
        ///        "pageSize": 20
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns projects list</response>
        /// <response code="401">Unauthorized access</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1")]
        [HttpGet]
        public async Task<IActionResult> GetProjects(string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var userId = Utils.GetUserIdFromToken(User);

            var command = new GetProjectsQuery(userId, searchTerm, sortColumn, sortOrder, page, pageSize);

            var res = await sender.Send(command);

            return Ok(res);
        }

        /// <summary>
        ///     Delete Project
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /
        ///
        /// </remarks>
        /// <response code="204">Project deleted successfully</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Project not found</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var userId = Utils.GetUserIdFromToken(User);

            var command = new DeleteProjectCommand(userId, new Domain.Projects.ProjectId(id));

            await sender.Send(command);

            return NoContent();
        }

    }
}
