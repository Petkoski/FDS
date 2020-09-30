using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FDS2.Data.Models
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string Checksum { get; set; }

        public virtual IEnumerable<PackageFile> PackageFiles { get; set; }
        public virtual IEnumerable<UpdateFile> UpdateFiles { get; set; }
    }
}
