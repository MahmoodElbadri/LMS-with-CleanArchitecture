using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.DTOs.RequestDTOs;

public class LoanAddRequest
{
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool IsReturned { get; set; }
    public int BookID { get; set; }
    public int UserID { get; set; }
}
