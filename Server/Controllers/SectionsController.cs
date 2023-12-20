using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SectionsController(DataContext context, IWebHostEnvironment webHostEnvironment) {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        #region CRUD operations

        [HttpGet]
        public async Task<IActionResult> Get() {

            List<SectionModel> sections = await _context.Sections.Include(x => x.Surveys).ToListAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            SectionModel section = await GetSectionById(id);
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SectionModel sectionToCreate) {
            try {

                if (sectionToCreate == null) {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

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
        public async Task<IActionResult> Update(int id, [FromBody] SectionModel sectionToUpdate) {
            try {
                if (id < 1 || sectionToUpdate == null || id != sectionToUpdate.Id) {
                    return BadRequest(ModelState);
                }

                bool exists = await _context.Sections.AnyAsync(x => x.Id == id);
                if (exists == false) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

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
