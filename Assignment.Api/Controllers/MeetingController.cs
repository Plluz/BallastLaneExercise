using Assignment.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/meetings")]
[Authorize]
public class MeetingController : ControllerBase
{
    private readonly ILogger<MeetingController> _logger;
    private readonly IMeetingService _meetingService;

    public MeetingController(ILogger<MeetingController> logger, IMeetingService meetingService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _meetingService = meetingService ?? throw new ArgumentNullException(nameof(meetingService));
    }

    /// <summary>
    /// Retrieves all meetings.
    /// </summary>
    /// <returns>Returns a collection of all meetings.</returns>
    /// <response code="200">Returns the collection of meetings.</response>
    /// <response code="400">If there's an issue with the request.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Meeting>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _meetingService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific meeting by its ID.
    /// </summary>
    /// <param name="id">The ID of the meeting to retrieve.</param>
    /// <returns>Returns the meeting with the specified ID.</returns>
    /// <response code="200">Returns the requested meeting.</response>
    /// <response code="400">If there's an issue with the request.</response>
    /// <response code="404">If the meeting with the specified ID is not found.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Meeting))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var meeting = await _meetingService.GetByIdAsync(id);

        if (meeting == null)
            return NotFound();

        return Ok(meeting);
    }

    /// <summary>
    /// Adds a new meeting.
    /// </summary>
    /// <param name="meeting">The meeting object to add.</param>
    /// <returns>Returns a response indicating success or failure.</returns>
    /// <response code="201">Indicates successful creation of the meeting.</response>
    /// <response code="400">If there's an issue with the request or the meeting couldn't be created.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromBody] Meeting meeting)
    {
        var created = await _meetingService.AddAsync(meeting);
            
        if (!created)
            return BadRequest();

        return Created();
    }

    /// <summary>
    /// Updates an existing meeting.
    /// </summary>
    /// <param name="updatedMeeting">The updated meeting object.</param>
    /// <returns>Returns the updated meeting object.</returns>
    /// <response code="200">Indicates successful update of the meeting.</response>
    /// <response code="400">If there's an issue with the request or the meeting couldn't be updated.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Meeting))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] Meeting updatedMeeting)
    {
        var updated = await _meetingService.UpdateAsync(updatedMeeting);

        if (!updated)
            return BadRequest();

        return Ok(updatedMeeting);
    }

    /// <summary>
    /// Deletes a meeting by its ID.
    /// </summary>
    /// <param name="id">The ID of the meeting to delete.</param>
    /// <returns>Returns a response indicating success or failure.</returns>
    /// <response code="204">Indicates successful deletion of the meeting.</response>
    /// <response code="400">If there's an issue with the request or the meeting couldn't be deleted.</response>
    /// <response code="404">If the meeting with the specified ID is not found.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isDeleted = await _meetingService.DeleteAsync(id);

        if (!isDeleted)
            return NotFound();

        return NoContent();
    }
}
