using api.Attributes;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
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
            var symptoms = await _symptomService.Get();
            if (symptoms == null || symptoms.Count == 0)
            {
                return NoContent();
            }

            return Ok(symptoms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var symptom = await _symptomService.Get(id);
            if (symptom == null)
            {
                return NoContent();
            }

            return Ok(symptom);
        }

        [AuthorizeRoles(Role.Admin, Role.Patient)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Symptom symptom)
        {
            if (await _symptomService.Create(symptom) >= 0)
            {
                return Created("/api/v1/Medic", symptom);
            }

            return BadRequest();
        }

        [AuthorizeRoles(Role.Admin, Role.Patient)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Symptom symptom)
        {
            return Ok(await _symptomService.Update(symptom));
        }

        [AuthorizeRoles(Role.Admin, Role.Patient)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _symptomService.Delete(id));
        }
    }
}
