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
using PublicApi.DTO.v1.Mappers;
using Notification = BLL.App.DTO.Notification;


namespace WebApp.ApiControllers
{
    /// <summary>
    /// PersonRehearsalsController
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonRehearsalsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        private PersonRehearsalMapper _personRehearsalMapper;
        /// <summary>
        /// PersonRehearsalsController
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public PersonRehearsalsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;

            _personRehearsalMapper = new PersonRehearsalMapper(_mapper);
            
        }

        // GET: api/PersonRehearsals
        /// <summary>
        /// Get all personRehearsals for logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonRehearsalDTO>>> GetPersonRehearsals()
        {
            var dto = (from personRehearsal in await _bll.PersonRehearsals.GetAllAsync(User.GetUserId()!.Value)
                select PersonRehearsalMapper.MapToDTO(personRehearsal)).ToList();
            return Ok(dto);
        }
    
        // GET: api/PersonRehearsals
        /// <summary>
        /// Get all personRehearsals for logged in user with status (pending, declined or accepted)
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<PersonRehearsalDTO>>> GetPersonRehearsalsByStatus(string status)
        {
            var dto = (from personRehearsal in await _bll.PersonRehearsals.GetAllByStatus(User.GetUserId()!.Value, status)
                select PersonRehearsalMapper.MapToDTO(personRehearsal)).ToList();
            return Ok(dto);
        }
        
        // GET: api/PersonRehearsals/5
        /// <summary>
        /// Get personRehearsal by id for logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonRehearsalDTO>> GetPersonRehearsal(Guid id)
        {
            var bllEntity =
                await _bll.PersonRehearsals.FirstOrDefaultAsync(id,
                    User.GetUserId()!.Value);
            if (bllEntity == null)
            {
                return NotFound();
            }

            return Ok(PersonRehearsalMapper.MapToDTO(bllEntity));
        }
        
        // PUT: api/PersonRehearsals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change personRehearsal by changed in user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personRehearsal"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonRehearsal(Guid id, PersonRehearsalAddDTO personRehearsal)
        {
            var bllEntity = await _bll.PersonRehearsals.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            if (bllEntity == null)
            {
                return BadRequest();
            }

            bllEntity.Status = personRehearsal.Status;
            bllEntity.Comment = personRehearsal.Comment;

            _bll.PersonRehearsals.Update(bllEntity);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.PersonRehearsals.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
        
        // POST: api/PersonRehearsals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create personRehearsal
        /// </summary>
        /// <param name="personRehearsal"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PersonRehearsalDTO>> PostPersonRehearsal(PersonRehearsalAddDTO personRehearsal)
        {
            var bllEntity = new BLL.App.DTO.PersonRehearsal
                {
                    Status = Constants.Constants.STATUS_PENDING,
                    Comment = "",
                    RehearsalId = personRehearsal.RehearsalID,
                    AppUserId = personRehearsal.AppUserId //TODO: praegu võtab lihtsalt current user id, peab saama muuta
                };
                _bll.PersonRehearsals.Add(bllEntity);
                await _bll.SaveChangesAsync();

                var updatedEntity = _bll.PersonRehearsals.GetUpdatedEntityAfterSaveChanges(bllEntity);
                var returnEntity =
                    _mapper.Map<BLL.App.DTO.PersonRehearsal, PublicApi.DTO.v1.PersonRehearsalDTO>(updatedEntity);

                return CreatedAtAction("GetPersonRehearsals", new {id = returnEntity.Id}, returnEntity);
            
        }
        
        /// <summary>
        /// Create personRehearsals in bulk
        /// </summary>
        /// <param name="personRehearsals"></param>
        /// <returns></returns>
        [HttpPost("bulk")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PersonRehearsalDTO>> PostBulkPersonRehearsal(List<PersonRehearsalAddDTO> personRehearsals)
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
            var bllEntities = new List<BLL.App.DTO.PersonRehearsal>();
            foreach (var personRehearsal in personRehearsals)
            {
                bllEntities.Add(new BLL.App.DTO.PersonRehearsal
                {
                    Status = Constants.Constants.STATUS_PENDING,
                    Comment = "",
                    RehearsalId = personRehearsal.RehearsalID,
                    AppUserId = personRehearsal.AppUserId
                });
                _bll.Notifications.Add(new Notification()
                {
                    AppUserId = personRehearsal.AppUserId,
                    Body = "New rehearsal invite",
                    TimePosted = DateTime.Now
                });
            }
            _bll.PersonRehearsals.AddRange(bllEntities);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        
        // DELETE: api/PersonRehearsals/5
        /// <summary>
        /// Delete personRehearsal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeletePersonRehearsal(Guid id)
        {
            var entity = await _bll.PersonRehearsals.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _bll.PersonRehearsals.Remove(entity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
