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
    /// StudentModel class property for BuilderRepository.
    /// </summary>
     //
    public class StudentModelBuilderRepository : BaseBuilderRepository<StudentModel, StudentModelRequestBuildDto, StudentModelResponseBuildDto>, IStudentModelBuilderRepository<StudentModelRequestBuildDto, StudentModelResponseBuildDto>
    {
        /// <summary>
        /// Constructor for StudentModelBuilderRepository.
        /// </summary>
        public StudentModelBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
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