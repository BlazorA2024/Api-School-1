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
    public class RowModelController : ControllerBase
    {
        private readonly IUseRowModelService _rowmodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly DataContext _context;

        public RowModelController(DataContext context, IUseRowModelService rowmodelService, IMapper mapper, ILoggerFactory logger)
        {
            _rowmodelService = rowmodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(RowModelController).FullName);
           // _context = context;
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

        // Get a RowModel by ID.
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
                var entity = await _rowmodelService.GetByIdAsync(id);
                if (entity == null)
                    return NotFound("RowModel not found.");
                var output = new RowModelInfoVMV
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    SchoolNames = entity.School.Name,
                    StudentsNames = entity.Students.Select(m => m.Name.FullName).ToList(),
                    ModuleNames = entity.Modules.Select(m => m.Name).ToList(),
                    TeacherNames = entity.Teachers.Select(ts => ts?.Name?.FullName).ToList()
                };
                var item = _mapper.Map<RowModelInfoVMV>(output);
                return Ok(item);
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
        //[HttpGet("searchByName")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<IEnumerable<StudentModelOutputVM>>> SearchBName([FromQuery] string name)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        _logger.LogWarning("Name is empty in SearchByName.");
        //        return BadRequest("«·«”„ „ÿ·Ê» ··»ÕÀ.");
        //    }

        //    try
        //    {
        //        _logger.LogInformation("Searching StudentModels by name: {name}", name);

        //        var students = await _context.Rows
        //            .Include(s => s.Name)  
        //            .ToListAsync();  

        //        var filteredStudents = students
        //            .Where(s => s.Name != null && s.Name.Contains(name))
        //            .ToList();

        //        if (!filteredStudents.Any())
        //        {
        //            _logger.LogWarning("No StudentModels found with name: {name}", name);
        //            return NotFound("·« ÌÊÃœ ÿ·«» »Â–« «·«”„.");
        //        }

        //        var result = _mapper.Map<List<RowModel>>(filteredStudents);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while searching StudentModels by name: {name}", name);
        //        return StatusCode(500, new { Message = "ÕœÀ Œÿ√ √À‰«¡ «·»ÕÀ.", Details = ex.Message });
        //    }
        //}
        [HttpGet("searchByName")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RowModelOutputVM>>> SearchBName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Name is empty in SearchByName.");
                return BadRequest("«·«”„ „ÿ·Ê» ··»ÕÀ.");
            }
            try
            {
                var results = await _rowmodelService.SearchByRowsAsync(name);

                if (results == null || !results.Any())
                    return NotFound("·« ÌÊÃœ ’› »Â–« «·«”„.");

                var output = _mapper.Map<List<RowModelOutputVM>>(results);
                return Ok(output);
            }
            //try
            //{
            //    _logger.LogInformation("Searching RowModels by name: {name}", name);

            //    // «” Œœ«„ AsNoTracking · Õ”Ì‰ «·√œ«¡ ≈–« ﬂ«‰  «·»Ì«‰«  ·«  Õ «Ã ≈·Ï «· ⁄œÌ·
            //    var row = await _context.Rows
            //        .AsNoTracking() // ··Õ’Ê· ⁄·Ï ‰ «∆Ã ›ﬁÿ »œÊ‰   »⁄ «·ﬂÌ«‰« 
            //        .Where(s => s.Name != null && s.Name.Contains(name)) //  √ﬂœ „‰ √‰ Name €Ì— ›«—€
            //        .ToListAsync();

            //    if (row == null || !row.Any())
            //    {
            //        _logger.LogWarning("No RowModels found with name: {name}", name);
            //        return NotFound("·« ÌÊÃœ ’› »Â–« «·«”„.");
            //    }

            //    var result = _mapper.Map<List<RowModel>>(row);
            //    return Ok(result);
            //}
            catch (Exception ex)
            {
                //  ”ÃÌ·  ›«’Ì· «·Œÿ√ ·  »⁄ «·”»»
                _logger.LogError(ex, "Error occurred while searching RowModels by name: {name}", name);
                return StatusCode(500, new { Message = "ÕœÀ Œÿ√ √À‰«¡ «·»ÕÀ.", Details = ex.Message });
            }
        }

    }
}