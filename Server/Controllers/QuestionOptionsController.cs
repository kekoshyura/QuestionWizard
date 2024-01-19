using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers {

    [Route("api/[controller]")]
    [ApiController]

    public class QuestionOptionsController : ControllerBase {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public QuestionOptionsController(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }



        #region CRUD operations

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get() {

            List<QuestionOptionModel> questions = await _context.QuestionOptions.ToListAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id) {
            QuestionOptionModel section = await GetQuestionOptionById(id);
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionOptionDTO questionOptionToCreateDTO) {
            try {

                if (questionOptionToCreateDTO == null) {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                QuestionOptionModel questionOptionToCreate = _mapper.Map<QuestionOptionModel>(questionOptionToCreateDTO);

                await _context.QuestionOptions.AddAsync(questionOptionToCreate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }

                else {
                    return Created("Create", questionOptionToCreate);
                }
            }
            catch (Exception e) {

                return StatusCode(500, $"Something went wrong on our side. Please contact to administrator. Error message {e.Message}.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionOptionDTO questionOptionToUpdateDTO) {
            try {
                if (id < 1 || questionOptionToUpdateDTO == null || id != questionOptionToUpdateDTO.Id) {
                    return BadRequest(ModelState);
                }

                var oldQuestionOption = await _context.QuestionOptions.FindAsync(id);
                if (oldQuestionOption == null) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                QuestionOptionModel questionOptionToUpdate = _mapper.Map<QuestionOptionModel>(questionOptionToUpdateDTO);
                _context.Entry(oldQuestionOption).State = EntityState.Detached;

                _context.QuestionOptions.Update(questionOptionToUpdate);

                bool changesPersistedToDatabase = await PersistsChangesToDatabase();
                if (changesPersistedToDatabase == false) {
                    return StatusCode(500, $"Something went wrong on our side. Please contact to administrator.");
                }
                else {
                    return Created("Create", questionOptionToUpdate);
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

                bool exists = await _context.QuestionOptions.AnyAsync(x => x.Id == id);
                if (exists == false) {
                    return NotFound();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                var questionOptionToDelete = await GetQuestionOptionById(id);
                if (questionOptionToDelete == null) {
                    return NotFound();
                }

                _context.QuestionOptions.Remove(questionOptionToDelete);

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
        public async Task<QuestionOptionModel> GetQuestionOptionById(int questionOptionId) {
            var questionOptionToGet = await _context.QuestionOptions.FirstAsync(s => s.Id == questionOptionId);
            return questionOptionToGet;
        }
        #endregion
    }
}
