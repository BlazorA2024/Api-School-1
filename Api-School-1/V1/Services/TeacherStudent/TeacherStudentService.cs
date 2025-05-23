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

namespace V1.Services.Services
{
    public class TeacherStudentService : BaseService<TeacherStudentRequestDso, TeacherStudentResponseDso>, IUseTeacherStudentService
    {
        private readonly ITeacherStudentShareRepository _share;
        public TeacherStudentService(ITeacherStudentShareRepository buildTeacherStudentShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildTeacherStudentShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting TeacherStudent entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for TeacherStudent entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<TeacherStudentResponseDso> CreateAsync(TeacherStudentRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new TeacherStudent entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<TeacherStudentResponseDso>(result);
                _logger.LogInformation("Created TeacherStudent entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating TeacherStudent entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting TeacherStudent entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting TeacherStudent entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<TeacherStudentResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherStudent entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<TeacherStudentResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherStudent entities.");
                return null;
            }
        }

        public override async Task<TeacherStudentResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving TeacherStudent entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<TeacherStudentResponseDso>(result);
                _logger.LogInformation("Retrieved TeacherStudent entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for TeacherStudent entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<TeacherStudentResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<TeacherStudentResponseDso> for TeacherStudent entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<TeacherStudentResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for TeacherStudent entities.");
                return null;
            }
        }

        public override async Task<TeacherStudentResponseDso> UpdateAsync(TeacherStudentRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating TeacherStudent entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<TeacherStudentResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for TeacherStudent entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if TeacherStudent exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("TeacherStudent not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of TeacherStudent with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<TeacherStudentResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherStudents with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<TeacherStudentResponseDso>>(results.Data);
                return new PagedResponse<TeacherStudentResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherStudents.");
                return new PagedResponse<TeacherStudentResponseDso>(new List<TeacherStudentResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<TeacherStudentResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching TeacherStudent by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("TeacherStudent not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved TeacherStudent successfully.");
                return GetMapper().Map<TeacherStudentResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving TeacherStudent by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting TeacherStudent with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("TeacherStudent with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherStudent with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<TeacherStudentRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<TeacherStudentRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} TeacherStudents...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} TeacherStudents deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple TeacherStudents.");
            }
        }

        public override async Task<PagedResponse<TeacherStudentResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherStudent entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<TeacherStudentResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherStudent entities.");
                return null;
            }
        }

        public override async Task<TeacherStudentResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving TeacherStudent entity...");
                return GetMapper().Map<TeacherStudentResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for TeacherStudent entity.");
                return null;
            }
        }
    }
}