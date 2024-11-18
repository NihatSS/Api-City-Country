using ApiPractice.Data;
using ApiPractice.Model;
using ApiPractice.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ApiPractice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Cities.Include(x=>x.Country).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityDTO model)
        {
            await _context.Cities.AddAsync(new City()
            {
                Name = model.Name,
                CountryId = model.CountryId,
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            City existCity = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

            if (existCity is null) return NotFound();

            _context.Cities.Remove(existCity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CityDTO requst)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city is null) return NotFound();
            city.Name = requst.Name ?? city.Name;
            city.CountryId = requst.CountryId ?? city.CountryId;

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            City city = await _context.Cities.Include(x=>x.Country)
                                             .FirstOrDefaultAsync(x => x.Id == id);
            if (city is null) return NotFound();
            return Ok(city);
        }

        [HttpGet("{searchText}")]
        public async Task<IActionResult> Search([FromRoute] string searchText)
        {
            var city = await _context.Cities.Include(x => x.Country)
                                            .Where(x => x.Name.ToLower()
                                                              .Contains(searchText.ToLower()))
                                            .ToListAsync();
            if (city is null) return NotFound();

            return Ok(city);
        }
    }
}
