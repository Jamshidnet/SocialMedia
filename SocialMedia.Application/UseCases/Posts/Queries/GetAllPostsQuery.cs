using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.Models;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Posts.Queries;

public  record GetAllPostsQuery : IRequest<PaginatedList<PostDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PaginatedList<PostDto>>
{

    IMapper _mapper;
    IApplicationDbContext _dbContext;

    public GetAllPostsQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PaginatedList<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {

        Post[] orders = await _dbContext.Posts.Include(x=>x.Comments).ToArrayAsync();

        List<PostDto> dtos = _mapper.Map<PostDto[]>(orders).ToList();

        PaginatedList<PostDto> paginatedList =
             PaginatedList<PostDto>.CreateAsync(
                dtos, request.PageNumber, request.PageSize);

        return paginatedList;
    }
}

