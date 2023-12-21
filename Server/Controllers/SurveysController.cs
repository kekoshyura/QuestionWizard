using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SurveysController(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        #region Utility methods
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> PersistsChangesToDatabase() {
            int amountChanges = await _context.SaveChangesAsync();
            return amountChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<SurveyModel> GetSurveyById(int surveyId) {
            var surveyToGet = await _context.Surveys.FirstAsync(s => s.Id == surveyId);
            return surveyToGet;
        }
        #endregion

        #region CRUD operations
        [HttpGet]
        public async Task<IActionResult> Get() {

            List<SurveyModel> sections = await _context.Surveys
                                                    .Include(x => x.Section)
                                                    .ThenInclude(x => x.Surveys)
                                                    .ThenInclude(x => x.Questions)
                                                    .ThenInclude(x => x.QuestionOptions)
                                                    .ToListAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            SurveyModel section = await GetSurveyById(id);
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SurveyDTO surveyToCreateDTO) {
            try {

                if (surveyToCreateDTO == null) {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                SurveyModel syrveyToCreate = _mapper.Map<SurveyModel>(surveyToCreateDTO);

                await _context.Surveys.AddAsync(syrveyToCreate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }

                else {
                    return Created("Create", syrveyToCreate);
                }
            }
            catch (Exception e) {

                return StatusCode(500, $"Something went wrong on our side. Please contact to administrator. Error message {e.Message}.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SurveyDTO surveyToUpdateDTO) {
            try {
                if (id < 1 || surveyToUpdateDTO == null || id != surveyToUpdateDTO.Id) {
                    return BadRequest(ModelState);
                }

                var oldSurvey = await _context.Surveys.FindAsync(id);
                if (oldSurvey == null) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                SurveyModel surveyToUpdate = _mapper.Map<SurveyModel>(surveyToUpdateDTO);
                _context.Surveys.Update(surveyToUpdate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }
                else {
                    return Created("Create", surveyToUpdate);
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

                var sectionToDelete = await GetSurveyById(id);
                if (sectionToDelete == null) {
                    return NotFound();
                }

                _context.Surveys.Remove(sectionToDelete);

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


    }
}
