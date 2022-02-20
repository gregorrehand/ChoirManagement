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
    /// Controller for projectSheetMusic
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProjectSheetMusicsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Controller for projectSheetMusic
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public ProjectSheetMusicsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/ProjectSheetMusics
        /// <summary>
        /// Get all projectSheetMusic
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.ProjectSheetMusic>>> GetProjectSheetMusics()
        {
            return Ok(await _bll.ProjectSheetMusics.GetAllAsync());
        }

        // GET: api/ProjectSheetMusics/5
        /// <summary>
        /// Get a single projectSheetMusic by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.ProjectSheetMusic>> GetProjectSheetMusic(Guid id)
        {
            return Ok(await _bll.ProjectSheetMusics.FirstOrDefaultAsync(id));
        }

        // PUT: api/ProjectSheetMusics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put projectSheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoProjectSheetMusic"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutProjectSheetMusic(Guid id, PublicApi.DTO.v1.ProjectSheetMusic dtoProjectSheetMusic)
        {
            if (id != dtoProjectSheetMusic.Id)
            {
                return BadRequest();
            }

            var projectSheetMusic = await _bll.ProjectSheetMusics.FirstOrDefaultAsync(dtoProjectSheetMusic.Id);
            if (projectSheetMusic == null)
            {
                return BadRequest();
            }

            projectSheetMusic.ProjectId = dtoProjectSheetMusic.ProjectId;
            projectSheetMusic.SheetMusicId = dtoProjectSheetMusic.SheetMusicId;

            _bll.ProjectSheetMusics.Update(projectSheetMusic);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.ProjectSheetMusics.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/ProjectSheetMusics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post projectSheetMusic
        /// </summary>
        /// <param name="dtoProjectSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PublicApi.DTO.v1.ProjectSheetMusic>> PostProjectSheetMusic(ProjectSheetMusic dtoProjectSheetMusic)
        {
            var bllEntity =
                _mapper.Map<PublicApi.DTO.v1.ProjectSheetMusic, BLL.App.DTO.ProjectSheetMusic>(dtoProjectSheetMusic);
            _bll.ProjectSheetMusics.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.ProjectSheetMusics.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity = _mapper.Map<BLL.App.DTO.ProjectSheetMusic, PublicApi.DTO.v1.ProjectSheetMusic>(updatedEntity);

            return CreatedAtAction("GetProjectSheetMusics", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/ProjectSheetMusics/5
        /// <summary>
        /// Delete projectSheetMusic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteProjectSheetMusic(Guid id)
        {
            var projectSheetMusic = await _bll.ProjectSheetMusics.FirstOrDefaultAsync(id);
            if (projectSheetMusic == null)
            {
                return NotFound();
            }

            _bll.ProjectSheetMusics.Remove(projectSheetMusic);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
