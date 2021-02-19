using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Table
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Table(string name)
        {
            this.Name = name;
            Link = $@"/Room/{name}";
        }
        public Table() { }
    }
}
