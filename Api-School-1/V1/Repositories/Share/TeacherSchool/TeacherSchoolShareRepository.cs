using AutoMapper;
using ApiSchool.Data;
using ApiSchool.Models;
using V1.Repositories.Base;
using AutoGenerator.Repositories.Builder;
using V1.DyModels.Dto.Build.Requests;
using V1.DyModels.Dto.Build.Responses;
using AutoGenerator;
using V1.Repositories.Builder;
using AutoGenerator.Repositories.Share;
using System.Linq.Expressions;
using AutoGenerator.Repositories.Base;
using AutoGenerator.Helper;
using V1.DyModels.Dto.Share.Requests;
using V1.DyModels.Dto.Share.Responses;
using System;

namespace V1.Repositories.Share
{
    /// <summary>
    /// TeacherSchool class for ShareRepository.
    /// </summary>
    public class TeacherSchoolShareRepository : BaseShareRepository<TeacherSchoolRequestShareDto, TeacherSchoolResponseShareDto, TeacherSchoolRequestBuildDto, TeacherSchoolResponseBuildDto>, ITeacherSchoolShareRepository
    {
        // Declare the builder repository.
        private readonly TeacherSchoolBuilderRepository _builder;
        /// <summary>
        /// Constructor for TeacherSchoolShareRepository.
        /// </summary>
        public TeacherSchoolShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new TeacherSchoolBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(TeacherSchoolShareRepository).FullName));
        // Initialize the logger.
        }

        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting TeacherSchool entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for TeacherSchool entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<TeacherSchoolResponseShareDto> CreateAsync(TeacherSchoolRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new TeacherSchool entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created TeacherSchool entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating TeacherSchool entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<TeacherSchoolResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all TeacherSchool entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TeacherSchool entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<TeacherSchoolResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving TeacherSchool entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for TeacherSchool entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<TeacherSchoolResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<TeacherSchoolResponseShareDto> for TeacherSchool entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for TeacherSchool entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to save changes to the database.
        /// </summary>
        public Task SaveChangesAsync()
        {
            try
            {
                _logger.LogInformation("Saving changes to the database for TeacherSchool entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for TeacherSchool entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<TeacherSchoolResponseShareDto> UpdateAsync(TeacherSchoolRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating TeacherSchool entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
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
                var exists = await _builder.ExistsAsync(value, name);
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

        public override async Task<PagedResponse<TeacherSchoolResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all TeacherSchools with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<TeacherSchoolResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TeacherSchools.");
                return new PagedResponse<TeacherSchoolResponseShareDto>(new List<TeacherSchoolResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<TeacherSchoolResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching TeacherSchool by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("TeacherSchool not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved TeacherSchool successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving TeacherSchool by ID: {Id}", id);
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            return _builder.DeleteAsync(id);
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting TeacherSchool with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("TeacherSchool with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TeacherSchool with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<TeacherSchoolRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<TeacherSchoolRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} TeacherSchools...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} TeacherSchools deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple TeacherSchools.");
            }
        }

        public override async Task<PagedResponse<TeacherSchoolResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  TeacherSchool entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for TeacherSchool entities as pagination.");
                return null;
            }
        }

        public override async Task<TeacherSchoolResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving TeacherSchool entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for TeacherSchool entity.");
                return null;
            }
        }
    }
}