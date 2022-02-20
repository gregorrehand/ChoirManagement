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
using Microsoft.VisualBasic;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using Notification = BLL.App.DTO.Notification;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// PersonConcertController
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonConcertsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        private PersonConcertMapper _personConcertMapper;


        /// <summary>
        /// Controller for personconcerts
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public PersonConcertsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;

            _personConcertMapper = new PersonConcertMapper(_mapper);
        }

        // GET: api/PersonConcerts
        /// <summary>
        /// Get personConcerts for logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonConcertDTO>>> GetPersonConcerts()
        {
            var dto = (from personConcert in await _bll.PersonConcerts.GetAllAsync(User.GetUserId()!.Value)
                select PersonConcertMapper.MapToDTO(personConcert)).ToList();
            return Ok(dto);
        }

        // GET: api/PersonConcerts
        /// <summary>
        /// Get person concert by status (pending, accepted or declined)
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<PersonConcertDTO>>> GetPersonConcertsByStatus(string status)
        {
            var dto = (from personConcert in await _bll.PersonConcerts.GetAllByStatus(User.GetUserId()!.Value, status)
                select PersonConcertMapper.MapToDTO(personConcert)).ToList();
            return Ok(dto);
        }

        // GET: api/PersonConcerts/5
        /// <summary>
        /// GEt personconcert by id for logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonConcertDTO>> GetPersonConcert(Guid id)
        {
            var bllEntity =
                await _bll.PersonConcerts.FirstOrDefaultAsync(id,
                    User.GetUserId()!.Value);
            if (bllEntity == null)
            {
                return NotFound();
            }

            return Ok(PersonConcertMapper.MapToDTO(bllEntity));
        }

        // PUT: api/PersonConcerts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put personConcert by logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personConcert"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonConcert(Guid id, PersonConcertAddDTO personConcert)
        {
            var bllEntity = await _bll.PersonConcerts.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            if (bllEntity == null)
            {
                return BadRequest();
            }

            bllEntity.Status = personConcert.Status!;
            bllEntity.Comment = personConcert.Comment;

            _bll.PersonConcerts.Update(bllEntity);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.PersonConcerts.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/PersonConcerts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new personConcert
        /// </summary>
        /// <param name="personConcert"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PersonConcertDTO>> PostPersonConcert(PersonConcertAddDTO personConcert)
        {

            var bllEntity = new BLL.App.DTO.PersonConcert
            {
                Status = Constants.Constants.STATUS_PENDING,
                Comment = "",
                ConcertId = personConcert.ConcertId,
                AppUserId = personConcert.AppUserId
            };
            _bll.PersonConcerts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.PersonConcerts.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity =
                _mapper.Map<BLL.App.DTO.PersonConcert, PublicApi.DTO.v1.PersonConcertDTO>(updatedEntity);

            return CreatedAtAction("GetPersonConcerts", new {id = returnEntity.Id}, returnEntity);
        }
        
        /// <summary>
        /// Create bulk personConcerts
        /// </summary>
        /// <param name="personConcerts"></param>
        /// <returns></returns>
        [HttpPost("bulk")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PersonConcertDTO>> PostBulkPersonConcert(List<PersonConcertAddDTO> personConcerts)
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
            var bllEntities = new List<BLL.App.DTO.PersonConcert>();
            foreach (var personConcert in personConcerts)
            {
                bllEntities.Add(new BLL.App.DTO.PersonConcert
                {
                    Status = Constants.Constants.STATUS_PENDING,
                    Comment = "",
                    ConcertId = personConcert.ConcertId,
                    AppUserId = personConcert.AppUserId
                });
                _bll.Notifications.Add(new Notification()
                {
                    AppUserId = personConcert.AppUserId,
                    Body = "New concert invite",
                    TimePosted = DateTime.Now
                });
            }
            _bll.PersonConcerts.AddRange(bllEntities);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // DELETE: api/PersonConcerts/5
        /// <summary>
        /// Delete personConcert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeletePersonConcert(Guid id)
        {
            var entity = await _bll.PersonConcerts.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _bll.PersonConcerts.Remove(entity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}


