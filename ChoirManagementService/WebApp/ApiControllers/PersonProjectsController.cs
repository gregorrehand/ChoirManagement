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
using PublicApi.DTO.v1;
using Notification = BLL.App.DTO.Notification;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for personProjects
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonProjectsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller for personProjects
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public PersonProjectsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
            
        }

        // GET: api/PersonProjects
        /// <summary>
        /// Get personProjects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonProjectDTO>>> GetPersonProjects()
        {
            var dto = from personProject in await _bll.PersonProjects.GetAllAsync(User.GetUserId()!.Value)
                select _mapper.Map<PersonProjectDTO>(personProject);
            return Ok(dto);

        }

        // GET: api/PersonProjects/5
        /// <summary>
        /// Get personProject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonProjectDTO>> GetPersonProject(Guid id)
        {
            var bllEntity =
                await _bll.PersonProjects.FirstOrDefaultAsync(id);
            if (bllEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PersonProjectDTO>(bllEntity));
        }
        
        // PUT: api/PersonProjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put personProject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personProject"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonProject(Guid id, PersonProjectDTO personProject)
        {
            var bllEntity = await _bll.PersonProjects.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            if (bllEntity == null)
            {
                return BadRequest();
            }

            bllEntity.Status = personProject.Status!;
            bllEntity.Comment = personProject.Comment;
            bllEntity.ProjectId = personProject.ProjectId;

            _bll.PersonProjects.Update(bllEntity);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.PersonProjects.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
        
        // POST: api/PersonProjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post personProject
        /// </summary>
        /// <param name="personProject"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PersonProjectDTO>> PostPersonProject(PersonProjectDTO personProject)
        {
            var bllEntity = new BLL.App.DTO.PersonProject
                {
                    Status = Constants.Constants.STATUS_PENDING,
                    Comment = "",
                    ProjectId = personProject.ProjectId,
                    AppUserId = personProject.AppUserId, 
                };
                _bll.PersonProjects.Add(bllEntity);
                await _bll.SaveChangesAsync();

                var updatedEntity = _bll.PersonProjects.GetUpdatedEntityAfterSaveChanges(bllEntity);
                var returnEntity =
                    _mapper.Map<BLL.App.DTO.PersonProject, PublicApi.DTO.v1.PersonProjectDTO>(updatedEntity);

                return CreatedAtAction("GetPersonProjects", new {id = returnEntity.Id}, returnEntity);
            
        }
        
        /// <summary>
        /// Post personProjects in bulk
        /// </summary>
        /// <param name="personProjects"></param>
        /// <returns></returns>
        [HttpPost("bulk")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PersonProjectDTO>> PostBulkPersonProject(List<PersonProjectDTO> personProjects)
        {
            /*
             * Ma ei suutnud välja mõelda, kuidas ma saaks siit (ja ka analoogsetest PostBulk meetoditest) äriloogikat
             * BLLi tõsta ilma, et ma kaotaks performance poole peal. Kuna mappimine API.DTO -> BLL.DTO peab toimuma
             * kontrolleris, mitte service-s (sest BLL ei tea public APIst midagi), siis ma paratamatult pean sisse tuleva
             * listi mappimise eesmärgil mingil moel läbi itereerima (kas siis manuaalselt for loopiga või kuhugi mapperi
             * sisse peidetuna, kusagil itereeritakse ikkagi kogu see list mappimise protsessi jooksul läbi.
             *
             * Samuti on mul vaja list läbi itereerida selleks, et igale seotud kasutajale notification saata. By the book
             * see peaks toimuma BLL-is. Aga praegusel juhul ma saan need kaks asja kombineerida üheks iteratsiooniks
             * siinsamas kontrolleris. Kui ma juba nagunii kontrolleris pean itereerima, siis tunudub mõistlik ka lisada
             * notificationid siin. 
             */
            var bllEntities = new List<BLL.App.DTO.PersonProject>();
            foreach (var entity in personProjects)
            {
                bllEntities.Add(new BLL.App.DTO.PersonProject
                {
                    Status = Constants.Constants.STATUS_PENDING,
                    Comment = "",
                    ProjectId = entity.ProjectId,
                    AppUserId = entity.AppUserId
                });
                _bll.Notifications.Add(new Notification()
                {
                    AppUserId = entity.AppUserId,
                    Body = "New rehearsal invite",
                    TimePosted = DateTime.Now
                });
            }
            _bll.PersonProjects.AddRange(bllEntities);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // DELETE: api/PersonProjects/5
        /// <summary>
        /// Delete personProject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeletePersonProject(Guid id)
        {
            var entity = await _bll.PersonProjects.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _bll.PersonProjects.Remove(entity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
