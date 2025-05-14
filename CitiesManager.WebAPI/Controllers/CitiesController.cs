using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.WebAPI.DatabaseContext;
using CitiesManager.WebAPI.Models;

namespace CitiesManager.WebAPI.Controllers
{
   
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// To get list of cityIds and CityName from "Cities" Table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _context.Cities
             .OrderBy(temp => temp.CityName).ToListAsync();
            return cities;
        }


        /// <summary>
        /// To get city names based on CityID
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        // GET: api/Cities/5
        [HttpGet("{cityID}")]
        public async Task<ActionResult<City>> GetCity(Guid cityID)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(temp => temp.CityID == cityID);

            if (city == null)
            {
                return Problem(detail: "Invalid CityID",statusCode:400, title: "City Search");
                //return NotFound();
            }

            return city;
        }


        /// <summary>
        /// To Edit city from City table 
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        // PUT: api/Cities/5
        [HttpPut("{cityID}")]
        public async Task<IActionResult> PutCity(Guid cityID, [Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            if (cityID != city.CityID)
            {
                return BadRequest(); //HTTP 400
            }

            var existingCity = await _context.Cities.FindAsync(cityID);
            if (existingCity == null)
            {
                return NotFound(); //HTTP 404
            }

            existingCity.CityName = city.CityName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(cityID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// To insert new cities in table 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<City>> PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cities'  is null.");
            }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { cityID = city.CityID }, city); //api/Cities/67d28f3d-43eb-49c7-916c-5b39172955e5
        }


        /// <summary>
        /// To delete city based on their city names
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound(); //HTTP 404
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent(); //HTTP 200
        }


        private bool CityExists(Guid id)
        {
            return (_context.Cities?.Any(e => e.CityID == id)).GetValueOrDefault();
        }
    }
}