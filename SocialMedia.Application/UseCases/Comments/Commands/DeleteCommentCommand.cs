using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Comments.Commands;

public  record DeleteCommentCommand(Guid Id) : IRequest<CommentDto>;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, CommentDto>
{

    IMapper _mapper;
    IApplicationDbContext _dbContext;

    public DeleteCommentCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<CommentDto> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        Comment? comment = await _dbContext.Comments.FindAsync(request.Id);

        if (comment is null)
        {
            throw new Exception(" there is no comment with this Id . ");
        }

        _dbContext.Comments.Remove(comment);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CommentDto>(comment);
    }
}
