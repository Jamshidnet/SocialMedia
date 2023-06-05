using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public  record DeleteUserCommand(Guid Id) : IRequest<UserDto>;
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
    {

        IMapper _mapper;
        IApplicationDbContext _dbContext;

        public DeleteUserCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
           User?  user= await  _dbContext.Users.FirstOrDefaultAsync(x=>x.Id==request.Id);
            if (user is null)
                throw  new Exception(" there is no user with this Id . ");
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}
