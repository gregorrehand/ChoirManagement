using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using Notification = BLL.App.DTO.Notification;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// ScheduleController
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SchedulesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;



        /// <summary>
        /// Controller for Schedules
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public SchedulesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Schedules
        /// <summary>
        /// Get Schedules for logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetScheduleByProject()
        {
            var dto = from personProject in await _bll.PersonProjects.GetAllAsync(User.GetUserId()!.Value)
                select ScheduleMapper.MapToDTO(personProject);
            return Ok(dto);        }

        // GET: api/Schedules
        /// <summary>
        /// Get person concert by status (pending, accepted or declined)
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("bydate")]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetScheduleByDate(string status)
        {
            throw new NotImplementedException();

        }
    }
}


