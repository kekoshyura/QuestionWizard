using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase {
        private readonly DataContext _context;

        public SurveysController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSevtions() {

            List<SurveyModel> sections = await _context.Surveys.ToListAsync();
            return Ok(sections);
        }
    }
}
