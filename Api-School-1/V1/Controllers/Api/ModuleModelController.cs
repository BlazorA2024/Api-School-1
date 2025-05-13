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
using ApiSchool.Models;
using Microsoft.EntityFrameworkCore;
using ApiSchool.Data;

namespace V1.Controllers.Api
{
    //[ApiExplorerSettings(GroupName = "V1")]
    [Route("api/V1/Api/[controller]")]
    [ApiController]
    public class ModuleModelController : ControllerBase
    {
        private readonly IUseModuleModelService _modulemodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly DataContext _context;

        public ModuleModelController(DataContext context, IUseModuleModelService modulemodelService, IMapper mapper, ILoggerFactory logger)
        {
            _modulemodelService = modulemodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(ModuleModelController).FullName);
            _context = context;
        }
      
        // Get all ModuleModels.
        [HttpGet(Name = "GetModuleModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModuleModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all ModuleModels...");
                var result = await _modulemodelService.GetAllAsync();
                var items = _mapper.Map<List<ModuleModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ModuleModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a ModuleModel by ID.
        [HttpGet("{id}", Name = "GetModuleModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuleModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModuleModel ID received.");
                return BadRequest("Invalid ModuleModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ModuleModel with ID: {id}", id);
                var entity = await _modulemodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ModuleModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ModuleModelInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModuleModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ModuleModel by Lg.
        [HttpGet("GetModuleModelByLanguage", Name = "GetModuleModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuleModelOutputVM>> GetModuleModelByLg(ModuleModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModuleModel ID received.");
                return BadRequest("Invalid ModuleModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ModuleModel with ID: {id}", id);
                var entity = await _modulemodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ModuleModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ModuleModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModuleModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ModuleModels by Lg.
        [HttpGet("GetModuleModelsByLanguage", Name = "GetModuleModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModuleModelOutputVM>>> GetModuleModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid ModuleModel lg received.");
                return BadRequest("Invalid ModuleModel lg null ");
            }

            try
            {
                var modulemodels = await _modulemodelService.GetAllAsync();
                if (modulemodels == null)
                {
                    _logger.LogWarning("ModuleModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<ModuleModelOutputVM>>(modulemodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModuleModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new ModuleModel.
        [HttpPost(Name = "CreateModuleModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuleModelOutputVM>> Create([FromBody] ModuleModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("ModuleModel data is null in Create.");
                return BadRequest("ModuleModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new ModuleModel with data: {@model}", model);
                var item = _mapper.Map<ModuleModelRequestDso>(model);
                var createdEntity = await _modulemodelService.CreateAsync(item);
                var createdItem = _mapper.Map<ModuleModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new ModuleModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple ModuleModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModuleModelOutputVM>>> CreateRange([FromBody] IEnumerable<ModuleModelCreateVM> models)
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
                _logger.LogInformation("Creating multiple ModuleModels.");
                var items = _mapper.Map<List<ModuleModelRequestDso>>(models);
                var createdEntities = await _modulemodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<ModuleModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple ModuleModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing ModuleModel.
        [HttpPut(Name = "UpdateModuleModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuleModelOutputVM>> Update([FromBody] ModuleModelUpdateVM model)
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
                _logger.LogInformation("Updating ModuleModel with ID: {id}", model?.Id);
                var item = _mapper.Map<ModuleModelRequestDso>(model);
                var updatedEntity = await _modulemodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("ModuleModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<ModuleModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating ModuleModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a ModuleModel.
        [HttpDelete("{id}", Name = "DeleteModuleModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModuleModel ID received in Delete.");
                return BadRequest("Invalid ModuleModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting ModuleModel with ID: {id}", id);
                await _modulemodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ModuleModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of ModuleModels.
        [HttpGet("CountModuleModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting ModuleModels...");
                var count = await _modulemodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting ModuleModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("searchByName")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModuleModelOutputVM>>> SearchBName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Name is empty in SearchByName.");
                return BadRequest("«·«”„ „ÿ·Ê» ··»ÕÀ.");
            }

            try
            {
                _logger.LogInformation("Searching ModuleModel by name: {name}", name);

                var modul = await _context.Modules
                    .AsNoTracking()
                    .Where(s => s.Name != null && s.Name.Contains(name))
                    .ToListAsync();

                if (modul == null || !modul.Any())
                {
                    _logger.LogWarning("No ModuleModel found with name: {name}", name);
                    return NotFound("·« ÌÊÃœ ÿ·«» »Â–« «·«”„.");
                }

                var result = _mapper.Map<List<ModuleModel>>(modul);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //  ”ÃÌ·  ›«’Ì· «·Œÿ√ ·  »⁄ «·”»»
                _logger.LogError(ex, "Error occurred while searching RowModels by name: {name}", name);
                return StatusCode(500, new { Message = "ÕœÀ Œÿ√ √À‰«¡ «·»ÕÀ.", Details = ex.Message });
            }
        }
    }
}