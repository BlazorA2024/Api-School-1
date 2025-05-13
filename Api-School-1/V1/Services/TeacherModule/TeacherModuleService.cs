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
    public class TeacherModuleService : BaseService<TeacherModuleRequestDso, TeacherModuleResponseDso>, IUseTeacherModuleService
    {
        private readonly ITeacherModuleShareRepository _share;
        public TeacherModuleService(ITeacherModuleShareRepository buildTeacherModuleShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildTeacherModuleShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting TeacherModule entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for TeacherModule entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<TeacherModuleResponseDso> CreateAsync(TeacherModuleRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new TeacherModule entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<TeacherModuleResponseDso>(result);
                _logger.LogInformation("Created TeacherModule entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating TeacherModule entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting TeacherModule entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting TeacherModule entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<TeacherModuleResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherModule entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<TeacherModuleResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherModule entities.");
                return null;
            }
        }

        public override async Task<TeacherModuleResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving TeacherModule entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<TeacherModuleResponseDso>(result);
                _logger.LogInformation("Retrieved TeacherModule entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for TeacherModule entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<TeacherModuleResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<TeacherModuleResponseDso> for TeacherModule entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<TeacherModuleResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for TeacherModule entities.");
                return null;
            }
        }

        public override async Task<TeacherModuleResponseDso> UpdateAsync(TeacherModuleRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating TeacherModule entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<TeacherModuleResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for TeacherModule entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if TeacherModule exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("TeacherModule not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of TeacherModule with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<TeacherModuleResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherModules with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<TeacherModuleResponseDso>>(results.Data);
                return new PagedResponse<TeacherModuleResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherModules.");
                return new PagedResponse<TeacherModuleResponseDso>(new List<TeacherModuleResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<TeacherModuleResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching TeacherModule by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("TeacherModule not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved TeacherModule successfully.");
                return GetMapper().Map<TeacherModuleResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving TeacherModule by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting TeacherModule with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("TeacherModule with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherModule with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<TeacherModuleRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<TeacherModuleRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} TeacherModules...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} TeacherModules deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple TeacherModules.");
            }
        }

        public override async Task<PagedResponse<TeacherModuleResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherModule entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<TeacherModuleResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherModule entities.");
                return null;
            }
        }

        public override async Task<TeacherModuleResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving TeacherModule entity...");
                return GetMapper().Map<TeacherModuleResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for TeacherModule entity.");
                return null;
            }
        }
    }
}