using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.Models;
using SocialMedia.Application.UseCases.Users.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Users.Queries
{
    public record GetAllUsersQuery : IRequest<PaginatedList<UserDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserDto>>
    {

        IMapper _mapper;
        IApplicationDbContext _dbContext;

        public GetAllUsersQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            User[] users = await _dbContext.Users.ToArrayAsync();

            List<UserDto> dtos = _mapper.Map<UserDto[]>(users).ToList();

            PaginatedList<UserDto> paginatedList =
                 PaginatedList<UserDto>.CreateAsync(
                    dtos, request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
