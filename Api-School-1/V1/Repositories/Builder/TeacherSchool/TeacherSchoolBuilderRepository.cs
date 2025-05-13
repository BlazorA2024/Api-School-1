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
    /// TeacherSchool class property for BuilderRepository.
    /// </summary>
     //
    public class TeacherSchoolBuilderRepository : BaseBuilderRepository<TeacherSchool, TeacherSchoolRequestBuildDto, TeacherSchoolResponseBuildDto>, ITeacherSchoolBuilderRepository<TeacherSchoolRequestBuildDto, TeacherSchoolResponseBuildDto>
    {
        /// <summary>
        /// Constructor for TeacherSchoolBuilderRepository.
        /// </summary>
        public TeacherSchoolBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
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