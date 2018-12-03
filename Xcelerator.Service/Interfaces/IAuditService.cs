using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xcelerator.Model;

namespace Xcelerator.Service.Interfaces
{
    public interface IAuditService
    {
        void Add(AuditDTO entity);
        void Update(AuditDTO entity);
        void Remove(AuditDTO entity);
        IEnumerable<AuditDTO> FindAll();
        IEnumerable<AuditDTO> Find(Expression<Func<AuditDTO, bool>> predicate);
        Task<AuditDTO> GetAsync(int id);
        bool Any(Expression<Func<AuditDTO, bool>> predicate);
    }
}
