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
    public class StudentModuleController : ControllerBase
    {
        private readonly IUseStudentModuleService _studentmoduleService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public StudentModuleController(IUseStudentModuleService studentmoduleService, IMapper mapper, ILoggerFactory logger)
        {
            _studentmoduleService = studentmoduleService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(StudentModuleController).FullName);
        }

        // Get all StudentModules.
        [HttpGet(Name = "GetStudentModules")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentModuleOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all StudentModules...");
                var result = await _studentmoduleService.GetAllAsync();
                var items = _mapper.Map<List<StudentModuleOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all StudentModules");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a StudentModule by ID.
        [HttpGet("{id}", Name = "GetStudentModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModuleInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid StudentModule ID received.");
                return BadRequest("Invalid StudentModule ID.");
            }

            try
            {
                _logger.LogInformation("Fetching StudentModule with ID: {id}", id);
                var entity = await _studentmoduleService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("StudentModule not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<StudentModuleInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching StudentModule with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a StudentModule by Lg.
        [HttpGet("GetStudentModuleByLanguage", Name = "GetStudentModuleByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModuleOutputVM>> GetStudentModuleByLg(StudentModuleFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid StudentModule ID received.");
                return BadRequest("Invalid StudentModule ID.");
            }

            try
            {
                _logger.LogInformation("Fetching StudentModule with ID: {id}", id);
                var entity = await _studentmoduleService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("StudentModule not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<StudentModuleOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching StudentModule with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a StudentModules by Lg.
        [HttpGet("GetStudentModulesByLanguage", Name = "GetStudentModulesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentModuleOutputVM>>> GetStudentModulesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid StudentModule lg received.");
                return BadRequest("Invalid StudentModule lg null ");
            }

            try
            {
                var studentmodules = await _studentmoduleService.GetAllAsync();
                if (studentmodules == null)
                {
                    _logger.LogWarning("StudentModules not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<StudentModuleOutputVM>>(studentmodules, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching StudentModules with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new StudentModule.
        [HttpPost(Name = "CreateStudentModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModuleOutputVM>> Create([FromBody] StudentModuleCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("StudentModule data is null in Create.");
                return BadRequest("StudentModule data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new StudentModule with data: {@model}", model);
                var item = _mapper.Map<StudentModuleRequestDso>(model);
                var createdEntity = await _studentmoduleService.CreateAsync(item);
                var createdItem = _mapper.Map<StudentModuleOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new StudentModule");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple StudentModules.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentModuleOutputVM>>> CreateRange([FromBody] IEnumerable<StudentModuleCreateVM> models)
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
                _logger.LogInformation("Creating multiple StudentModules.");
                var items = _mapper.Map<List<StudentModuleRequestDso>>(models);
                var createdEntities = await _studentmoduleService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<StudentModuleOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple StudentModules");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing StudentModule.
        [HttpPut(Name = "UpdateStudentModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModuleOutputVM>> Update([FromBody] StudentModuleUpdateVM model)
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
                _logger.LogInformation("Updating StudentModule with ID: {id}", model?.Id);
                var item = _mapper.Map<StudentModuleRequestDso>(model);
                var updatedEntity = await _studentmoduleService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("StudentModule not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<StudentModuleOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating StudentModule with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a StudentModule.
        [HttpDelete("{id}", Name = "DeleteStudentModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid StudentModule ID received in Delete.");
                return BadRequest("Invalid StudentModule ID.");
            }

            try
            {
                _logger.LogInformation("Deleting StudentModule with ID: {id}", id);
                await _studentmoduleService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting StudentModule with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of StudentModules.
        [HttpGet("CountStudentModule")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting StudentModules...");
                var count = await _studentmoduleService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting StudentModules");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}