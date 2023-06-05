using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Users.Models;
using SocialMedia.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.UseCases.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public Guid Id  { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        IMapper _mapper;
        IApplicationDbContext _dbContext;

        public UpdateUserCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User()
            {
                UserName = request.UserName,
                Age = request.Age,
            };

             _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }


}
