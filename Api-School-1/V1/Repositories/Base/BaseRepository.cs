using AutoGenerator.Repositories.Base;
using ApiSchool.Data;
using ApiSchool.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace V1.Repositories.Base
{
    /// <summary>
    /// BaseRepository class for ShareRepository.
    /// </summary>
    public sealed class BaseRepository<T> : TBaseRepository<ApplicationUser, IdentityRole, string, T>, IBaseRepository<T> where T : class
    {
        public BaseRepository(DataContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}