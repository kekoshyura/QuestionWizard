using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase {
        private readonly DataContext _context;

        public SectionsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSevtions() {

            List<SectionModel> sections = await _context.Sections.ToListAsync();
            return Ok(sections);
        }
    }
}
