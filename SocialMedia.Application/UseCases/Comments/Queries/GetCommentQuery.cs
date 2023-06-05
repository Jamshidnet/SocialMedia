using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Comments.Queries
{
    public  record GetCommentQuery(Guid Id) : IRequest<CommentDto>;


    public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentDto>
    {
        IMapper _mapper;
        IApplicationDbContext _dbContext;

        public GetCommentQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<CommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {

            Comment? comment = await _dbContext.Comments.FindAsync(request.Id);

            if (comment == null)
            {
                throw new Exception(" there is on commmit with this id . ");
            }

            return _mapper.Map<CommentDto>(comment);
        }
    }
}
