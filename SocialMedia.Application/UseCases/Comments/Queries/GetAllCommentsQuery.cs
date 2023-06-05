using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.Models;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Comments.Queries;

public  record GetAllCommentsQuery : IRequest<PaginatedList<CommentDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, PaginatedList<CommentDto>>
{

    IMapper _mapper;
    IApplicationDbContext _dbContext;

    public GetAllCommentsQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PaginatedList<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {

        Comment[] comment = await _dbContext.Comments.ToArrayAsync();

        List<CommentDto> dtos = _mapper.Map<CommentDto[]>(comment).ToList();

        PaginatedList<CommentDto> paginatedList =
             PaginatedList<CommentDto>.CreateAsync(
                dtos, request.PageNumber, request.PageSize);

        return paginatedList;

    }
}

