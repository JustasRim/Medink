using Application.Services;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var patient = await _patientService.Get();
            if (patient == null || patient.Count == 0)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _patientService.Get(id);
            if (patient == null)
            {
                return NoContent();
            }

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Patient patient)
        {
            if (await _patientService.Create(patient) >= 0)
            {
                return Created("/api/v1/Medic", patient);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Patient patient)
        {
            return Ok(await _patientService.Update(patient));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _patientService.Delete(id));
        }
    }
}
