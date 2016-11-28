using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.Models
{
    public class TodoItem
    {
        public string Key { get; set; }

        [StringLength(255, ErrorMessage = "Maximum Length 255 characters")]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
