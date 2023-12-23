using AutoMapper;
using Core.Models;
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
