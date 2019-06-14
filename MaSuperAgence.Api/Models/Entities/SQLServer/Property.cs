using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaSuperAgence.Api.Models.Entities.SQLServer
{
  [Table("Properties")]
  public partial class Property
    {
        public Property()
        {
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Surface { get; set; }
        public int? Rooms { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
