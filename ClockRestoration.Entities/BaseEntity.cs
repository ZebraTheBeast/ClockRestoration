using System.ComponentModel.DataAnnotations;

namespace ClockRestoration.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
