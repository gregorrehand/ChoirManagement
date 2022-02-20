using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for rehearsals
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RehearsalsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        /// <summary>
        /// Controller for rehearsals
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public RehearsalsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
            
        }

        // GET: api/Rehearsals
        /// <summary>
        /// Get rehearsals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RehearsalDTO>>> GetRehearsals()
        {
            var dto = from rehearsal in await _bll.Rehearsals.GetAllAsync()
                select _mapper.Map<RehearsalDTO>(rehearsal);
            return Ok(dto);
            
        }

        // GET: api/Rehearsals/5
        /// <summary>
        /// Get rehearsal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RehearsalDTO>> GetRehearsal(Guid id)
        {
            var bllEntity =
                await _bll.Rehearsals.FirstOrDefaultAsync(id);
            if (bllEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RehearsalDTO>(bllEntity));
        }

        // PUT: api/Rehearsals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put rehearsal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rehearsal"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutRehearsal(Guid id, RehearsalAddDTO rehearsal)
        {
            var bllEntity = await _bll.Rehearsals.FirstOrDefaultAsync(id);
            if (bllEntity == null)
            {
                return BadRequest();
            }

            bllEntity.Info = rehearsal.Info;
            bllEntity.RehearsalProgramme = rehearsal.RehearsalProgramme!;
            bllEntity.Location = rehearsal.Location!;
            bllEntity.Start = rehearsal.Start;
            bllEntity.End = rehearsal.End;
            _bll.Rehearsals.UpdateWithNotification(bllEntity);
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Rehearsals.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Rehearsals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post rehearsal
        /// </summary>
        /// <param name="rehearsal"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<RehearsalDTO>> PostRehearsal(RehearsalAddDTO rehearsal)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.RehearsalAddDTO, BLL.App.DTO.Rehearsal>(rehearsal);
            _bll.Rehearsals.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Rehearsals.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity =
                _mapper.Map<BLL.App.DTO.Rehearsal, PublicApi.DTO.v1.RehearsalDTO>(updatedEntity);

            return CreatedAtAction("GetRehearsals", new {id = returnEntity.Id}, returnEntity);
        }

        // DELETE: api/Rehearsals/5
        /// <summary>
        /// Delete rehearsal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteRehearsal(Guid id)
        {
            var entity = await _bll.Rehearsals.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _bll.Rehearsals.Remove(entity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
