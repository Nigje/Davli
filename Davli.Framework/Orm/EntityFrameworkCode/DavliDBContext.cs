
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Davli.Framework.Models;
using Davli.Framework.Orm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Davli.Framework.Orm.EntityFrameworkCode
{
    public class DavliDBContext : DbContext
    {
        //********************************************************************************************************
        //Variables:
        //Todo: fix DI
        public RequestContext RequestContext { get; set; }
        //********************************************************************************************************

        //public BaseDBContext(DbContextOptions options) : base(options)
        //{
        //}
        //********************************************************************************************************

        public DavliDBContext(DbContextOptions options, RequestContext requestContext) : base(options)
        {
            RequestContext = requestContext;
        }
        //********************************************************************************************************
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        //********************************************************************************************************
        public override int SaveChanges()
        {
            ApplyDavliConcepts();
            return base.SaveChanges();
        }
        //********************************************************************************************************
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyDavliConcepts();
            return await base.SaveChangesAsync(cancellationToken);
        }
        //********************************************************************************************************
        private void ApplyDavliConcepts()
        {
            var userId = GetAuditUserId();
            foreach (var entry in base.ChangeTracker.Entries().ToList())
            {
                ApplyDavliConcepts(entry, userId);
            }
        }
        //********************************************************************************************************
        private void ApplyDavliConcepts(EntityEntry entry, long? userId)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyAbpConceptsForAddedEntity(entry, userId);
                    break;
                case EntityState.Modified:
                    ApplyAbpConceptsForModifiedEntity(entry, userId);
                    break;
                case EntityState.Deleted:
                    //Do nothing
                    //It can be used for safe delete.
                    break;
                case EntityState.Unchanged:
                    //Do nothing.
                    break;
            }
        }

        //********************************************************************************************************
        private void ApplyAbpConceptsForModifiedEntity(EntityEntry entry, long? userId)
        {
            if (entry.Entity is IModificationConcept modifiedEntity)
            {
                modifiedEntity.LastModificationTime = DateTime.Now;
                modifiedEntity.LastModifierUserId = userId;
            }
        }
        //********************************************************************************************************
        private void ApplyAbpConceptsForAddedEntity(EntityEntry entry, long? userId)
        {
            if (entry.Entity is ICreationConcept createdEntity)
            {
                createdEntity.CreationTime = DateTime.Now;
                createdEntity.CreatorUserId = userId;
            }
        }

        //********************************************************************************************************
        private long? GetAuditUserId()
        {
            //return the current user unique number.
            return RequestContext?.UserId;
        }

        //********************************************************************************************************
    }

}
