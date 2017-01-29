using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public partial class File
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Data { get; set; }

        [Required]
        public string MimeType { get; set; }
    }
}
