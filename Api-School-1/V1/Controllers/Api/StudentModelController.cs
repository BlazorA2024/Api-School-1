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
using AutoGenerator;
using ApiSchool.Data;
using Microsoft.EntityFrameworkCore;
using ApiSchool.Models;

namespace V1.Controllers.Api
{
    //[ApiExplorerSettings(GroupName = "V1")]
    [Route("api/V1/Api/[controller]")]
    [ApiController]
    public class StudentModelController : ControllerBase
    {
        private readonly IUseStudentModelService _studentmodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly DataContext _context;
        public StudentModelController(DataContext context,IUseStudentModelService studentmodelService, IMapper mapper, ILoggerFactory logger)
        {
            _studentmodelService = studentmodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(StudentModelController).FullName);
            _context = context;
        }

        // Get all StudentModels.
        [HttpGet(Name = "GetStudentModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all StudentModels...");
                var result = await _studentmodelService.GetAllAsync();
                var items = _mapper.Map<List<StudentModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all StudentModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a StudentModel by ID.
        //[HttpGet("{id}", Name = "GetStudentModel")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<StudentModelInfoVM>> GetById(string? id)
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        _logger.LogWarning("Invalid StudentModel ID received.");
        //        return BadRequest("Invalid StudentModel ID.");
        //    }

        //    try
        //    {
        //        _logger.LogInformation("Fetching StudentModel with ID: {id}", id);
        //        var entity = await _studentmodelService.GetByIdAsync(id);
        //        if (entity == null)
        //        {
        //            _logger.LogWarning("StudentModel not found with ID: {id}", id);
        //            return NotFound();
        //        }

        //        var item = _mapper.Map<StudentModelInfoVM>(entity);
        //        return Ok(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error while fetching StudentModel with ID: {id}", id);
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}
     

        // // Get a StudentModel by Lg.
        [HttpGet("GetStudentModelByLanguage", Name = "GetStudentModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModelOutputVM>> GetStudentModelByLg(StudentModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid StudentModel ID received.");
                return BadRequest("Invalid StudentModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching StudentModel with ID: {id}", id);
                var entity = await _studentmodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("StudentModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<StudentModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching StudentModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a StudentModels by Lg.
        [HttpGet("GetStudentModelsByLanguage", Name = "GetStudentModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentModelOutputVM>>> GetStudentModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid StudentModel lg received.");
                return BadRequest("Invalid StudentModel lg null ");
            }

            try
            {
                var studentmodels = await _studentmodelService.GetAllAsync();
                if (studentmodels == null)
                {
                    _logger.LogWarning("StudentModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<StudentModelOutputVM>>(studentmodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching StudentModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new StudentModel.
        [HttpPost(Name = "CreateStudentModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModelOutputVM>> Create([FromBody] StudentModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("StudentModel data is null in Create.");
                return BadRequest("StudentModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new StudentModel with data: {@model}", model);
                var item = _mapper.Map<StudentModelRequestDso>(model);
                item.Name.Name = model.Name.Name;
                item.Name.Title= model.Name.Title; 
                item.Name.FullName= model.Name.FullName;

                var createdEntity = await _studentmodelService.CreateAsync(item);
                var createdItem = _mapper.Map<StudentModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new StudentModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple StudentModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentModelOutputVM>>> CreateRange([FromBody] IEnumerable<StudentModelCreateVM> models)
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
                _logger.LogInformation("Creating multiple StudentModels.");
                var items = _mapper.Map<List<StudentModelRequestDso>>(models);
               
                var createdEntities = await _studentmodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<StudentModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple StudentModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing StudentModel.
        [HttpPut(Name = "UpdateStudentModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModelOutputVM>> Update([FromBody] StudentModelUpdateVM model)
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
                _logger.LogInformation("Updating StudentModel with ID: {id}", model?.Id);
                var item = _mapper.Map<StudentModelRequestDso>(model);
                item.Id = model.Id;
                item.Name.Name = model.Body.Name.Name;
                item.Name.Title = model.Body.Name.Title;
                item.Name.FullName = model.Body.Name.FullName;
                item.RowId = model.Body.RowId;
                item.SchoolId = model.Body.SchoolId;
                item.SexType = model.Body.SexType;
                item.Age = model.Body.Age;

                var updatedEntity = await _studentmodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("StudentModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<StudentModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating StudentModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a StudentModel.
        [HttpDelete("{id}", Name = "DeleteStudentModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid StudentModel ID received in Delete.");
                return BadRequest("Invalid StudentModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting StudentModel with ID: {id}", id);
                await _studentmodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting StudentModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of StudentModels.
        [HttpGet("CountStudentModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting StudentModels...");
                var count = await _studentmodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting StudentModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
       [HttpGet("searchByName")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentModelOutputVM>>> SearchBName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Name is empty in SearchByName.");
                return BadRequest("«·«”„ „ÿ·Ê» ··»ÕÀ.");
            }

            try
            {
                _logger.LogInformation("Searching StudentModels by name: {name}", name);

                // «” Œœ«„ Include · Õ„Ì· «·ﬂ«∆‰ «·„— »ÿ NameModel
                var students = await _context.Students
                    .Include(s => s.Name)  //  Õ„Ì· «·ﬂ«∆‰ «·„— »ÿ NameModel
                    .ToListAsync();  //  Õ„Ì· Ã„Ì⁄ «·»Ì«‰«  ›Ì «·–«ﬂ—…

                // «·»ÕÀ ›Ì FullName »«” Œœ«„ LINQ ›Ì «·–«ﬂ—…
                var filteredStudents = students
                    .Where(s => s.Name != null && s.Name.FullName.Contains(name))
                    .ToList();

                if (!filteredStudents.Any())
                {
                    _logger.LogWarning("No StudentModels found with name: {name}", name);
                    return NotFound("·« ÌÊÃœ ÿ·«» »Â–« «·«”„.");
                }

                var result = _mapper.Map<List<StudentModel>>(filteredStudents);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching StudentModels by name: {name}", name);
                return StatusCode(500, new { Message = "ÕœÀ Œÿ√ √À‰«¡ «·»ÕÀ.", Details = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetStudentModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentModelOutputVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid StudentModel ID received.");
                return BadRequest("Invalid StudentModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching StudentModel with ID: {id}", id);

                // «” —Ã«⁄ «·ﬂ«∆‰ „‰ ﬁ«⁄œ… «·»Ì«‰«  „⁄  ÷„Ì‰ «·ﬂ«∆‰«  «·„— »ÿ…
                var entity = await _context.Students
                    .Include(s => s.Name)            //  ÷„Ì‰ «·ﬂ«∆‰ Name
                    .Include(s => s.Row)             //  ÷„Ì‰ «·ﬂ«∆‰ Row
                    .Include(s => s.School)          //  ÷„Ì‰ «·ﬂ«∆‰ School
                    .Include(s => s.Moduls)          //  ÷„Ì‰ «·ﬂ«∆‰ Moduls
                    .Include(s => s.Teachers)        //  ÷„Ì‰ «·ﬂ«∆‰ Teachers
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (entity == null)
                {
                    _logger.LogWarning("StudentModel not found with ID: {id}", id);
                    return NotFound("StudentModel not found.");
                }

                //  ÕÊÌ· «·ﬂ«∆‰ ≈·Ï «·ﬂ«∆‰ «·„ÿ·Ê» »«” Œœ«„ AutoMapper
                var item = _mapper.Map<StudentModelOutputVM>(entity);
                //  var item = _mapper.Map<StudentModelInfoVM>(entity);

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching StudentModel with ID: {id}", id);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }




    }
}