using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SymptomController : Controller
    {
        private ISymptomService _symptomService;

        public SymptomController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _symptomService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _symptomService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Symptom symptom)
        {
            return Ok(await _symptomService.Create(symptom));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Symptom symptom)
        {
            return Ok(await _symptomService.Update(symptom));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _symptomService.Delete(id));
        }
    }
}
