using AutoGenerator;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoGenerator.Services.Base;
using V1.DyModels.Dso.Requests;
using V1.DyModels.Dso.Responses;
using ApiSchool.Models;
using V1.DyModels.Dto.Share.Requests;
using V1.DyModels.Dto.Share.Responses;
using V1.Repositories.Share;
using System.Linq.Expressions;
using V1.Repositories.Builder;
using AutoGenerator.Repositories.Base;
using AutoGenerator.Helper;
using System;
using Microsoft.EntityFrameworkCore;
using ApiSchool.Data;


namespace V1.Services.Services
{
    public class StudentModelService : BaseService<StudentModelRequestDso, StudentModelResponseDso>, IUseStudentModelService
    {
        private readonly IStudentModelShareRepository _share;
        private readonly DataContext _context;

        public StudentModelService(DataContext context, IStudentModelShareRepository buildStudentModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildStudentModelShareRepository;
            _context = context;
        }
        public override async Task<StudentModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving StudentModel entity with ID: {id}...");
                //  var result = await _share.GetByIdAsync(id);
                var entity = await _context.Students
                         .Include(s => s.Name)
                         .Include(s => s.Row)
                         .Include(s => s.School)
                         .Include(s => s.StudentModules)
                             .ThenInclude(sm => sm.Module) // ?? åÐÇ ÇáÓØÑ ÖÑæÑí
                         .Include(s => s.TeacherStudents)
                             .ThenInclude(ts => ts.Teacher)
                                 .ThenInclude(t => t.Name) // ÊÃßÏ ÃíÖÇð ãä ÊÍãíá ÇÓã ÇáãÚáã
                         .FirstOrDefaultAsync(s => s.Id == id);
                var item = GetMapper().Map<StudentModelResponseDso>(entity);
                _logger.LogInformation("Retrieved StudentModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for StudentModel entity with ID: {id}.");
                return null;
            }
        }
        public async Task<IEnumerable<StudentModelResponseDso>> SearchByStudentsAsync(string name)
        {


            try
            {
                _logger.LogInformation("Searching StudentModels by name: {name}", name);

                
                var students = await _context.Students
                    .Include(s => s.Name)  
                    .ToListAsync();  

                var filteredStudents = students
                    .Where(s => s.Name != null && s.Name.FullName.Contains(name))
                    .ToList();

                if (!filteredStudents.Any())
                {
                    _logger.LogWarning("No StudentModels found with name: {name}", name);
                }

                var result = GetMapper().Map<List<StudentModelResponseDso>>(filteredStudents);
                _logger.LogInformation(" StudentModels entity successfully.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching StudentModels by name: {name}", name);
                return null;
            }
        }
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting StudentModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for StudentModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<StudentModelResponseDso> CreateAsync(StudentModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new StudentModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<StudentModelResponseDso>(result);
                _logger.LogInformation("Created StudentModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating StudentModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting StudentModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting StudentModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<StudentModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all StudentModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<StudentModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for StudentModel entities.");
                return null;
            }
        }

        //public override async Task<StudentModelResponseDso?> GetByIdAsync(string id)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Retrieving StudentModel entity with ID: {id}...");
        //        var result = await _share.GetByIdAsync(id);
        //        var item = GetMapper().Map<StudentModelResponseDso>(result);
        //        _logger.LogInformation("Retrieved StudentModel entity successfully.");
        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error in GetByIdAsync for StudentModel entity with ID: {id}.");
        //        return null;
        //    }
        //}

        public override IQueryable<StudentModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<StudentModelResponseDso> for StudentModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<StudentModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for StudentModel entities.");
                return null;
            }
        }

        public override async Task<StudentModelResponseDso> UpdateAsync(StudentModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating StudentModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<StudentModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for StudentModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if StudentModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("StudentModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of StudentModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<StudentModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all StudentModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<StudentModelResponseDso>>(results.Data);
                return new PagedResponse<StudentModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all StudentModels.");
                return new PagedResponse<StudentModelResponseDso>(new List<StudentModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<StudentModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching StudentModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("StudentModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved StudentModel successfully.");
                return GetMapper().Map<StudentModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving StudentModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting StudentModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("StudentModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting StudentModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<StudentModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<StudentModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} StudentModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} StudentModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple StudentModels.");
            }
        }

        public override async Task<PagedResponse<StudentModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all StudentModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<StudentModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for StudentModel entities.");
                return null;
            }
        }

        public override async Task<StudentModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving StudentModel entity...");
                return GetMapper().Map<StudentModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for StudentModel entity.");
                return null;
            }
        }
    }
}