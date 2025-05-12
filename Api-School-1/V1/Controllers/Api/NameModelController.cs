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
    public class NameModelController : ControllerBase
    {
        private readonly IUseNameModelService _namemodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public NameModelController(IUseNameModelService namemodelService, IMapper mapper, ILoggerFactory logger)
        {
            _namemodelService = namemodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(NameModelController).FullName);
        }

        // Get all NameModels.
        [HttpGet(Name = "GetNameModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<NameModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all NameModels...");
                var result = await _namemodelService.GetAllAsync();
                var items = _mapper.Map<List<NameModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all NameModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a NameModel by ID.
        [HttpGet("{id}", Name = "GetNameModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NameModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid NameModel ID received.");
                return BadRequest("Invalid NameModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching NameModel with ID: {id}", id);
                var entity = await _namemodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("NameModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<NameModelInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching NameModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a NameModel by Lg.
        [HttpGet("GetNameModelByLanguage", Name = "GetNameModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NameModelOutputVM>> GetNameModelByLg(NameModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid NameModel ID received.");
                return BadRequest("Invalid NameModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching NameModel with ID: {id}", id);
                var entity = await _namemodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("NameModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<NameModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching NameModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a NameModels by Lg.
        [HttpGet("GetNameModelsByLanguage", Name = "GetNameModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<NameModelOutputVM>>> GetNameModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid NameModel lg received.");
                return BadRequest("Invalid NameModel lg null ");
            }

            try
            {
                var namemodels = await _namemodelService.GetAllAsync();
                if (namemodels == null)
                {
                    _logger.LogWarning("NameModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<NameModelOutputVM>>(namemodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching NameModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new NameModel.
        [HttpPost(Name = "CreateNameModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NameModelOutputVM>> Create([FromBody] NameModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("NameModel data is null in Create.");
                return BadRequest("NameModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new NameModel with data: {@model}", model);
                var item = _mapper.Map<NameModelRequestDso>(model);
                var createdEntity = await _namemodelService.CreateAsync(item);
                var createdItem = _mapper.Map<NameModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new NameModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple NameModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<NameModelOutputVM>>> CreateRange([FromBody] IEnumerable<NameModelCreateVM> models)
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
                _logger.LogInformation("Creating multiple NameModels.");
                var items = _mapper.Map<List<NameModelRequestDso>>(models);
                var createdEntities = await _namemodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<NameModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple NameModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing NameModel.
        [HttpPut(Name = "UpdateNameModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NameModelOutputVM>> Update([FromBody] NameModelUpdateVM model)
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
                _logger.LogInformation("Updating NameModel with ID: {id}", model?.Id);
                var item = _mapper.Map<NameModelRequestDso>(model);
                var updatedEntity = await _namemodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("NameModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<NameModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating NameModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a NameModel.
        [HttpDelete("{id}", Name = "DeleteNameModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid NameModel ID received in Delete.");
                return BadRequest("Invalid NameModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting NameModel with ID: {id}", id);
                await _namemodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting NameModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of NameModels.
        [HttpGet("CountNameModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting NameModels...");
                var count = await _namemodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting NameModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}