using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for concertSheetMusic
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ConcertSheetMusicsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Controller for concertSheetMusic
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public ConcertSheetMusicsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/ConcertSheetMusics
        /// <summary>
        /// Get all concertSheetMusic
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.ConcertSheetMusicDTO>>> GetConcertSheetMusics()
        {
            return Ok(await _bll.ConcertSheetMusics.GetAllAsync());
        }

        // GET: api/ConcertSheetMusics/5
        /// <summary>
        /// Get a single concertSheetMusic by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.ConcertSheetMusicDTO>> GetConcertSheetMusic(Guid id)
        {
            return Ok(await _bll.ConcertSheetMusics.FirstOrDefaultAsync(id));
        }

        // PUT: api/ConcertSheetMusics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put concertSheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoConcertSheetMusic"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutConcertSheetMusic(Guid id, PublicApi.DTO.v1.ConcertSheetMusicDTO dtoConcertSheetMusic)
        {
            if (id != dtoConcertSheetMusic.Id)
            {
                return BadRequest();
            }

            var concertSheetMusic = await _bll.ConcertSheetMusics.FirstOrDefaultAsync(dtoConcertSheetMusic.Id);
            if (concertSheetMusic == null)
            {
                return BadRequest();
            }

            concertSheetMusic.ConcertId = dtoConcertSheetMusic.ConcertId;
            concertSheetMusic.SheetMusicId = dtoConcertSheetMusic.SheetMusicId;

            _bll.ConcertSheetMusics.Update(concertSheetMusic);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.ConcertSheetMusics.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/ConcertSheetMusics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post concertSheetMusic
        /// </summary>
        /// <param name="dtoConcertSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PublicApi.DTO.v1.ConcertSheetMusicDTO>> PostConcertSheetMusic(ConcertSheetMusicDTO dtoConcertSheetMusic)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.ConcertSheetMusicDTO, BLL.App.DTO.ConcertSheetMusic>(dtoConcertSheetMusic);
            _bll.ConcertSheetMusics.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.ConcertSheetMusics.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity = _mapper.Map<BLL.App.DTO.ConcertSheetMusic, PublicApi.DTO.v1.ConcertSheetMusicDTO>(updatedEntity);

            return CreatedAtAction("GetConcertSheetMusics", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/ConcertSheetMusics/5
        /// <summary>
        /// Delete concertSheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteConcertSheetMusic(Guid id)
        {
            var concertSheetMusic = await _bll.ConcertSheetMusics.FirstOrDefaultAsync(id);
            if (concertSheetMusic == null)
            {
                return NotFound();
            }

            _bll.ConcertSheetMusics.Remove(concertSheetMusic);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
