using AutoMapper;
using ApiSchool.Data;
using ApiSchool.Models;
using V1.Repositories.Base;
using AutoGenerator.Repositories.Builder;
using V1.DyModels.Dto.Build.Requests;
using V1.DyModels.Dto.Build.Responses;
using System;

namespace V1.Repositories.Builder
{
    /// <summary>
    /// SchoolModel class property for BuilderRepository.
    /// </summary>
     //
    public class SchoolModelBuilderRepository : BaseBuilderRepository<SchoolModel, SchoolModelRequestBuildDto, SchoolModelResponseBuildDto>, ISchoolModelBuilderRepository<SchoolModelRequestBuildDto, SchoolModelResponseBuildDto>
    {
        /// <summary>
        /// Constructor for SchoolModelBuilderRepository.
        /// </summary>
        public SchoolModelBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
        {
        // Initialize necessary fields or call base constructor.
        ///
        /// 
         
        /// 
        }
    //
    // Add additional methods or properties as needed.
    }
}