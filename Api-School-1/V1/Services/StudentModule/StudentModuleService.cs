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
    public class StudentModuleService : BaseService<StudentModuleRequestDso, StudentModuleResponseDso>, IUseStudentModuleService
    {
        private readonly IStudentModuleShareRepository _share;
        public StudentModuleService(IStudentModuleShareRepository buildStudentModuleShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildStudentModuleShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting StudentModule entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for StudentModule entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<StudentModuleResponseDso> CreateAsync(StudentModuleRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new StudentModule entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<StudentModuleResponseDso>(result);
                _logger.LogInformation("Created StudentModule entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating StudentModule entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting StudentModule entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting StudentModule entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<StudentModuleResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all StudentModule entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<StudentModuleResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for StudentModule entities.");
                return null;
            }
        }

        public override async Task<StudentModuleResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving StudentModule entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<StudentModuleResponseDso>(result);
                _logger.LogInformation("Retrieved StudentModule entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for StudentModule entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<StudentModuleResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<StudentModuleResponseDso> for StudentModule entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<StudentModuleResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for StudentModule entities.");
                return null;
            }
        }

        public override async Task<StudentModuleResponseDso> UpdateAsync(StudentModuleRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating StudentModule entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<StudentModuleResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for StudentModule entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if StudentModule exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("StudentModule not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of StudentModule with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<StudentModuleResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all StudentModules with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<StudentModuleResponseDso>>(results.Data);
                return new PagedResponse<StudentModuleResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all StudentModules.");
                return new PagedResponse<StudentModuleResponseDso>(new List<StudentModuleResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<StudentModuleResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching StudentModule by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("StudentModule not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved StudentModule successfully.");
                return GetMapper().Map<StudentModuleResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving StudentModule by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting StudentModule with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("StudentModule with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting StudentModule with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<StudentModuleRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<StudentModuleRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} StudentModules...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} StudentModules deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple StudentModules.");
            }
        }

        public override async Task<PagedResponse<StudentModuleResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all StudentModule entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<StudentModuleResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for StudentModule entities.");
                return null;
            }
        }

        public override async Task<StudentModuleResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving StudentModule entity...");
                return GetMapper().Map<StudentModuleResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for StudentModule entity.");
                return null;
            }
        }
    }
}