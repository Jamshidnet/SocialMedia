using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Users.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Users.Queries;

public record GetUserQuery(Guid Id) : IRequest<UserDto>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    IMapper _mapper;
    IApplicationDbContext _dbContext;

    public GetUserQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {

         User? user= await _dbContext.Users.FindAsync(request.Id);

        if (user is null)
        {
            throw new Exception(" there is no User with this Id. ");
        }

        return _mapper.Map<UserDto>(user);
    }

}

