using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentCrudops.Models
{
    [Table("Task")]
    public class Tasks
    {

        [Key]
        [ScaffoldColumn(false)]
        public int TaskId { get; set; }
        [Required(ErrorMessage = "taskname is required")]
        public string? TaskName { get; set; }

        [Required(ErrorMessage = "Description  is required")]
        public string Description { get; set; }
       
    }
}

