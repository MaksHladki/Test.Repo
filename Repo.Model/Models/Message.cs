using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Repo.Infrastructure.Enums;
using Repo.Model.Models.Common;

namespace Repo.Model.Models
{
    [Table("Message")]
    [JsonObject(MemberSerialization.OptIn)]
    public class Message : Entity
    {
        [JsonProperty("label")]
        [MaxLength(128)]
        [Required]
        public string Label { get; set; }

        [JsonProperty("body")]
        [MaxLength(2048)]
        [Required]
        public string Body { get; set; }

        [JsonProperty("status", ItemConverterType = typeof(StringEnumConverter))]
        public MessageStatus Status { get; set; }

        [JsonProperty("sent")]
        public DateTime? DateSent { get; set; }

        [JsonProperty("received")]
        public DateTime? DateReceived { get; set; }

        [JsonProperty("from")]
        public Guid UserFromId { get; set; }

        [JsonProperty("to")]
        public Guid UserToId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserFromId")]
        public virtual User UserFrom { get; set; }

        [JsonIgnore]
        [ForeignKey("UserToId")]
        public virtual User UserTo { get; set; }
    }
}
