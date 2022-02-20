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
    /// Controller for news
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class NewsesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Controller for news
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public NewsesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Newses
        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.News>>> GetNewses()
        {
            return Ok(await _bll.Newses.GetAllAsync());
        }

        // GET: api/Newses/5
        /// <summary>
        /// Get a single news by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.News>> GetNews(Guid id)
        {
            return Ok(await _bll.Newses.FirstOrDefaultAsync(id));
        }

        // PUT: api/Newses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put news
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoNews"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> PutNews(Guid id, PublicApi.DTO.v1.News dtoNews)
        {
            if (id != dtoNews.Id)
            {
                return BadRequest();
            }

            var news = await _bll.Newses.FirstOrDefaultAsync(dtoNews.Id);
            if (news == null)
            {
                return BadRequest();
            }
            
            news.Body = dtoNews.Body;
            news.Title = dtoNews.Title;
            news.ProjectName = dtoNews.ProjectName;

            _bll.Newses.Update(news);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Newses.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Newses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post news
        /// </summary>
        /// <param name="dtoNews"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<PublicApi.DTO.v1.News>> PostNews(AddNews dtoNews)
        {
            var bllEntity = _mapper.Map<PublicApi.DTO.v1.AddNews, BLL.App.DTO.News>(dtoNews);
            
            _bll.Newses.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Newses.GetUpdatedEntityAfterSaveChanges(bllEntity);
            var returnEntity = _mapper.Map<BLL.App.DTO.News, PublicApi.DTO.v1.News>(updatedEntity);

            return CreatedAtAction("GetNewses", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/Newses/5
        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var news = await _bll.Newses.FirstOrDefaultAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            _bll.Newses.Remove(news);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
