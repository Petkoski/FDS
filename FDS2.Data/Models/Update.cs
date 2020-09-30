using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FDS2.Data.Models
{
    public class Update
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool CountryRestrictions { get; set; }
        public DateTime? PublishDate { get; set; }

        public virtual Version Version { get; set; }
        public virtual IEnumerable<CountryUpdate> CountryUpdates { get; set; }
        public virtual IEnumerable<UpdateFile> UpdateFiles { get; set; }
    }
}
