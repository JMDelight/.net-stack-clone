using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
