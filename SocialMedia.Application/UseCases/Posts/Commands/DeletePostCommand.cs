using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Posts.Commands
{
    public record DeletePostCommand(Guid Id) : IRequest<PostDto>;
    

    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, PostDto>
    {
        IMapper _mappper;
        IApplicationDbContext _dbContext;

        public DeletePostCommandHandler(IMapper mappper, IApplicationDbContext dbContext)
        {
            _mappper = mappper;
            _dbContext = dbContext;
        }

        public  async Task<PostDto> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            Post? post =  await _dbContext.Posts.FindAsync(request.Id);
            if (post is null)
            {
                throw new Exception(" there is no post with this post id ");
            }

            _dbContext.Posts.Remove(post);
           await  _dbContext.SaveChangesAsync(cancellationToken);
            return _mappper.Map<PostDto>(post);
        }
    }

}
