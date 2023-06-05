using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Domein.Entities;

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

        public CreatePostCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            Post post = new Post()
            {
               
                Text = request.Text,
                UserId = request.UserId,
            };

            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PostDto>(post);
        }
    }
}
