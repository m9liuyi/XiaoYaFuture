using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XiaoYaFuture.Common;
using XiaoYaFuture.Data.Entity.Context;
using XiaoYaFuture.DataAcessLayer.Interface;
using XiaoYaFuture.QueryParameter;
using XiaoYaFuture.Utility;
using Z.BulkOperations;

namespace XiaoYaFuture.DataAcessLayer
{
    public class BaseDal<T, E> : IBaseDal<T, E> 
        where T : BaseDto
        where E : BaseEntity
    {
        private XYFContext context { get; set; }


        public BaseDal(XYFContext context)
        {
            this.context = context;
        }

        #region IBaseDal<T> Members

        public List<T> List<M>(M queryParameter) 
            where M : BaseQueryParameters
        {
            var wheres = Extension.QueryToWhere<E,M>(queryParameter);

            IQueryable<E> iQueryable = context.Set<E>();
            if (wheres != null)
            {
                iQueryable = iQueryable.Where(wheres);
            }

            var result = iQueryable.ToList();

            return result.S2T<E, T>();
        }

        public List<T> BulkInsert(List<T> entities)
        {
            List<AuditEntry> auditEntries = new List<AuditEntry>();

            context.BulkInsert(entities, options =>
            {
                options.UseAudit = true;
                options.BulkOperationExecuted = bulkOperation => auditEntries.AddRange(bulkOperation.AuditEntries);
            });

            return auditEntries.AuditEntryToEntity<T>();
        }

        public List<T> BulkUpdate(List<T> entities)
        {
            List<AuditEntry> auditEntries = new List<AuditEntry>();

            context.BulkUpdate(entities, options =>
            {
                options.UseAudit = true;
                options.BulkOperationExecuted = bulkOperation => auditEntries.AddRange(bulkOperation.AuditEntries);
            });

            return auditEntries.SelectMany(p => p.Values.Select(x => x.NewValue)) as List<T>;
        }

        public List<T> BulkRemove(List<T> entities)
        {
            List<AuditEntry> auditEntries = new List<AuditEntry>();

            context.BulkDelete(entities, options =>
            {
                options.UseAudit = true;
                options.BulkOperationExecuted = bulkOperation => auditEntries.AddRange(bulkOperation.AuditEntries);
            });

            return auditEntries.SelectMany(p => p.Values.Select(x => x.NewValue)) as List<T>;
        }

        public List<T> BulkRemoveById<M>(M query)
            where M : BaseQueryParameters
        {
            if (query == null
                || !(query.PrimaryKey == null || (query.PrimaryKeys == null || query.PrimaryKeys.Count() == 0)))
            {
                return new List<T>();
            }

            List<AuditEntry> auditEntries = new List<AuditEntry>();

            var wheres = Extension.QueryToWhere<E, M>(query);

            context.Set<E>().Where(wheres).DeleteFromQuery<E>(options =>
            {
                options.UseAudit = true;
                options.BulkOperationExecuted = bulkOperation => auditEntries.AddRange(bulkOperation.AuditEntries);
            });

            return auditEntries.AuditEntryToEntity<T>();
        }

        public List<T> BulkDelete(List<T> entities)
        {
            List<AuditEntry> auditEntries = new List<AuditEntry>();

            context.BulkUpdate(entities, options =>
            {
                options.UseAudit = true;
                options.BulkOperationExecuted = bulkOperation => auditEntries.AddRange(bulkOperation.AuditEntries);
            });

            return auditEntries.SelectMany(p => p.Values.Select(x => x.NewValue)) as List<T>;
        }

        public List<T> BulkDeleteById<M>(M query) 
            where M : BaseQueryParameters
        {

            if (query == null 
                || !(query.PrimaryKey == null || (query.PrimaryKeys == null || query.PrimaryKeys.Count() == 0)))
            {
                return new List<T>();
            }

            List<AuditEntry> auditEntries = new List<AuditEntry>();

            var wheres = Extension.QueryToWhere<E, M>(query);

            var columnValues = new Dictionary<string, object>();
            columnValues.Add("IsDeleted", true);

            context.Set<E>().Where(wheres).UpdateFromQuery<E>(p => Extension.PrepareT4UpdateFromQuery<E>(columnValues));

            return auditEntries.AuditEntryToEntity<T>();
        }
        
        #endregion
    }
}
