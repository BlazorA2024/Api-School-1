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
using V1.DyModels.VMs;

namespace V1.Services.Services
{
    public interface IUseSchoolModelService : ISchoolModelService<SchoolModelRequestDso, SchoolModelResponseDso>, IBaseService//يمكنك  التزويد بكل  دوال   طبقة Builder   ببوابات  الطبقة   هذه نفسها
    //, ISchoolModelBuilderRepository<SchoolModelRequestDso, SchoolModelResponseDso>
    , IBasePublicRepository<SchoolModelRequestDso, SchoolModelResponseDso>
    {
       
        Task<IEnumerable<SchoolModelResponseDso>> SearchByNameAsync(string name);

    }
}