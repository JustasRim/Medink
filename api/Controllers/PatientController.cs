using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private IMedicService _medicService;

        public PatientController(IMedicService medicService)
        {
            _medicService = medicService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _medicService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _medicService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Medic medic)
        {
            return Ok(await _medicService.Create(medic));
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
    }
}
