using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using V1.Services.Services;
using Microsoft.AspNetCore.Mvc;
using V1.DyModels.VMs;
using System.Linq.Expressions;
using V1.DyModels.Dso.Requests;
using AutoGenerator.Helper.Translation;
using System;

namespace V1.Controllers.Api
{
    //[ApiExplorerSettings(GroupName = "V1")]
    [Route("api/V1/Api/[controller]")]
    [ApiController]
    public class TeacherStudentController : ControllerBase
    {
        private readonly IUseTeacherStudentService _teacherstudentService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TeacherStudentController(IUseTeacherStudentService teacherstudentService, IMapper mapper, ILoggerFactory logger)
        {
            _teacherstudentService = teacherstudentService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(TeacherStudentController).FullName);
        }

        // Get all TeacherStudents.
        [HttpGet(Name = "GetTeacherStudents")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherStudentOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherStudents...");
                var result = await _teacherstudentService.GetAllAsync();
                var items = _mapper.Map<List<TeacherStudentOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherStudents");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a TeacherStudent by ID.
        [HttpGet("{id}", Name = "GetTeacherStudent")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherStudentInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherStudent ID received.");
                return BadRequest("Invalid TeacherStudent ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherStudent with ID: {id}", id);
                var entity = await _teacherstudentService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherStudent not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherStudentInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherStudent with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherStudent by Lg.
        [HttpGet("GetTeacherStudentByLanguage", Name = "GetTeacherStudentByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherStudentOutputVM>> GetTeacherStudentByLg(TeacherStudentFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherStudent ID received.");
                return BadRequest("Invalid TeacherStudent ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherStudent with ID: {id}", id);
                var entity = await _teacherstudentService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherStudent not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherStudentOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherStudent with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherStudents by Lg.
        [HttpGet("GetTeacherStudentsByLanguage", Name = "GetTeacherStudentsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherStudentOutputVM>>> GetTeacherStudentsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid TeacherStudent lg received.");
                return BadRequest("Invalid TeacherStudent lg null ");
            }

            try
            {
                var teacherstudents = await _teacherstudentService.GetAllAsync();
                if (teacherstudents == null)
                {
                    _logger.LogWarning("TeacherStudents not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<TeacherStudentOutputVM>>(teacherstudents, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherStudents with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new TeacherStudent.
        [HttpPost(Name = "CreateTeacherStudent")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherStudentOutputVM>> Create([FromBody] TeacherStudentCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("TeacherStudent data is null in Create.");
                return BadRequest("TeacherStudent data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new TeacherStudent with data: {@model}", model);
                var item = _mapper.Map<TeacherStudentRequestDso>(model);
                var createdEntity = await _teacherstudentService.CreateAsync(item);
                var createdItem = _mapper.Map<TeacherStudentOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new TeacherStudent");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple TeacherStudents.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherStudentOutputVM>>> CreateRange([FromBody] IEnumerable<TeacherStudentCreateVM> models)
        {
            if (models == null)
            {
                _logger.LogWarning("Data is null in CreateRange.");
                return BadRequest("Data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in CreateRange: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating multiple TeacherStudents.");
                var items = _mapper.Map<List<TeacherStudentRequestDso>>(models);
                var createdEntities = await _teacherstudentService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<TeacherStudentOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple TeacherStudents");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing TeacherStudent.
        [HttpPut(Name = "UpdateTeacherStudent")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherStudentOutputVM>> Update([FromBody] TeacherStudentUpdateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Invalid data in Update.");
                return BadRequest("Invalid data.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Update: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Updating TeacherStudent with ID: {id}", model?.Id);
                var item = _mapper.Map<TeacherStudentRequestDso>(model);
                var updatedEntity = await _teacherstudentService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("TeacherStudent not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<TeacherStudentOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating TeacherStudent with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a TeacherStudent.
        [HttpDelete("{id}", Name = "DeleteTeacherStudent")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherStudent ID received in Delete.");
                return BadRequest("Invalid TeacherStudent ID.");
            }

            try
            {
                _logger.LogInformation("Deleting TeacherStudent with ID: {id}", id);
                await _teacherstudentService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherStudent with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of TeacherStudents.
        [HttpGet("CountTeacherStudent")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting TeacherStudents...");
                var count = await _teacherstudentService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting TeacherStudents");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}