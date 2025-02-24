﻿using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class NewsService: BaseEntityService<IAppUnitOfWork, INewsRepository, BLLAppDTO.News, DALAppDTO.News>, INewsService
    {
        public NewsService(IAppUnitOfWork serviceUow, INewsRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new NewsMapper(mapper))
        {
        }
    }
}