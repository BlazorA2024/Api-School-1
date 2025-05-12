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
    public class ModuleModelService : BaseService<ModuleModelRequestDso, ModuleModelResponseDso>, IUseModuleModelService
    {
        private readonly IModuleModelShareRepository _share;
        public ModuleModelService(IModuleModelShareRepository buildModuleModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildModuleModelShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting ModuleModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for ModuleModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<ModuleModelResponseDso> CreateAsync(ModuleModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new ModuleModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<ModuleModelResponseDso>(result);
                _logger.LogInformation("Created ModuleModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ModuleModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting ModuleModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting ModuleModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<ModuleModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all ModuleModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<ModuleModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ModuleModel entities.");
                return null;
            }
        }

        public override async Task<ModuleModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving ModuleModel entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<ModuleModelResponseDso>(result);
                _logger.LogInformation("Retrieved ModuleModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for ModuleModel entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<ModuleModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<ModuleModelResponseDso> for ModuleModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<ModuleModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for ModuleModel entities.");
                return null;
            }
        }

        public override async Task<ModuleModelResponseDso> UpdateAsync(ModuleModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating ModuleModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<ModuleModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for ModuleModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if ModuleModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("ModuleModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of ModuleModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<ModuleModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all ModuleModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<ModuleModelResponseDso>>(results.Data);
                return new PagedResponse<ModuleModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ModuleModels.");
                return new PagedResponse<ModuleModelResponseDso>(new List<ModuleModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<ModuleModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching ModuleModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("ModuleModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved ModuleModel successfully.");
                return GetMapper().Map<ModuleModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving ModuleModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting ModuleModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("ModuleModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ModuleModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<ModuleModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<ModuleModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} ModuleModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} ModuleModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple ModuleModels.");
            }
        }

        public override async Task<PagedResponse<ModuleModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all ModuleModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<ModuleModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ModuleModel entities.");
                return null;
            }
        }

        public override async Task<ModuleModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving ModuleModel entity...");
                return GetMapper().Map<ModuleModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for ModuleModel entity.");
                return null;
            }
        }
    }
}