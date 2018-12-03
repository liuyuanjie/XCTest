using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Xcelerator.Entity;
using Xcelerator.Model;
using Xcelerator.Repositories.Interfaces;
using Xcelerator.Service.Interfaces;

namespace Xcelerator.Service
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public void Add(AuditDTO entity)
        {
            _auditRepository.Add(Mapper.Map<AuditDTO, Audit>(entity));
        }

        public IEnumerable<AuditDTO> Find(Expression<Func<AuditDTO, bool>> predicate)
        {
            return Mapper.Map<IEnumerable<Audit>, IEnumerable<AuditDTO>>(
                _auditRepository.Find(Mapper.Map<Expression<Func<AuditDTO, bool>>, Expression<Func<Audit, bool>>>(predicate)));
        }

        public async Task<AuditDTO> GetAsync(int id)
        {
            return Mapper.Map<Audit, AuditDTO>(await _auditRepository.GetAsync(id));
        }

        public bool Any(Expression<Func<AuditDTO, bool>> predicate)
        {
            return _auditRepository.Any(Mapper.Map<Expression<Func<AuditDTO, bool>>, Expression<Func<Audit, bool>>>(predicate));
        }

        public IEnumerable<AuditDTO> FindAll()
        {
            return Mapper.Map<IEnumerable<Audit>, IEnumerable<AuditDTO>>(_auditRepository.FindAll());
        }

        public void Remove(AuditDTO entity)
        {
            _auditRepository.Remove(Mapper.Map<AuditDTO, Audit>(entity));
        }

        public void Update(AuditDTO entity)
        {
            _auditRepository.Update(Mapper.Map<AuditDTO, Audit>(entity));
        }
    }
}