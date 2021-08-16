using System;
using System.Collections.Generic;
using System.Text;

namespace Davli.Framework.Orm.Models.Impl
{
    public class DavliEntity<TPrimaryKey> : IDavliEntity, ICreationConcept, IModificationConcept
    {
        public TPrimaryKey Id { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
