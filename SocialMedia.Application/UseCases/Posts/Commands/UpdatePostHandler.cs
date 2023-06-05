using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Posts.Commands
{
    public class UpdatePostCommmand : IRequest<PostDto>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public Guid UserId { get; set; }
    }

    public class UpdatePostCommmandHandler : IRequestHandler<UpdatePostCommmand, PostDto>
    {

        IMapper _mapper;
        IApplicationDbContext _dbContext;

        public UpdatePostCommmandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PostDto> Handle(UpdatePostCommmand request, CancellationToken cancellationToken)
        {
            Post? post = new Post()
            {
                Id = request.Id,
                Text = request.Text,
                UserId = request.UserId
            };
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PostDto>(post);
        }
    }
}
