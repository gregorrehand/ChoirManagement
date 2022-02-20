﻿using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IConcertRepository : IBaseRepository<Concert>, IConcertRepositoryCustom<Concert>
    {
        
    }
    
    public interface IConcertRepositoryCustom<TEntity>
    {
    }
}