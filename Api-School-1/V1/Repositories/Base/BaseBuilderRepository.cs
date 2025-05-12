using ApiSchool.Data;
using ApiSchool.Models;
using AutoMapper;
using AutoGenerator;
using AutoGenerator.Repositories.Builder;
using System;

namespace V1.Repositories.Base
{
    /// <summary>
    /// BaseRepository class for ShareRepository.
    /// </summary>
    public abstract class BaseBuilderRepository<TModel, TBuildRequestDto, TBuildResponseDto> : TBaseBuilderRepository<TModel, TBuildRequestDto, TBuildResponseDto>, IBaseBuilderRepository<TBuildRequestDto, TBuildResponseDto>, ITBuildRepository where TModel : class where TBuildRequestDto : class where TBuildResponseDto : class
    {
        public BaseBuilderRepository(DataContext context, IMapper mapper, ILogger logger) : base(new BaseRepository<TModel>(context, logger), mapper, logger)
        {
        }
    }
}