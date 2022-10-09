using Application.Services;
using Domain.Entities;
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
            return Ok(await _patientService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _patientService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Patient patient)
        {
            return Ok(await _patientService.Create(patient));
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
