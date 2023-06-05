using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Comments.Commands;

public  class UpdateCommentCommand  : IRequest<CommentDto>
{
    public Guid Id { get; set; }

    public string Text { get; set; }
}
public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
    IMapper _mapper;
    IApplicationDbContext _dbContext;

    public UpdateCommentCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async  Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
     
        Comment? comment =  await _dbContext.Comments.FindAsync(request.Id);
        
        if(comment is null)
        {
            throw new Exception(" there is no commment with this id. ");
        }    

        comment.Text = request.Text;
        _dbContext.Comments.Update(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CommentDto>(comment);
    }
}
