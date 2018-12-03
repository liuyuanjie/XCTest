using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xcelerator.Api.Configurations.Authorization;
using Xcelerator.Model;
using Xcelerator.Repositories.Interfaces;
using Xcelerator.Service.Interfaces;

namespace Xcelerator.Api.Controllers
{
    [Route("api/audits")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;

        public AuditController(IUnitOfWork unitOfWork, IAuditService auditService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
        }

        // GET: api/Audit
        [HttpGet]
        public IEnumerable<AuditDTO> GetAudits()
        {
            return _auditService.FindAll();
        }

        // GET: api/Audit/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetAudit([FromRoute] int id)
        {
            var audit = await _auditService.GetAsync(id);

            if (audit == null)
            {
                return NotFound();
            }

            return Ok(audit);
        }

        // PUT: api/Audit/5
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequiredAuditEditPolicy)]
        public async Task<IActionResult> PutAudit([FromRoute] int id, [FromBody] AuditDTO audit)
        {
            if (id != audit.Id)
            {
                return BadRequest();
            }

            _auditService.Update(audit);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                if (!AuditExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Audit
        [HttpPost]
        public async Task<IActionResult> PostAudit([FromBody] AuditDTO audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _auditService.Add(audit);
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

            var audit = await _auditService.GetAsync(id);
            if (audit == null)
            {
                return NotFound();
            }

            _auditService.Add(audit);
            await _unitOfWork.SaveChangesAsync();

            return Ok(audit);
        }

        private bool AuditExists(int id)
        {
            return _auditService.Any(e => e.Id == id);
        }
    }
}