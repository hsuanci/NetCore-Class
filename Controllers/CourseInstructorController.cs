using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcoreClass.Models;

namespace netcoreClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public CourseInstructorController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // DELETE: api/CourseInstructor/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CourseInstructor>> DeleteCourseInstructor(int id)
        {
            var courseInstructor = await _context.CourseInstructor.FindAsync(id);
            if (courseInstructor == null)
            {
                return NotFound();
            }

            _context.CourseInstructor.Remove(courseInstructor);
            await _context.SaveChangesAsync();

            return courseInstructor;
        }

        // GET: api/CourseInstructor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseInstructor>>> GetCourseInstructor()
        {
            return await _context.CourseInstructor.ToListAsync();
        }

        // GET: api/CourseInstructor/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CourseInstructor>> GetCourseInstructor(int id)
        {
            var courseInstructor = await _context.CourseInstructor.FindAsync(id);

            if (courseInstructor == null)
            {
                return NotFound();
            }

            return courseInstructor;
        }

        // POST: api/CourseInstructor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CourseInstructor>> PostCourseInstructor(CourseInstructor courseInstructor)
        {
            _context.CourseInstructor.Add(courseInstructor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseInstructorExists(courseInstructor.CourseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourseInstructor", new { id = courseInstructor.CourseId }, courseInstructor);
        }

        // PUT: api/CourseInstructor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutCourseInstructor(int id, CourseInstructor courseInstructor)
        {
            if (id != courseInstructor.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(courseInstructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseInstructorExists(id))
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

        private bool CourseInstructorExists(int id)
        {
            return _context.CourseInstructor.Any(e => e.CourseId == id);
        }
    }
}