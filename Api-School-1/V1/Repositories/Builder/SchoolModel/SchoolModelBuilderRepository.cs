using AutoMapper;
using ApiSchool.Data;
using ApiSchool.Models;
using V1.Repositories.Base;
using AutoGenerator.Repositories.Builder;
using V1.DyModels.Dto.Build.Requests;
using V1.DyModels.Dto.Build.Responses;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace V1.Repositories.Builder
{
    /// <summary>
    /// SchoolModel class property for BuilderRepository.
    /// </summary>
     //
    public class SchoolModelBuilderRepository : BaseBuilderRepository<SchoolModel, SchoolModelRequestBuildDto, SchoolModelResponseBuildDto>, ISchoolModelBuilderRepository<SchoolModelRequestBuildDto, SchoolModelResponseBuildDto>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;  // ≈÷«›… «·Œ«’Ì… Â‰«
        /// <summary>
        /// Constructor for SchoolModelBuilderRepository.
        /// </summary>
        public SchoolModelBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
        {
        // Initialize necessary fields or call base constructor.
        ///
        /// 
         _dbContext = dbContext;
            _mapper = mapper;
            /// 
        }
        public async Task<List<SchoolModelResponseBuildDto>> SearchAsync(string name)
        {
            try
            {
                _logger.LogInformation("Searching for schools with name containing: {Name}", name);

                var query = _dbContext.Schools.AsQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(s => s.Name != null && EF.Functions.Like(s.Name, $"%{name}%"));
                }

                var result = await query.AsNoTracking().ToListAsync();

                _logger.LogInformation("Found {Count} schools matching the name: {Name}", result.Count, name);

                // ? «· ÕÊÌ· ≈·Ï DTO »«” Œœ«„ AutoMapper
                return _mapper.Map<List<SchoolModelResponseBuildDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for schools with name: {Name}", name);
                return new List<SchoolModelResponseBuildDto>();
            }
        }



        //
        // Add additional methods or properties as needed.
    }
}