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
    /// TeacherModule class property for BuilderRepository.
    /// </summary>
     //
    public class TeacherModuleBuilderRepository : BaseBuilderRepository<TeacherModule, TeacherModuleRequestBuildDto, TeacherModuleResponseBuildDto>, ITeacherModuleBuilderRepository<TeacherModuleRequestBuildDto, TeacherModuleResponseBuildDto>
    {
        /// <summary>
        /// Constructor for TeacherModuleBuilderRepository.
        /// </summary>
        public TeacherModuleBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
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