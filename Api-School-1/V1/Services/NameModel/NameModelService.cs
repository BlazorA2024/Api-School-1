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
    public class NameModelService : BaseService<NameModelRequestDso, NameModelResponseDso>, IUseNameModelService
    {
        private readonly INameModelShareRepository _share;
        public NameModelService(INameModelShareRepository buildNameModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildNameModelShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting NameModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for NameModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<NameModelResponseDso> CreateAsync(NameModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new NameModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<NameModelResponseDso>(result);
                _logger.LogInformation("Created NameModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating NameModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting NameModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting NameModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<NameModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all NameModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<NameModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for NameModel entities.");
                return null;
            }
        }

        public override async Task<NameModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving NameModel entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<NameModelResponseDso>(result);
                _logger.LogInformation("Retrieved NameModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for NameModel entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<NameModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<NameModelResponseDso> for NameModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<NameModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for NameModel entities.");
                return null;
            }
        }

        public override async Task<NameModelResponseDso> UpdateAsync(NameModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating NameModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<NameModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for NameModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if NameModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("NameModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of NameModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<NameModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all NameModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<NameModelResponseDso>>(results.Data);
                return new PagedResponse<NameModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all NameModels.");
                return new PagedResponse<NameModelResponseDso>(new List<NameModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<NameModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching NameModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("NameModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved NameModel successfully.");
                return GetMapper().Map<NameModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving NameModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting NameModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("NameModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting NameModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<NameModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<NameModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} NameModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} NameModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple NameModels.");
            }
        }

        public override async Task<PagedResponse<NameModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all NameModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<NameModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for NameModel entities.");
                return null;
            }
        }

        public override async Task<NameModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving NameModel entity...");
                return GetMapper().Map<NameModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for NameModel entity.");
                return null;
            }
        }
    }
}