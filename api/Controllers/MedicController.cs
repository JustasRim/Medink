using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicController : Controller
    {
        private IMedicService _medicService;

        public MedicController(IMedicService medicService)
        {
            _medicService = medicService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var medics = await _medicService.Get();

            if (medics == null || medics.Count == 0)
            {
                return NoContent();
            }

            return Ok(medics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var medic = await _medicService.Get(id);

            if (medic == null)
            {
                return NoContent();
            }

            return Ok(medic);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Medic medic)
        {
            if (await _medicService.Create(medic) >= 0)
            {
                return Created("/api/v1/Medic", medic);
            }
            
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Medic medic)
        {
            return Ok(await _medicService.Update(medic));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _medicService.Delete(id));
        }

        [HttpGet("{medicId}/patient/{patientId}")]
        public IActionResult GetSymptoms(int medicId, int patientId)
        {
            var symptoms = _medicService.GetSymptoms(medicId, patientId);
            if (symptoms == null || symptoms.Count == 0)
            {
                return NoContent();
            }

            return Ok(symptoms);
        }
    }
}
