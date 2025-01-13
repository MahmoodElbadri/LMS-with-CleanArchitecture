using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity )]
    public int ID { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public bool IsBorrowed { get; set; }
    public DateTime PublishDate { get; set; }
    public int Year { get; set; }
    public virtual ICollection<Loan>? Loans { get; set; } = new List<Loan>();
}
