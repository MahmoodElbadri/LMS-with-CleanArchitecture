using LMS.Core.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.DTOs.ResponseDTOs;

public class LoanResponse:LoanAddRequest
{
    public int ID { get; set; }
}
