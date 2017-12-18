using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Slice.Core.Underwriter.Data.Interfaces;

namespace Slice.Core.Underwriter.Data.Models
{
    public abstract class BaseModel : IBaseModel
    {
        protected BaseModel()
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime? CreatedOn { get; set; }

        [DataMember]
        public DateTime? ModifiedOn { get; set; }

        [DataMember]
        public int? CreatedById { get; set; }

        [DataMember]
        public int? ModifiedById { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public Guid? DeletedBy { get; set; }
    }
}