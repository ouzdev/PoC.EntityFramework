using System.Text.Json.Serialization;

namespace Common.Entity
{
    public class BaseEntity<T> : Entity<T>
    {
        /// <summary>
        /// Add JsonIgnore to prevent serialization
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }
   
}
