using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SectionsController : ControllerBase {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public SectionsController(DataContext context, IWebHostEnvironment webHostEnvironment, IMapper mapper) {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        #region CRUD operations

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get() {

            List<SectionModel> sections = await _context.Sections.Include(x => x.Surveys).ToListAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id) {
            SectionModel section = await GetSectionById(id);
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SectionDTO sectionToCreateDTO) {
            try {

                if (sectionToCreateDTO == null) {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                SectionModel sectionToCreate = _mapper.Map<SectionModel>(sectionToCreateDTO);

                await _context.Sections.AddAsync(sectionToCreate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }

                else {
                    return Created("Create", sectionToCreate);
                }
            }
            catch (Exception e) {

                return StatusCode(500, $"Something went wrong on our side. Please contact to administrator. Error message {e.Message}.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SectionDTO sectionToCreateDTO) {
            try {
                if (id < 1 || sectionToCreateDTO == null || id != sectionToCreateDTO.Id) {
                    return BadRequest(ModelState);
                }

                var oldSection = await _context.Sections.FindAsync(id);
                if (oldSection == null) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                SectionModel sectionToUpdate = _mapper.Map<SectionModel>(sectionToCreateDTO);
                _context.Entry(oldSection).State = EntityState.Detached;

                _context.Sections.Update(sectionToUpdate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }
                else {
                    return NoContent();
                }
            }
            catch (Exception e) {

                return StatusCode(500, $"Something went wrong on our side. Please contact to administrator. Error message {e.Message}.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id < 1) {
                    return BadRequest(ModelState);
                }

                bool exists = await _context.Sections.AnyAsync(x => x.Id == id);
                if (exists == false) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                var sectionToDelete = await GetSectionById(id);
                if (sectionToDelete == null) {
                    return NotFound();
                }

                _context.Sections.Remove(sectionToDelete);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }

                else {
                    return NoContent();
                }
            }
            catch (Exception e) {

                return StatusCode(500, $"Something went wrong on our side. Please contact to administrator. Error message {e.Message}.");
            }
        }

        #endregion


        #region Utility methods

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> PersistsChangesToDatabase() {
            int amountChanges = await _context.SaveChangesAsync();
            return amountChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<SectionModel> GetSectionById(int sectionId) {
            var sectionToGet = await _context.Sections.FirstAsync(s => s.Id == sectionId);
            return sectionToGet;
        }

        #endregion
    }
}
