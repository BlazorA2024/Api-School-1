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
    /// NameModel interface for RepositoriesRepository.
    /// </summary>
    public interface INameModelShareRepository : IBaseShareRepository<NameModelRequestShareDto, NameModelResponseShareDto> //
    , IBasePublicRepository<NameModelRequestShareDto, NameModelResponseShareDto>
    //  يمكنك  التزويد بكل  دوال   طبقة Builder   ببوابات  الطبقة   هذه نفسها      
    //,INameModelBuilderRepository<NameModelRequestShareDto, NameModelResponseShareDto>
    {
    // Define methods or properties specific to the share repository interface.
    }
}