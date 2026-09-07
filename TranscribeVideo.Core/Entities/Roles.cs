using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TranscribeVideo.Core.Entities
{
    [Table("Roles")]
    public class Roles
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
