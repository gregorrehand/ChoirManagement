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
    /// Controller for rehearsalSheetMusic
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class RehearsalSheetMusicsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Controller for rehearsalSheetMusic
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public RehearsalSheetMusicsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/RehearsalSheetMusics
        /// <summary>
        /// Get all rehearsalSheetMusic
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.RehearsalSheetMusic>>> GetRehearsalSheetMusics()
        {
            return Ok(await _bll.RehearsalSheetMusics.GetAllAsync());
        }

        // GET: api/RehearsalSheetMusics/5
        /// <summary>
        /// Get a single rehearsalSheetMusic by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.RehearsalSheetMusic>> GetRehearsalSheetMusic(Guid id)
        {
            return Ok(await _bll.RehearsalSheetMusics.FirstOrDefaultAsync(id));
        }

        // PUT: api/RehearsalSheetMusics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put rehearsalSheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoRehearsalSheetMusic"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutRehearsalSheetMusic(Guid id, PublicApi.DTO.v1.RehearsalSheetMusic dtoRehearsalSheetMusic)
        {
            if (id != dtoRehearsalSheetMusic.Id)
            {
                return BadRequest();
            }

            var rehearsalSheetMusic = await _bll.RehearsalSheetMusics.FirstOrDefaultAsync(dtoRehearsalSheetMusic.Id);
            if (rehearsalSheetMusic == null)
            {
                return BadRequest();
            }

            rehearsalSheetMusic.RehearsalId = dtoRehearsalSheetMusic.RehearsalId;
            rehearsalSheetMusic.SheetMusicId = dtoRehearsalSheetMusic.SheetMusicId;

            _bll.RehearsalSheetMusics.Update(rehearsalSheetMusic);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.RehearsalSheetMusics.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/RehearsalSheetMusics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post rehearsalSheetMusic
        /// </summary>
        /// <param name="dtoRehearsalSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PublicApi.DTO.v1.RehearsalSheetMusic>> PostRehearsalSheetMusic(RehearsalSheetMusic dtoRehearsalSheetMusic)
        {
            var bllEntity =
                _mapper.Map<PublicApi.DTO.v1.RehearsalSheetMusic, BLL.App.DTO.RehearsalSheetMusic>(
                    dtoRehearsalSheetMusic);
            _bll.RehearsalSheetMusics.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.RehearsalSheetMusics.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity = _mapper.Map<BLL.App.DTO.RehearsalSheetMusic, PublicApi.DTO.v1.RehearsalSheetMusic>(updatedEntity);

            return CreatedAtAction("GetRehearsalSheetMusics", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/RehearsalSheetMusics/5
        /// <summary>
        /// Delete rehearsalSheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteRehearsalSheetMusic(Guid id)
        {
            var rehearsalSheetMusic = await _bll.RehearsalSheetMusics.FirstOrDefaultAsync(id);
            if (rehearsalSheetMusic == null)
            {
                return NotFound();
            }

            _bll.RehearsalSheetMusics.Remove(rehearsalSheetMusic);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
