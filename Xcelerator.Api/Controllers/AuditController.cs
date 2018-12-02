using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xcelerator.Api.Configurations.Authorization;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Repositories.Interfaces;

namespace Xcelerator.Api.Controllers
{
    [Route("api/audits")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditRepository _auditRepository;

        public AuditController(IUnitOfWork unitOfWork, IAuditRepository auditRepository)
        {
            _unitOfWork = unitOfWork;
            _auditRepository = auditRepository;
        }

        // GET: api/Audit
        [HttpGet]
        public IEnumerable<Audit> GetAudits()
        {
            return _auditRepository.Find(null);
        }

        // GET: api/Audit/5
        [HttpGet("{id}")]
        [Authorize(Roles ="Client")]
        public async Task<IActionResult> GetAudit([FromRoute] int id)
        {
            var audit = await _auditRepository.GetAsync(id);

            if (audit == null)
            {
                return NotFound();
            }

            return Ok(audit);
        }

        // PUT: api/Audit/5
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequiredAuditEditPolicy)]
        public async Task<IActionResult> PutAudit([FromRoute] int id, [FromBody] Audit audit)
        {
            if (id != audit.Id)
            {
                return BadRequest();
            }

            _auditRepository.Update(audit);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditExists(id))
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

        // POST: api/Audit
        [HttpPost]
        public async Task<IActionResult> PostAudit([FromBody] Audit audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _auditRepository.Add(audit);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetAudit", new { id = audit.Id }, audit);
        }

        // DELETE: api/Audit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var audit = await _auditRepository.GetAsync(id);
            if (audit == null)
            {
                return NotFound();
            }

            _auditRepository.Add(audit);
            await _unitOfWork.SaveChangesAsync();

            return Ok(audit);
        }

        private bool AuditExists(int id)
        {
            return _auditRepository.Any(e => e.Id == id);
        }
    }
}