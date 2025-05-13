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
    public class TeacherSchoolService : BaseService<TeacherSchoolRequestDso, TeacherSchoolResponseDso>, IUseTeacherSchoolService
    {
        private readonly ITeacherSchoolShareRepository _share;
        public TeacherSchoolService(ITeacherSchoolShareRepository buildTeacherSchoolShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildTeacherSchoolShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting TeacherSchool entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for TeacherSchool entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<TeacherSchoolResponseDso> CreateAsync(TeacherSchoolRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new TeacherSchool entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<TeacherSchoolResponseDso>(result);
                _logger.LogInformation("Created TeacherSchool entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating TeacherSchool entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting TeacherSchool entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting TeacherSchool entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<TeacherSchoolResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherSchool entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<TeacherSchoolResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherSchool entities.");
                return null;
            }
        }

        public override async Task<TeacherSchoolResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving TeacherSchool entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<TeacherSchoolResponseDso>(result);
                _logger.LogInformation("Retrieved TeacherSchool entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for TeacherSchool entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<TeacherSchoolResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<TeacherSchoolResponseDso> for TeacherSchool entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<TeacherSchoolResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for TeacherSchool entities.");
                return null;
            }
        }

        public override async Task<TeacherSchoolResponseDso> UpdateAsync(TeacherSchoolRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating TeacherSchool entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<TeacherSchoolResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for TeacherSchool entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if TeacherSchool exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("TeacherSchool not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of TeacherSchool with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<TeacherSchoolResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherSchools with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<TeacherSchoolResponseDso>>(results.Data);
                return new PagedResponse<TeacherSchoolResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherSchools.");
                return new PagedResponse<TeacherSchoolResponseDso>(new List<TeacherSchoolResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<TeacherSchoolResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching TeacherSchool by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("TeacherSchool not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved TeacherSchool successfully.");
                return GetMapper().Map<TeacherSchoolResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving TeacherSchool by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting TeacherSchool with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("TeacherSchool with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherSchool with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<TeacherSchoolRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<TeacherSchoolRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} TeacherSchools...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} TeacherSchools deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple TeacherSchools.");
            }
        }

        public override async Task<PagedResponse<TeacherSchoolResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherSchool entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<TeacherSchoolResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherSchool entities.");
                return null;
            }
        }

        public override async Task<TeacherSchoolResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving TeacherSchool entity...");
                return GetMapper().Map<TeacherSchoolResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for TeacherSchool entity.");
                return null;
            }
        }
    }
}