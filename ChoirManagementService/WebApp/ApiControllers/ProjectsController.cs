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
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// ProjectController
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        /// <summary>
        /// ProjectController
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public ProjectsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Projects
        /// <summary>
        /// Get all projects with concerts, personConcerts, rehearsals, personRehearsals and personProjects attached. All the person objects will be sorted by voiceGroup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            Console.WriteLine(User.IsInRole("admin"));
            
            var dto = (from project in await _bll.Projects.GetAllSortedByVoiceGroup()
                select ProjectMapper.MapToDTO(project)).ToList();
            return Ok(dto);
            
        }

        // GET: api/Projects/5
        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<ProjectDTO>> GetProject(Guid id)
        {
            var bllEntity =
                await _bll.Projects.FirstOrDefaultAsync(id);
            if (bllEntity == null)
            {
                return NotFound();
            }

            return Ok(ProjectMapper.MapToDTO(bllEntity));
        }
        
        // // PUT: api/Projects/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutProject(Guid id, ProjectAddDTO project)
        {
            var bllEntity = await _bll.Projects.FirstOrDefaultAsync(id);
            if (bllEntity == null)
            {
                return BadRequest();
            }

            bllEntity.Name = project.Name;
            bllEntity.Info = project.Info;
            bllEntity.Programme = project.Programme;
            _bll.Projects.UpdateWithNotification(bllEntity);
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Projects.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }
        
        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<ProjectDTO>> PostProject(ProjectAddDTO project)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.ProjectAddDTO, BLL.App.DTO.Project>(project);
            _bll.Projects.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Projects.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity =
                _mapper.Map<BLL.App.DTO.Project, PublicApi.DTO.v1.ProjectDTO>(updatedEntity);

            return CreatedAtAction("GetProjects", new {id = returnEntity.Id}, returnEntity);
        }
        
        // DELETE: api/Projects/5
        /// <summary>
        /// Delete project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var entity = await _bll.Projects.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _bll.Projects.Remove(entity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
