using MediatR;
using AutoMapper;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Comments.Commands;

public class CreateCommentCommand : IRequest<CommentDto>
{
    public string Text { get; set; }
    public Guid? PostId { get; set; }
    public Guid? CommentId { get; set; }
    public Guid UserId { get; set; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    IMapper _mapper;
    IApplicationDbContext _dbContext;

    public CreateCommentCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {

        Comment comment = new Comment()
        {
            Text = request.Text,
            PostId = request.PostId,
            BaseCommentId = request.CommentId,
            UserId=request.UserId
        };

        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CommentDto>(comment);
    }
}
