using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Repo.Model.Models.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Entity
    {
        [JsonProperty("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        [MaxLength(128)]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedAt { get; set; }

        [JsonIgnore]
        [MaxLength(128)]
        public string ModifiedBy { get; set; }
    }
}
