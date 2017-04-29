using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Repo.Model.Models.Common;

namespace Repo.Model.Models
{
    [Table("User")]
    [JsonObject(MemberSerialization.OptIn)]
    public class User : Entity
    {
        [JsonProperty("login")]
        [MinLength(4), MaxLength(64)]
        [Required]
        [Index("Index_User_Login", IsUnique = true)]
        public string Login { get; set; }

        [JsonProperty("firstName")]
        [MinLength(2), MaxLength(64)]
        [Required]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        [MinLength(2), MaxLength(64)]
        [Required]
        public string LastName { get; set; }

        [JsonIgnore]
        [NotMapped]
        public string FullName => $"{FirstName} ${LastName}";

        [JsonProperty("locked")]
        public bool? IsLocked { get; set; }

        [JsonIgnore]
        [MaxLength(128)]
        [Required]
        public string Salt { get; set; }

        [JsonIgnore]
        [MaxLength(1024)]
        [Required]
        public string Hash { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<Message> MessagesFrom { get; set; }

        [JsonIgnore]
        public virtual ICollection<Message> MessagesTo { get; set; }
    }
}
