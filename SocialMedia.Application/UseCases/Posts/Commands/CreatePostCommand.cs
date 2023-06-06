using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Application.UseCases.Posts.Notification;
using SocialMedia.Application.UseCases.Users.Notification;
using SocialMedia.Domein.Entities;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using User = SocialMedia.Domein.Entities.User;

namespace SocialMedia.Application.UseCases.Posts.Commands
{
    public  class CreatePostCommand : IRequest<PostDto>
    {
        public string Text { get; set; }

        public Guid UserId { get; set; }

    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        IMapper _mapper;
        IApplicationDbContext _dbContext;
        IMediator _mediator;

        public CreatePostCommandHandler(IMapper mapper, IApplicationDbContext dbContext, IMediator mediator)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            Post post = new Post()
            {
               
                Text = request.Text,
                UserId = request.UserId,
            };
            User? user = await _dbContext.Users.FindAsync(post.UserId);
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new PostCreatedNotification(post.Text,user.UserName));

            return _mapper.Map<PostDto>(post);
        }
    }
}
