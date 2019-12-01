using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VMController : ControllerBase
    {
        private readonly BackEndDBContext _context;

        public VMController(BackEndDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VM>>> GetVMs()
        {
            return await _context.VMs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VM>> GetVM(Guid Id)
        {
            var vm = await _context.VMs.FindAsync(Id);

            if (vm == null)
            {
                return NotFound();
            }
            return vm;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVM(Guid Id, VM vm)
        {
            if (Id != vm.VMId)
            {
                return BadRequest();
            }

            _context.Entry(vm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VMExists(Id))
                {
                    return NotFound();
                }
                else
                { throw; }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VM>> PostVM(VM vm)
        {
            _context.VMs.Add(vm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVM", new { Id = vm.VMId }, vm);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VM>> DeleteVM(Guid Id)
        {
            var vm = await _context.VMs.FindAsync(Id);
            if (vm == null)
            {
                return NotFound();
            }

            _context.VMs.Remove(vm);
            await _context.SaveChangesAsync();

            return vm;
        }

        private bool VMExists(Guid Id)
        {
            return _context.VMs.Any(v => v.VMId == Id);
        }

    }
}
