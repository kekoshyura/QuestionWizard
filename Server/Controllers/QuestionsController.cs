using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class QuestionsController : ControllerBase {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionsController(IMapper mapper, DataContext context) {
            _mapper = mapper;
            _context = context;
        }

        #region CRUD operations
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get() {

            List<QuestionModel> questions = await _context.Questions
                                                        .Include(x => x.QuestionOptions)
                                                        .ToListAsync();

            return Ok(questions);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id) {
            QuestionModel section = await GetQuestionById(id);
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionDTO questionToCreateDTO) {
            try {

                if (questionToCreateDTO == null) {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                QuestionModel questionToCreate = _mapper.Map<QuestionModel>(questionToCreateDTO);

                await _context.Questions.AddAsync(questionToCreate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }

                else {
                    return Created("Create", questionToCreate);
                }
            }
            catch (Exception e) {

                return StatusCode(500, $"Something went wrong on our side. Please contact to administrator. Error message {e.Message}.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionDTO questionToCreateDTO) {
            try {
                if (id < 1 || questionToCreateDTO == null || id != questionToCreateDTO.Id) {
                    return BadRequest(ModelState);
                }

                var oldQuestion = await _context.Questions.FindAsync(id);
                if (oldQuestion == null) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                QuestionModel questionToUpdate = _mapper.Map<QuestionModel>(questionToCreateDTO);
                _context.Entry(oldQuestion).State = EntityState.Detached;

                _context.Questions.Update(questionToUpdate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }
                else {
                    return Created("Create", questionToUpdate);
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

                bool exists = await _context.Questions.AnyAsync(x => x.Id == id);
                if (exists == false) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                var questionToDelete = await GetQuestionById(id);
                if (questionToDelete == null) {
                    return NotFound();
                }

                _context.Questions.Remove(questionToDelete);

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

        #region Utility Methods
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> PersistsChangesToDatabase() {
            int amountChanges = await _context.SaveChangesAsync();
            return amountChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<QuestionModel> GetQuestionById(int questionId) {
            var questionToGet = await _context.Questions.FirstAsync(s => s.Id == questionId);
            return questionToGet;
        }
        #endregion
    }
}
