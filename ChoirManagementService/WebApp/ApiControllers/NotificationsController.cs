using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for notifications
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller for notifications
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public NotificationsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
            
        }

        // GET: api/Notifications
        /// <summary>
        /// Get notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return Ok(await _bll.Notifications.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: api/Notifications/5
        /// <summary>
        /// Get a notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(Guid id)
        {
            return Ok(await _bll.Notifications.FirstOrDefaultAsync(id, User.GetUserId()!.Value));
        }
        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post news
        /// </summary>
        /// <param name="dtoNotification"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PublicApi.DTO.v1.Notification>> PostNotification(Notification dtoNotification)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.Notification, BLL.App.DTO.Notification>(dtoNotification);
            
            _bll.Notifications.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Notifications.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity = _mapper.Map<BLL.App.DTO.Notification, PublicApi.DTO.v1.Notification>(updatedEntity);

            return CreatedAtAction("GetNotifications", new { id = returnEntity.Id }, returnEntity);
        }
    }
}
