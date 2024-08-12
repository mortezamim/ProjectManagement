using Application.Helpers;
using Application.TaskDetails.Create;
using Application.TaskDetails.Delete;
using Domain.TaskDetails;
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
    public class TaskController : ControllerBase
    {
        private readonly ISender sender;

        public TaskController(ISender sender)
        {
            this.sender = sender;
        }

        /// <summary>
        ///     Create new task
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "Name": "Project 12.0.0.1",
        ///        "Description": "IDK, its a cool new project",
        ///        "DueDate": "2024-08-12T12:52:43.820Z",
        ///        "Status": 1,
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created successfully</response>
        /// <response code="400">Invalid form data</response>
        /// <response code="401">Unauthorized access</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1")]
        [HttpPost("{projectId:guid}", Name = nameof(CreateTask))]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request, Guid projectId)
        {
            var userId = Utils.GetUserIdFromToken(User);

            var command = new CreateTaskCommand(userId, new Domain.Projects.ProjectId(projectId),
                request.Name, request.Description, request.DueDate, request.Status);

            var res = await sender.Send(command);

            if (res is null)
                return BadRequest();

            return Created();
        }

        ///// <summary>
        /////     Delete Task
        ///// </summary>
        ///// <remarks>
        ///// Sample request:
        /////
        /////     Delete /
        /////
        ///// </remarks>
        ///// <response code="204">Task deleted successfully</response>
        ///// <response code="401">Unauthorized access</response>
        ///// <response code="404">Task not found</response>
        //[ProducesResponseType(typeof(User), StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        //[MapToApiVersion("1")]
        //[HttpDelete("{id:guid}")]
        //public async Task<IActionResult> DeleteTask(Guid id)
        //{
        //    var userId = Utils.GetUserIdFromToken(User);

        //    try
        //    {
        //        var command = new DeleteTaskCommand(userId, new TaskId(id));

        //        await sender.Send(command);
        //    }
        //    catch (EntryPointNotFoundException e)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        /// <summary>
        ///     Delete Task
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /
        ///
        /// </remarks>
        /// <response code="204">Task deleted successfully</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Task not found</response>
        [ProducesResponseType(typeof(User), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userId = Utils.GetUserIdFromToken(User);

            var command = new DeleteTaskCommand(userId, new TaskId(id));

            await sender.Send(command);

            return NoContent();
        }
    }
}
