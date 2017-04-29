using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Repo.Model.Models.Common;

namespace Repo.Model.Models
{
    [Table("Role")]
    [JsonObject(MemberSerialization.OptIn)]
    public class Role : Entity
    {
        [JsonProperty("name")]
        [MinLength(2), MaxLength(64)]
        [Required]
        [Index("Index_Role_Name", IsUnique = true)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
