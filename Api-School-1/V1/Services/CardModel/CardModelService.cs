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
    public class CardModelService : BaseService<CardModelRequestDso, CardModelResponseDso>, IUseCardModelService
    {
        private readonly ICardModelShareRepository _share;
        public CardModelService(ICardModelShareRepository buildCardModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildCardModelShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting CardModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for CardModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<CardModelResponseDso> CreateAsync(CardModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new CardModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<CardModelResponseDso>(result);
                _logger.LogInformation("Created CardModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating CardModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting CardModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting CardModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<CardModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all CardModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<CardModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for CardModel entities.");
                return null;
            }
        }

        public override async Task<CardModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving CardModel entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<CardModelResponseDso>(result);
                _logger.LogInformation("Retrieved CardModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for CardModel entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<CardModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<CardModelResponseDso> for CardModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<CardModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for CardModel entities.");
                return null;
            }
        }

        public override async Task<CardModelResponseDso> UpdateAsync(CardModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating CardModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<CardModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for CardModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if CardModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("CardModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of CardModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<CardModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all CardModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<CardModelResponseDso>>(results.Data);
                return new PagedResponse<CardModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all CardModels.");
                return new PagedResponse<CardModelResponseDso>(new List<CardModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<CardModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching CardModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("CardModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved CardModel successfully.");
                return GetMapper().Map<CardModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving CardModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting CardModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("CardModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting CardModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<CardModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<CardModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} CardModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} CardModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple CardModels.");
            }
        }

        public override async Task<PagedResponse<CardModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all CardModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<CardModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for CardModel entities.");
                return null;
            }
        }

        public override async Task<CardModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving CardModel entity...");
                return GetMapper().Map<CardModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for CardModel entity.");
                return null;
            }
        }
    }
}