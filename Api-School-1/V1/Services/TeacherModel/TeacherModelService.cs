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
    public class TeacherModelService : BaseService<TeacherModelRequestDso, TeacherModelResponseDso>, IUseTeacherModelService
    {
        private readonly ITeacherModelShareRepository _share;
        private readonly DataContext _context;

        public TeacherModelService(DataContext context, ITeacherModelShareRepository buildTeacherModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildTeacherModelShareRepository;
            _context = context;
        }

        public override async Task<TeacherModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving TeacherModel entity with ID: {id}...");
                // var result = await _share.GetByIdAsync(id);
                var entity = await _context.Teachers
                    .Include(t => t.Name)
                    .Include(t => t.Row)
                    .Include(t => t.TeacherSchools)
                        .ThenInclude(ts => ts.School)
                    .Include(t => t.TeacherModules)
                        .ThenInclude(tm => tm.Module)
                    .Include(t => t.TeacherStudents)
                        .ThenInclude(ts => ts.Student)
                            .ThenInclude(s => s.Name)
                    .FirstOrDefaultAsync(t => t.Id == id);
                var item = GetMapper().Map<TeacherModelResponseDso>(entity);
                _logger.LogInformation("Retrieved TeacherModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for TeacherModel entity with ID: {id}.");
                return null;
            }
        }


        public async Task<IEnumerable<TeacherModelResponseDso>> SearchByTeachersAsync(string name)
        {


            try
            {
                _logger.LogInformation("Searching TeacherModel by name: {name}", name);


                var students = await _context.Teachers
                    .Include(s => s.Name)
                    .ToListAsync();

                var filteredStudents = students
                    .Where(s => s.Name != null && s.Name.FullName.Contains(name))
                    .ToList();

                if (!filteredStudents.Any())
                {
                    _logger.LogWarning("No TeacherModel found with name: {name}", name);
                }

                var result = GetMapper().Map<List<TeacherModelResponseDso>>(filteredStudents);
                _logger.LogInformation(" TeacherModel entity successfully.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching TeacherModel by name: {name}", name);
                return null;
            }
        }
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting TeacherModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for TeacherModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<TeacherModelResponseDso> CreateAsync(TeacherModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new TeacherModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<TeacherModelResponseDso>(result);
                _logger.LogInformation("Created TeacherModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating TeacherModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting TeacherModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting TeacherModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<TeacherModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<TeacherModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherModel entities.");
                return null;
            }
        }

        //public override async Task<TeacherModelResponseDso?> GetByIdAsync(string id)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Retrieving TeacherModel entity with ID: {id}...");
        //        var result = await _share.GetByIdAsync(id);
        //        var item = GetMapper().Map<TeacherModelResponseDso>(result);
        //        _logger.LogInformation("Retrieved TeacherModel entity successfully.");
        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error in GetByIdAsync for TeacherModel entity with ID: {id}.");
        //        return null;
        //    }
        //}

        public override IQueryable<TeacherModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<TeacherModelResponseDso> for TeacherModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<TeacherModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for TeacherModel entities.");
                return null;
            }
        }

        public override async Task<TeacherModelResponseDso> UpdateAsync(TeacherModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating TeacherModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<TeacherModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for TeacherModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if TeacherModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("TeacherModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of TeacherModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<TeacherModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<TeacherModelResponseDso>>(results.Data);
                return new PagedResponse<TeacherModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherModels.");
                return new PagedResponse<TeacherModelResponseDso>(new List<TeacherModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<TeacherModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching TeacherModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("TeacherModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved TeacherModel successfully.");
                return GetMapper().Map<TeacherModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving TeacherModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting TeacherModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("TeacherModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<TeacherModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<TeacherModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} TeacherModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} TeacherModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple TeacherModels.");
            }
        }

        public override async Task<PagedResponse<TeacherModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<TeacherModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherModel entities.");
                return null;
            }
        }

        public override async Task<TeacherModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving TeacherModel entity...");
                return GetMapper().Map<TeacherModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for TeacherModel entity.");
                return null;
            }
        }
    }
}