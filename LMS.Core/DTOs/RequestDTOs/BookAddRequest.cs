using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.DTOs.RequestDTOs;

public class BookAddRequest
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public bool IsBorrowed { get; set; }
    public DateTime PublishDate { get; set; }
}
