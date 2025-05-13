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
    public class TeacherModuleController : ControllerBase
    {
        private readonly IUseTeacherModuleService _teachermoduleService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TeacherModuleController(IUseTeacherModuleService teachermoduleService, IMapper mapper, ILoggerFactory logger)
        {
            _teachermoduleService = teachermoduleService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(TeacherModuleController).FullName);
        }

        // Get all TeacherModules.
        [HttpGet(Name = "GetTeacherModules")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherModuleOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherModules...");
                var result = await _teachermoduleService.GetAllAsync();
                var items = _mapper.Map<List<TeacherModuleOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherModules");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a TeacherModule by ID.
        [HttpGet("{id}", Name = "GetTeacherModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModuleInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherModule ID received.");
                return BadRequest("Invalid TeacherModule ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherModule with ID: {id}", id);
                var entity = await _teachermoduleService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherModule not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherModuleInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherModule with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherModule by Lg.
        [HttpGet("GetTeacherModuleByLanguage", Name = "GetTeacherModuleByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModuleOutputVM>> GetTeacherModuleByLg(TeacherModuleFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherModule ID received.");
                return BadRequest("Invalid TeacherModule ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherModule with ID: {id}", id);
                var entity = await _teachermoduleService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherModule not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherModuleOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherModule with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherModules by Lg.
        [HttpGet("GetTeacherModulesByLanguage", Name = "GetTeacherModulesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherModuleOutputVM>>> GetTeacherModulesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid TeacherModule lg received.");
                return BadRequest("Invalid TeacherModule lg null ");
            }

            try
            {
                var teachermodules = await _teachermoduleService.GetAllAsync();
                if (teachermodules == null)
                {
                    _logger.LogWarning("TeacherModules not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<TeacherModuleOutputVM>>(teachermodules, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherModules with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new TeacherModule.
        [HttpPost(Name = "CreateTeacherModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModuleOutputVM>> Create([FromBody] TeacherModuleCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("TeacherModule data is null in Create.");
                return BadRequest("TeacherModule data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new TeacherModule with data: {@model}", model);
                var item = _mapper.Map<TeacherModuleRequestDso>(model);
                var createdEntity = await _teachermoduleService.CreateAsync(item);
                var createdItem = _mapper.Map<TeacherModuleOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new TeacherModule");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple TeacherModules.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherModuleOutputVM>>> CreateRange([FromBody] IEnumerable<TeacherModuleCreateVM> models)
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
                _logger.LogInformation("Creating multiple TeacherModules.");
                var items = _mapper.Map<List<TeacherModuleRequestDso>>(models);
                var createdEntities = await _teachermoduleService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<TeacherModuleOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple TeacherModules");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing TeacherModule.
        [HttpPut(Name = "UpdateTeacherModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModuleOutputVM>> Update([FromBody] TeacherModuleUpdateVM model)
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
                _logger.LogInformation("Updating TeacherModule with ID: {id}", model?.Id);
                var item = _mapper.Map<TeacherModuleRequestDso>(model);
                var updatedEntity = await _teachermoduleService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("TeacherModule not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<TeacherModuleOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating TeacherModule with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a TeacherModule.
        [HttpDelete("{id}", Name = "DeleteTeacherModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherModule ID received in Delete.");
                return BadRequest("Invalid TeacherModule ID.");
            }

            try
            {
                _logger.LogInformation("Deleting TeacherModule with ID: {id}", id);
                await _teachermoduleService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherModule with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of TeacherModules.
        [HttpGet("CountTeacherModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting TeacherModules...");
                var count = await _teachermoduleService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting TeacherModules");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}