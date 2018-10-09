using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClockRestoration.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
