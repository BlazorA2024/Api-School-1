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
    public class SchoolModelService : BaseService<SchoolModelRequestDso, SchoolModelResponseDso>, IUseSchoolModelService
    {
        private readonly ISchoolModelShareRepository _share;
        public SchoolModelService(ISchoolModelShareRepository buildSchoolModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildSchoolModelShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting SchoolModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for SchoolModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<SchoolModelResponseDso> CreateAsync(SchoolModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new SchoolModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<SchoolModelResponseDso>(result);
                _logger.LogInformation("Created SchoolModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating SchoolModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting SchoolModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting SchoolModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<SchoolModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all SchoolModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<SchoolModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for SchoolModel entities.");
                return null;
            }
        }

        public override async Task<SchoolModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving SchoolModel entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<SchoolModelResponseDso>(result);
                _logger.LogInformation("Retrieved SchoolModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for SchoolModel entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<SchoolModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<SchoolModelResponseDso> for SchoolModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<SchoolModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for SchoolModel entities.");
                return null;
            }
        }

        public override async Task<SchoolModelResponseDso> UpdateAsync(SchoolModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating SchoolModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<SchoolModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for SchoolModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if SchoolModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("SchoolModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of SchoolModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<SchoolModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all SchoolModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<SchoolModelResponseDso>>(results.Data);
                return new PagedResponse<SchoolModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all SchoolModels.");
                return new PagedResponse<SchoolModelResponseDso>(new List<SchoolModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<SchoolModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching SchoolModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("SchoolModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved SchoolModel successfully.");
                return GetMapper().Map<SchoolModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving SchoolModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting SchoolModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("SchoolModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting SchoolModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<SchoolModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<SchoolModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} SchoolModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} SchoolModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple SchoolModels.");
            }
        }

        public override async Task<PagedResponse<SchoolModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all SchoolModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<SchoolModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for SchoolModel entities.");
                return null;
            }
        }

        public override async Task<SchoolModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving SchoolModel entity...");
                return GetMapper().Map<SchoolModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for SchoolModel entity.");
                return null;
            }
        }
    }
}