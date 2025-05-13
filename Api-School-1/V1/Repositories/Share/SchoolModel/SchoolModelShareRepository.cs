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
using Microsoft.EntityFrameworkCore;
using V1.DyModels.Dso.Responses;

namespace V1.Repositories.Share
{
    /// <summary>
    /// SchoolModel class for ShareRepository.
    /// </summary>
    public class SchoolModelShareRepository : BaseShareRepository<SchoolModelRequestShareDto, SchoolModelResponseShareDto, SchoolModelRequestBuildDto, SchoolModelResponseBuildDto>, ISchoolModelShareRepository
    {
        // Declare the builder repository.
        private readonly SchoolModelBuilderRepository _builder;
        private readonly DataContext _context;
        /// <summary>
        /// Constructor for SchoolModelShareRepository.
        /// </summary>
        public SchoolModelShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new SchoolModelBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(SchoolModelShareRepository).FullName));
            _context = dbContext;
        // Initialize the logger.
        }
        public async Task<IEnumerable<SchoolModelResponseShareDto>> SearcNameSchoolAsync(string name)
{
    try
    {
        _logger.LogInformation("Searching schools with name containing: {name}", name);

                var schools = await _builder.SearchAsync(name);
                var output = MapToIEnumerableShareResponseDto(await _builder.SearchAsync(name)); // ? «· ’ÕÌÕ Â‰«
                // ? Õ· «·Œÿ√
                _logger.LogInformation("Returned {Count} schools matching the name: {name}", output.Count(), name);

        return output;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred while searching for schools by name: {name}", name);
        return new List<SchoolModelResponseShareDto>();
    }
}

//     public async Task<IEnumerable<SchoolModelResponseShareDto>> SearcNameSchoolAsync(string name)
//{
//    try
//    {
//        _logger.LogInformation("Searching schools with name containing: {name}", name);

//        var schools = await _context.Schools
//            .AsNoTracking()
//            .Where(s => s.Name != null && s.Name.Contains(name))
//            .ToListAsync();

//        var output = MapToIEnumerableShareResponseDto(schools); // ? «· ’ÕÌÕ Â‰«
//        _logger.LogInformation("Returned {Count} schools matching the name: {name}", output.Count(), name);

//        return output;
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex, "Error occurred while searching for schools by name: {name}", name);
//        return new List<SchoolModelResponseShareDto>();
//    }
//}



        //public async Task<IEnumerable<SchoolModelResponseShareDto>> SearchByNameAsync(string name)
        //{
        //    _logger.LogInformation("Searching schools with name containing: {name}", name);

        //    var schools = await _builder
        //       // .AsNoTracking()
        //        .Where(s => s.Name != null && s.Name.Contains(name))
        //        .ToListAsync();

        //    return GetMapper().Map<List<SchoolModelResponseShareDto>>(schools);
        //}
        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting SchoolModel entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for SchoolModel entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<SchoolModelResponseShareDto> CreateAsync(SchoolModelRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new SchoolModel entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created SchoolModel entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating SchoolModel entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<SchoolModelResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all SchoolModel entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for SchoolModel entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<SchoolModelResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving SchoolModel entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for SchoolModel entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<SchoolModelResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<SchoolModelResponseShareDto> for SchoolModel entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for SchoolModel entities.");
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
                _logger.LogInformation("Saving changes to the database for SchoolModel entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for SchoolModel entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<SchoolModelResponseShareDto> UpdateAsync(SchoolModelRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating SchoolModel entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
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
                var exists = await _builder.ExistsAsync(value, name);
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

        public override async Task<PagedResponse<SchoolModelResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all SchoolModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<SchoolModelResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all SchoolModels.");
                return new PagedResponse<SchoolModelResponseShareDto>(new List<SchoolModelResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<SchoolModelResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching SchoolModel by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("SchoolModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved SchoolModel successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving SchoolModel by ID: {Id}", id);
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
                _logger.LogInformation("Deleting SchoolModel with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("SchoolModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting SchoolModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<SchoolModelRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<SchoolModelRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} SchoolModels...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} SchoolModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple SchoolModels.");
            }
        }

        public override async Task<PagedResponse<SchoolModelResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  SchoolModel entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for SchoolModel entities as pagination.");
                return null;
            }
        }

        public override async Task<SchoolModelResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving SchoolModel entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for SchoolModel entity.");
                return null;
            }
        }
    }
}