using Assignment.Domain.Entities;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/meetings")]
public class MeetingController : ControllerBase
{
    private readonly ILogger<MeetingController> _logger;
    private readonly IMeetingService _meetingService;

    public MeetingController(ILogger<MeetingController> logger, IMeetingService meetingService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _meetingService = meetingService ?? throw new ArgumentNullException(nameof(meetingService));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _meetingService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while retrieving meetings.");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var meeting = await _meetingService.GetByIdAsync(id);

            if (meeting == null)
                return NotFound();

            return Ok(meeting);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while retrieving the meeting.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Meeting meeting)
    {
        try
        {
            var created = await _meetingService.AddAsync(meeting);
            
            if (!created)
                return BadRequest();

            return Created();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while creating the meeting.");
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Meeting updatedMeeting)
    {
        try
        {
            var updated = await _meetingService.UpdateAsync(id, updatedMeeting);

            if (!updated)
                return BadRequest();

            return Ok(updatedMeeting);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while updating the meeting.");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var isDeleted = await _meetingService.DeleteAsync(id);

            if (!isDeleted)
                return BadRequest();

            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while deleting the meeting.");
        }
    }
}
