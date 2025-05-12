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
    public class SchoolModelController : ControllerBase
    {
        private readonly IUseSchoolModelService _schoolmodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly DataContext _context ;

        public SchoolModelController(DataContext context ,IUseSchoolModelService schoolmodelService, IMapper mapper, ILoggerFactory logger)
        {
            _schoolmodelService = schoolmodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(SchoolModelController).FullName);
            _context = context;
        }

        // Get all SchoolModels.
        [HttpGet(Name = "GetSchoolModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SchoolModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all SchoolModels...");
                var result = await _schoolmodelService.GetAllAsync();
                var items = _mapper.Map<List<SchoolModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all SchoolModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a SchoolModel by ID.
        //[HttpGet("{id}", Name = "GetSchoolModel")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<SchoolModelInfoVM>> GetById(string? id)
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        _logger.LogWarning("Invalid SchoolModel ID received.");
        //        return BadRequest("Invalid SchoolModel ID.");
        //    }

        //    try
        //    {
        //        _logger.LogInformation("Fetching SchoolModel with ID: {id}", id);
        //        var entity = await _schoolmodelService.GetByIdAsync(id);
        //        if (entity == null)
        //        {
        //            _logger.LogWarning("SchoolModel not found with ID: {id}", id);
        //            return NotFound();
        //        }

        //        var item = _mapper.Map<SchoolModelInfoVM>(entity);
        //        return Ok(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error while fetching SchoolModel with ID: {id}", id);
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}
        [HttpGet("{id}", Name = "GetSchoolModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RowModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid SchoolModel ID received.");
                return BadRequest("Invalid SchoolModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching SchoolModel with ID: {id}", id);

                var entity = await _context.Schools
                   .Include(s => s.Rows)
                   .Include(s => s.Students)
                   .Include(s => s.Moduls)
                   .Include(s => s.Teachers)
                   .FirstOrDefaultAsync(s => s.Id == id);

                if (entity == null)
                {
                    _logger.LogWarning("SchoolModel not found with ID: {id}", id);
                    return NotFound();
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,  // „⁄«·Ã… «·œÊ—…
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, //  Ã«Â· «·ÕﬁÊ· –«  «·ﬁÌ„… null
                    WriteIndented = true  // · ‰”Ìﬁ «·«” Ã«»… »‘ﬂ· ÃÌœ («Œ Ì«—Ì)
                };

                var item = _mapper.Map<SchoolModel>(entity);

                // ≈—Ã«⁄ «·‰ ÌÃ… »«” Œœ«„ JsonResult „⁄ «·ŒÌ«—«  «·„⁄œ·…
                return new JsonResult(item, options);  // ”Ì „ «·¬‰ ≈—Ã«⁄ «·»Ì«‰«  „⁄ œ⁄„ «·œÊ—« 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching SchoolModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a SchoolModel by Lg.
        [HttpGet("GetSchoolModelByLanguage", Name = "GetSchoolModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SchoolModelOutputVM>> GetSchoolModelByLg(SchoolModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid SchoolModel ID received.");
                return BadRequest("Invalid SchoolModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching SchoolModel with ID: {id}", id);
                var entity = await _schoolmodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("SchoolModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<SchoolModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching SchoolModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a SchoolModels by Lg.
        [HttpGet("GetSchoolModelsByLanguage", Name = "GetSchoolModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SchoolModelOutputVM>>> GetSchoolModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid SchoolModel lg received.");
                return BadRequest("Invalid SchoolModel lg null ");
            }

            try
            {
                var schoolmodels = await _schoolmodelService.GetAllAsync();
                if (schoolmodels == null)
                {
                    _logger.LogWarning("SchoolModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<SchoolModelOutputVM>>(schoolmodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching SchoolModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new SchoolModel.
        [HttpPost(Name = "CreateSchoolModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SchoolModelOutputVM>> Create([FromBody] SchoolModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("SchoolModel data is null in Create.");
                return BadRequest("SchoolModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new SchoolModel with data: {@model}", model);
                var item = _mapper.Map<SchoolModelRequestDso>(model);
                var createdEntity = await _schoolmodelService.CreateAsync(item);
                var createdItem = _mapper.Map<SchoolModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new SchoolModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple SchoolModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SchoolModelOutputVM>>> CreateRange([FromBody] IEnumerable<SchoolModelCreateVM> models)
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
                _logger.LogInformation("Creating multiple SchoolModels.");
                var items = _mapper.Map<List<SchoolModelRequestDso>>(models);
                var createdEntities = await _schoolmodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<SchoolModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple SchoolModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing SchoolModel.
        [HttpPut(Name = "UpdateSchoolModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SchoolModelOutputVM>> Update([FromBody] SchoolModelUpdateVM model)
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
                _logger.LogInformation("Updating SchoolModel with ID: {id}", model?.Id);
                var item = _mapper.Map<SchoolModelRequestDso>(model);
                item.Id = model.Id;
                item.Name = model.Body.Name;
                item.Title = model.Body.Title;
                var updatedEntity = await _schoolmodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("SchoolModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<SchoolModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating SchoolModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        //// Delete a SchoolModel.
        //[HttpDelete("{id}", Name = "DeleteSchoolModel")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete(string? id)
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        _logger.LogWarning("Invalid SchoolModel ID received in Delete.");
        //        return BadRequest("Invalid SchoolModel ID.");
        //    }

        //    try
        //    {
        //        _logger.LogInformation("Deleting SchoolModel with ID: {id}", id);
        //        await _schoolmodelService.DeleteAsync(id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error while deleting SchoolModel with ID: {id}", id);
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}
        [HttpDelete("{id}", Name = "DeleteSchoolModelcontext")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deletes(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid SchoolModel ID received in Delete.");
                return BadRequest("Invalid SchoolModel ID.");
            }

            try
            {
                _logger.LogInformation("Attempting to delete SchoolModel with ID: {id}", id);

                var school = await _context.Schools
                    .Include(s => s.Rows)
                    .Include(s => s.Students)
                    .Include(s => s.Moduls)
                    .Include(s => s.Teachers)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (school == null)
                {
                    _logger.LogWarning("SchoolModel with ID: {id} not found.", id);
                    return NotFound(new ProblemDetails { Title = "School not found" });
                }

                // Clear relationships to avoid constraint issues
                school.Rows.Clear();
                school.Students.Clear();
                school.Moduls.Clear();
                school.Teachers.Clear(); // Many-to-Many

                _context.Schools.Remove(school);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted SchoolModel with ID: {id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting SchoolModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of SchoolModels.
        [HttpGet("CountSchoolModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting SchoolModels...");
                var count = await _schoolmodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting SchoolModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}