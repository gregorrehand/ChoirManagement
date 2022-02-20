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
    /// Controller for sheetMusic
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class SheetMusicsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Controller for sheetMusic
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public SheetMusicsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/SheetMusics
        /// <summary>
        /// Get all sheetMusic
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.SheetMusic>>> GetSheetMusics()
        {
            return Ok(await _bll.SheetMusics.GetAllAsync());
        }

        // GET: api/SheetMusics/5
        /// <summary>
        /// Get a single sheetMusic by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.SheetMusic>> GetSheetMusic(Guid id)
        {
            return Ok(await _bll.SheetMusics.FirstOrDefaultAsync(id));
        }

        // PUT: api/SheetMusics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put sheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoSheetMusic"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutSheetMusic(Guid id, PublicApi.DTO.v1.SheetMusic dtoSheetMusic)
        {
            if (id != dtoSheetMusic.Id)
            {
                return BadRequest();
            }

            var sheetMusic = await _bll.SheetMusics.FirstOrDefaultAsync(dtoSheetMusic.Id);
            if (sheetMusic == null)
            {
                return BadRequest();
            }

            sheetMusic.Name = dtoSheetMusic.Name;
            sheetMusic.Content = dtoSheetMusic.Content;

            _bll.SheetMusics.Update(sheetMusic);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.SheetMusics.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/SheetMusics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post sheetMusic
        /// </summary>
        /// <param name="dtoSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PublicApi.DTO.v1.SheetMusic>> PostSheetMusic(SheetMusic dtoSheetMusic)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.SheetMusic, BLL.App.DTO.SheetMusic>(dtoSheetMusic);
            _bll.SheetMusics.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.SheetMusics.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity = _mapper.Map<BLL.App.DTO.SheetMusic, PublicApi.DTO.v1.SheetMusic>(updatedEntity);

            return CreatedAtAction("GetSheetMusics", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/SheetMusics/5
        /// <summary>
        /// Delete sheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteSheetMusic(Guid id)
        {
            var sheetMusic = await _bll.SheetMusics.FirstOrDefaultAsync(id);
            if (sheetMusic == null)
            {
                return NotFound();
            }

            _bll.SheetMusics.Remove(sheetMusic);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
