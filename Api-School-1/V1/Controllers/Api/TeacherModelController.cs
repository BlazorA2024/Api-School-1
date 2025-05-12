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
    public class TeacherModelController : ControllerBase
    {
        private readonly IUseTeacherModelService _teachermodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly DataContext _context;
        public TeacherModelController(DataContext context,IUseTeacherModelService teachermodelService, IMapper mapper, ILoggerFactory logger)
        {
            _teachermodelService = teachermodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(TeacherModelController).FullName);
            _context = context;
        }

        // Get all TeacherModels.
        [HttpGet(Name = "GetTeacherModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherModels...");
                var result = await _teachermodelService.GetAllAsync();
                var items = _mapper.Map<List<TeacherModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a TeacherModel by ID.
        [HttpGet("{id}", Name = "GetTeacherModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherModel ID received.");
                return BadRequest("Invalid TeacherModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherModel with ID: {id}", id);
                var entity = await _teachermodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherModelInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherModel by Lg.
        [HttpGet("GetTeacherModelByLanguage", Name = "GetTeacherModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModelOutputVM>> GetTeacherModelByLg(TeacherModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherModel ID received.");
                return BadRequest("Invalid TeacherModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TeacherModel with ID: {id}", id);
                var entity = await _teachermodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TeacherModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TeacherModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TeacherModels by Lg.
        [HttpGet("GetTeacherModelsByLanguage", Name = "GetTeacherModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherModelOutputVM>>> GetTeacherModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid TeacherModel lg received.");
                return BadRequest("Invalid TeacherModel lg null ");
            }

            try
            {
                var teachermodels = await _teachermodelService.GetAllAsync();
                if (teachermodels == null)
                {
                    _logger.LogWarning("TeacherModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<TeacherModelOutputVM>>(teachermodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TeacherModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new TeacherModel.
        [HttpPost(Name = "CreateTeacherModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModelOutputVM>> Create([FromBody] TeacherModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("TeacherModel data is null in Create.");
                return BadRequest("TeacherModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new TeacherModel with data: {@model}", model);
                var item = _mapper.Map<TeacherModelRequestDso>(model);
                item.Name.Name = model.Name.Name;
                item.Name.Title = model.Name.Title;
                item.Name.FullName = model.Name.FullName;
                var createdEntity = await _teachermodelService.CreateAsync(item);
                var createdItem = _mapper.Map<TeacherModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new TeacherModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple TeacherModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherModelOutputVM>>> CreateRange([FromBody] IEnumerable<TeacherModelCreateVM> models)
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
                _logger.LogInformation("Creating multiple TeacherModels.");
                var items = _mapper.Map<List<TeacherModelRequestDso>>(models);
                var createdEntities = await _teachermodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<TeacherModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple TeacherModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing TeacherModel.
        [HttpPut(Name = "UpdateTeacherModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherModelOutputVM>> Update([FromBody] TeacherModelUpdateVM model)
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
                _logger.LogInformation("Updating TeacherModel with ID: {id}", model?.Id);
                var item = _mapper.Map<TeacherModelRequestDso>(model);
                item.Id = model.Id;
                item.Name.Name = model.Body.Name.Name;
                item.Name.Title = model.Body.Name.Title;
                item.Name.FullName = model.Body.Name.FullName;
                item.RowId = model.Body.RowId;
                var updatedEntity = await _teachermodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("TeacherModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<TeacherModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating TeacherModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a TeacherModel.
        [HttpDelete("{id}", Name = "DeleteTeacherModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TeacherModel ID received in Delete.");
                return BadRequest("Invalid TeacherModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting TeacherModel with ID: {id}", id);
                await _teachermodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of TeacherModels.
        [HttpGet("CountTeacherModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting TeacherModels...");
                var count = await _teachermodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting TeacherModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("searchByName")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> SearchBName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Name is empty in SearchByName.");
                return BadRequest("«·«”„ „ÿ·Ê» ··»ÕÀ.");
            }

            try
            {
                _logger.LogInformation("Searching TeacherModel by name: {name}", name);
              
                var teacher = await _context.Teachers
                    .Include(s => s.Name)  
                    .ToListAsync();  

                var filteredTeacher = teacher
                    .Where(s => s.Name != null && s.Name.FullName.Contains(name))
                    .ToList();

                if (!filteredTeacher.Any())
                {
                    _logger.LogWarning("No TeacherModel found with name: {name}", name);
                    return NotFound("·« ÌÊÃœ ÿ·«» »Â–« «·«”„.");
                }

                var result = _mapper.Map<List<TeacherModel>>(filteredTeacher);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting TeacherModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}