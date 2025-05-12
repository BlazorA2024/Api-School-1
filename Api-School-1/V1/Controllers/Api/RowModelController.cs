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
using Microsoft.EntityFrameworkCore;
using ApiSchool.Data;
using ApiSchool.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace V1.Controllers.Api
{
    //[ApiExplorerSettings(GroupName = "V1")]
    [Route("api/V1/Api/[controller]")]
    [ApiController]
    public class RowModelController : ControllerBase
    {
        private readonly IUseRowModelService _rowmodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly DataContext _context;

        public RowModelController(DataContext context,IUseRowModelService rowmodelService, IMapper mapper, ILoggerFactory logger)
        {
            _rowmodelService = rowmodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(RowModelController).FullName);
            _context= context;
        }

        // Get all RowModels.
        [HttpGet(Name = "GetRowModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RowModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all RowModels...");
                var result = await _rowmodelService.GetAllAsync();
                var items = _mapper.Map<List<RowModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all RowModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //// Get a RowModel by ID.
        //[HttpGet("{id}", Name = "GetRowModel")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<RowModelInfoVM>> GetById(string? id)
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        _logger.LogWarning("Invalid RowModel ID received.");
        //        return BadRequest("Invalid RowModel ID.");
        //    }

        //    try
        //    {
        //        _logger.LogInformation("Fetching RowModel with ID: {id}", id);
        //        var entity = await _rowmodelService.GetByIdAsync(id);
        //        if (entity == null)
        //        {
        //            _logger.LogWarning("RowModel not found with ID: {id}", id);
        //            return NotFound();
        //        }

        //        var item = _mapper.Map<RowModelInfoVM>(entity);
        //        return Ok(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error while fetching RowModel with ID: {id}", id);
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}
        [HttpGet("{id}", Name = "GetRowModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RowModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid RowModel ID received.");
                return BadRequest("Invalid RowModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching RowModel with ID: {id}", id);

                var entity = await _context.Rows
                    .Include(r => r.Students)
                    .Include(r => r.Teachers)
                    .Include(r => r.Moduls)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (entity == null)
                {
                    _logger.LogWarning("RowModel not found with ID: {id}", id);
                    return NotFound();
                }

                // ≈⁄œ«œ JsonSerializerOptions „⁄ ReferenceHandler.Preserve
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,  // „⁄«·Ã… «·œÊ—…
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, //  Ã«Â· «·ÕﬁÊ· –«  «·ﬁÌ„… null
                    WriteIndented = true  // · ‰”Ìﬁ «·«” Ã«»… »‘ﬂ· ÃÌœ («Œ Ì«—Ì)
                };

                //  ÕÊÌ· «·ﬂ«∆‰ ≈·Ï JSON »«” Œœ«„ «·ŒÌ«—«  «·„⁄œ·…
                var item = _mapper.Map<RowModel>(entity);

                // ≈—Ã«⁄ «·‰ ÌÃ… »«” Œœ«„ JsonResult „⁄ «·ŒÌ«—«  «·„⁄œ·…
                return new JsonResult(item, options);  // ”Ì „ «·¬‰ ≈—Ã«⁄ «·»Ì«‰«  „⁄ œ⁄„ «·œÊ—« 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching RowModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }



        // // Get a RowModel by Lg.
        [HttpGet("GetRowModelByLanguage", Name = "GetRowModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RowModelOutputVM>> GetRowModelByLg(RowModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid RowModel ID received.");
                return BadRequest("Invalid RowModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching RowModel with ID: {id}", id);
                var entity = await _rowmodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("RowModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<RowModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching RowModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a RowModels by Lg.
        [HttpGet("GetRowModelsByLanguage", Name = "GetRowModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RowModelOutputVM>>> GetRowModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid RowModel lg received.");
                return BadRequest("Invalid RowModel lg null ");
            }

            try
            {
                var rowmodels = await _rowmodelService.GetAllAsync();
                if (rowmodels == null)
                {
                    _logger.LogWarning("RowModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<RowModelOutputVM>>(rowmodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching RowModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new RowModel.
        [HttpPost(Name = "CreateRowModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RowModelOutputVM>> Create([FromBody] RowModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("RowModel data is null in Create.");
                return BadRequest("RowModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new RowModel with data: {@model}", model);
                var item = _mapper.Map<RowModelRequestDso>(model);
                var createdEntity = await _rowmodelService.CreateAsync(item);
                var createdItem = _mapper.Map<RowModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new RowModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple RowModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RowModelOutputVM>>> CreateRange([FromBody] IEnumerable<RowModelCreateVM> models)
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
                _logger.LogInformation("Creating multiple RowModels.");
                var items = _mapper.Map<List<RowModelRequestDso>>(models);
                var createdEntities = await _rowmodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<RowModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple RowModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing RowModel.
        [HttpPut(Name = "UpdateRowModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RowModelOutputVM>> Update([FromBody] RowModelUpdateVM model)
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
                _logger.LogInformation("Updating RowModel with ID: {id}", model?.Id);
                var item = _mapper.Map<RowModelRequestDso>(model);
                item.Id = model.Id;
                item.Name = model.Body.Name;
                item.SchoolId = model.Body.SchoolId;
                var updatedEntity = await _rowmodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("RowModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<RowModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating RowModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a RowModel.
        [HttpDelete("{id}", Name = "DeleteRowModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid RowModel ID received in Delete.");
                return BadRequest("Invalid RowModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting RowModel with ID: {id}", id);
                await _rowmodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting RowModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of RowModels.
        [HttpGet("CountRowModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting RowModels...");
                var count = await _rowmodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting RowModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}