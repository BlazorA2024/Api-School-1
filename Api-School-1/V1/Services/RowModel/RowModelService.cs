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
    public class RowModelService : BaseService<RowModelRequestDso, RowModelResponseDso>, IUseRowModelService
    {
        private readonly IRowModelShareRepository _share;
        public RowModelService(IRowModelShareRepository buildRowModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildRowModelShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting RowModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for RowModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<RowModelResponseDso> CreateAsync(RowModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new RowModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<RowModelResponseDso>(result);
                _logger.LogInformation("Created RowModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating RowModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting RowModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting RowModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<RowModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all RowModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<RowModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for RowModel entities.");
                return null;
            }
        }

        public override async Task<RowModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving RowModel entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<RowModelResponseDso>(result);
                _logger.LogInformation("Retrieved RowModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for RowModel entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<RowModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<RowModelResponseDso> for RowModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<RowModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for RowModel entities.");
                return null;
            }
        }

        public override async Task<RowModelResponseDso> UpdateAsync(RowModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating RowModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<RowModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for RowModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if RowModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("RowModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of RowModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<RowModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all RowModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<RowModelResponseDso>>(results.Data);
                return new PagedResponse<RowModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all RowModels.");
                return new PagedResponse<RowModelResponseDso>(new List<RowModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<RowModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching RowModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("RowModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved RowModel successfully.");
                return GetMapper().Map<RowModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving RowModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting RowModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("RowModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting RowModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<RowModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<RowModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} RowModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} RowModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple RowModels.");
            }
        }

        public override async Task<PagedResponse<RowModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all RowModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<RowModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for RowModel entities.");
                return null;
            }
        }

        public override async Task<RowModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving RowModel entity...");
                return GetMapper().Map<RowModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for RowModel entity.");
                return null;
            }
        }
    }
}