using ApiPractice.Data;
using ApiPractice.Model;
using ApiPractice.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Countries.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryDTO model)
        {
            await _context.Countries.AddAsync(new Country()
            {
                Name = model.Name,
                Population = model.Population,
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Country existCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (existCountry is null) return NotFound();

            _context.Countries.Remove(existCountry);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CountryDTO requst)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x=>x.Id == id);
            if (country is null) return NotFound();
            country.Name = requst.Name ?? country.Name;
            country.Population = requst.Population ?? country.Population;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country is null) return NotFound();
            return Ok(country);
        }

        [HttpGet("{searchText}")]
        public async Task<IActionResult> Search([FromRoute] string searchText)
        {
            var country = await _context.Countries.Where(x => x.Name.ToLower()
                                                                    .Contains(searchText.ToLower()))
                                                  .ToListAsync();
            if (country is null) return NotFound();

            return Ok(country);
        }
    }
}
