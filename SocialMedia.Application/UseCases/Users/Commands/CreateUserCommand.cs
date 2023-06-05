using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Users.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Users.Commands;

public  class CreateUserCommand : IRequest<UserDto>
{
    
    public string UserName { get; set; }

    public int Age { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    IMapper _mapper;
    IApplicationDbContext _dbContext;

    public CreateUserCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async  Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Age = request.Age,
        };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserDto>(user);

    }
}