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
    public class TeacherSchoolController : ControllerBase
    {
        private readonly IUseTeacherSchoolService _teacherschoolService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TeacherSchoolController(IUseTeacherSchoolService teacherschoolService, IMapper mapper, ILoggerFactory logger)
        {
            _teacherschoolService = teacherschoolService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(TeacherSchoolController).FullName);
        }

        // Get all TeacherSchools.
        [HttpGet(Name = "GetTeacherSchools")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherSchoolOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherSchools...");
                var result = await _teacherschoolService.GetAllAsync();
                var items = _mapper.Map<List<TeacherSchoolOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherSchools");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a TeacherSchool by ID.
        [HttpGet("{id}", Name = "GetTeacherSchool")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherSchoolInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherSchool ID received.");
                return BadRequest("Invalid TeacherSchool ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherSchool with ID: {id}", id);
                var entity = await _teacherschoolService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherSchool not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherSchoolInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherSchool with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherSchool by Lg.
        [HttpGet("GetTeacherSchoolByLanguage", Name = "GetTeacherSchoolByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherSchoolOutputVM>> GetTeacherSchoolByLg(TeacherSchoolFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherSchool ID received.");
                return BadRequest("Invalid TeacherSchool ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherSchool with ID: {id}", id);
                var entity = await _teacherschoolService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherSchool not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherSchoolOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherSchool with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherSchools by Lg.
        [HttpGet("GetTeacherSchoolsByLanguage", Name = "GetTeacherSchoolsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherSchoolOutputVM>>> GetTeacherSchoolsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid TeacherSchool lg received.");
                return BadRequest("Invalid TeacherSchool lg null ");
            }

            try
            {
                var teacherschools = await _teacherschoolService.GetAllAsync();
                if (teacherschools == null)
                {
                    _logger.LogWarning("TeacherSchools not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<TeacherSchoolOutputVM>>(teacherschools, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherSchools with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new TeacherSchool.
        [HttpPost(Name = "CreateTeacherSchool")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherSchoolOutputVM>> Create([FromBody] TeacherSchoolCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("TeacherSchool data is null in Create.");
                return BadRequest("TeacherSchool data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new TeacherSchool with data: {@model}", model);
                var item = _mapper.Map<TeacherSchoolRequestDso>(model);
                var createdEntity = await _teacherschoolService.CreateAsync(item);
                var createdItem = _mapper.Map<TeacherSchoolOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new TeacherSchool");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple TeacherSchools.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherSchoolOutputVM>>> CreateRange([FromBody] IEnumerable<TeacherSchoolCreateVM> models)
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
                _logger.LogInformation("Creating multiple TeacherSchools.");
                var items = _mapper.Map<List<TeacherSchoolRequestDso>>(models);
                var createdEntities = await _teacherschoolService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<TeacherSchoolOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple TeacherSchools");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing TeacherSchool.
        [HttpPut(Name = "UpdateTeacherSchool")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherSchoolOutputVM>> Update([FromBody] TeacherSchoolUpdateVM model)
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
                _logger.LogInformation("Updating TeacherSchool with ID: {id}", model?.Id);
                var item = _mapper.Map<TeacherSchoolRequestDso>(model);
                var updatedEntity = await _teacherschoolService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("TeacherSchool not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<TeacherSchoolOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating TeacherSchool with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a TeacherSchool.
        [HttpDelete("{id}", Name = "DeleteTeacherSchool")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherSchool ID received in Delete.");
                return BadRequest("Invalid TeacherSchool ID.");
            }

            try
            {
                _logger.LogInformation("Deleting TeacherSchool with ID: {id}", id);
                await _teacherschoolService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherSchool with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of TeacherSchools.
        [HttpGet("CountTeacherSchool")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting TeacherSchools...");
                var count = await _teacherschoolService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting TeacherSchools");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}