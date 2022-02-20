using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for concerts
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConcertsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        /// <summary>
        /// Controller for concerts
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public ConcertsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
            
        }

        // GET: api/Concerts
        /// <summary>
        /// Get all concerts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConcertDTO>>> GetConcerts()
        {
            var dto = from concert in await _bll.Concerts.GetAllAsync()
                select _mapper.Map<ConcertDTO>(concert);
            return Ok(dto);
            
        }

        // GET: api/Concerts/5
        /// <summary>
        /// Get concert by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ConcertDTO>> GetConcert(Guid id)
        {
            var bllEntity =
                await _bll.Concerts.FirstOrDefaultAsync(id);
            if (bllEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ConcertDTO>(bllEntity));
        }

        // PUT: api/Concerts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put concert
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concert"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutConcert(Guid id, ConcertAddDTO concert)
        {
            var bllEntity = await _bll.Concerts.FirstOrDefaultAsync(id);
            if (bllEntity == null)
            {
                return BadRequest();
            }

            bllEntity.Name = concert.Name;
            bllEntity.Info = concert.Info;
            bllEntity.Programme = concert.Programme;
            bllEntity.Location = concert.Location;
            bllEntity.Start = concert.Start;
            bllEntity.ProjectId = concert.ProjectId;

            _bll.Concerts.UpdateWithNotification(bllEntity);
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Concerts.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Concerts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post concert
        /// </summary>
        /// <param name="concert"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<ConcertDTO>> PostConcert(ConcertAddDTO concert)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.ConcertAddDTO, BLL.App.DTO.Concert>(concert);

            _bll.Concerts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Concerts.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity =
                _mapper.Map<BLL.App.DTO.Concert, PublicApi.DTO.v1.ConcertDTO>(updatedEntity);

            return CreatedAtAction("GetConcerts", new {id = returnEntity.Id}, returnEntity);
        }

        // DELETE: api/Concerts/5
        /// <summary>
        /// Delete concert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteConcert(Guid id)
        {
            var entity = await _bll.Concerts.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _bll.Concerts.Remove(entity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
