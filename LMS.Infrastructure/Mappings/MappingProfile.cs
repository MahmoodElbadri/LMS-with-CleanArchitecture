using AutoMapper;
using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.DTOs.ResponseDTOs;
using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Mappings;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserAddRequest>()
            .ReverseMap();
        CreateMap<Loan, LoanAddRequest>()
            .ReverseMap();
        CreateMap<Book, BookAddRequest>()
            .ReverseMap();
        CreateMap<UserResponse, User>()
            .ReverseMap();
        CreateMap<LoanResponse, Loan>()
            .ReverseMap();
        CreateMap<BookResponse, Book>()
            .ReverseMap();

        //CreateMap<List<Book>, List<BookResponse>>().ReverseMap();

    }
}
