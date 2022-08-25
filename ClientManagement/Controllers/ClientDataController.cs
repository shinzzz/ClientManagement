using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientManagement;

namespace ClientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDataController : Controller
    {
        private readonly ClientDBContext _context;

        public ClientDataController(ClientDBContext context)
        {
            _context = context;
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ClientData>>> Get(int? id)
        {
            if (id == null || _context.ClientData == null)
            {
                return NotFound();
            }
            var clientData = await _context.ClientData
               .FirstOrDefaultAsync(m => m.ID == id);
            if (clientData == null)
            {
                return NotFound();
            }
            return Ok(clientData);


        }

        [HttpGet]
        public async Task<ActionResult<List<ClientData>>> GetAll()
        {


            var clientData = await _context.ClientData.ToListAsync();

            if (clientData == null)
            {
                return NotFound();
            }

            return Ok(clientData);


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,EmailID,Address,ContactNumber,Industry")] ClientData clientData)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientDataExists(clientData.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return Ok(clientData);
        }

        private bool ClientDataExists(int id)
        {
            return (_context.ClientData?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,EmailID,Address,ContactNumber,Industry")] ClientData clientData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientData);
                _context.SaveChanges();

            }

            return Ok(clientData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.ClientData == null)
            {
                return Problem("Entity set 'ClientDBContext.ClientData'  is null.");
            }
            var clientData = await _context.ClientData.FindAsync(id);
            if (clientData != null)
            {
                _context.ClientData.Remove(clientData);
            }

            await _context.SaveChangesAsync();
            return Ok(clientData);
        }
    }
}
