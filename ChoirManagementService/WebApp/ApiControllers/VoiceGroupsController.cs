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
    /// Controller for voiceGroup
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class VoiceGroupsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Controller for voiceGroup
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public VoiceGroupsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/VoiceGroups
        /// <summary>
        /// Get all voiceGroup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.VoiceGroup>>> GetVoiceGroups()
        {
            return Ok(await _bll.VoiceGroups.GetAllAsync());
        }

        // GET: api/VoiceGroups/5
        /// <summary>
        /// Get a single voiceGroup by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.VoiceGroup>> GetVoiceGroup(Guid id)
        {
            return Ok(await _bll.VoiceGroups.FirstOrDefaultAsync(id));
        }

        // PUT: api/VoiceGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put voiceGroup
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoVoiceGroup"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutVoiceGroup(Guid id, PublicApi.DTO.v1.VoiceGroup dtoVoiceGroup)
        {
            if (id != dtoVoiceGroup.Id)
            {
                return BadRequest();
            }

            var voiceGroup = await _bll.VoiceGroups.FirstOrDefaultAsync(dtoVoiceGroup.Id);
            if (voiceGroup == null)
            {
                return BadRequest();
            }

            voiceGroup.Name = dtoVoiceGroup.Name;

            _bll.VoiceGroups.Update(voiceGroup);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.VoiceGroups.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/VoiceGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post voiceGroup
        /// </summary>
        /// <param name="dtoVoiceGroup"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<VoiceGroup>> PostVoiceGroup(VoiceGroup dtoVoiceGroup)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.VoiceGroup, BLL.App.DTO.VoiceGroup>(dtoVoiceGroup);
            _bll.VoiceGroups.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.VoiceGroups.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity = _mapper.Map<BLL.App.DTO.VoiceGroup, PublicApi.DTO.v1.VoiceGroup>(updatedEntity);

            return CreatedAtAction("GetVoiceGroups", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/VoiceGroups/5
        /// <summary>
        /// Delete voiceGroup
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteVoiceGroup(Guid id)
        {
            var voiceGroup = await _bll.VoiceGroups.FirstOrDefaultAsync(id);
            if (voiceGroup == null)
            {
                return NotFound();
            }

            _bll.VoiceGroups.Remove(voiceGroup);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
