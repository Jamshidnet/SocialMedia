using AutoMapper;
using MediatR;
using SocialMedia.Application.Abstraction;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Posts.Queries;

public  record GetPostQuery(Guid Id) : IRequest<PostDto>;
public class GetAllPostQueryHandle : IRequestHandler<GetPostQuery,PostDto> 
{
    IMapper _mapper;
    IApplicationDbContext _dbContext;
     
    public GetAllPostQueryHandle(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public  async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        Post? post = await _dbContext.Posts.FindAsync(request.Id);
        if(post is null)
        {
            throw new Exception("there is no user with this Id. ");
        }

        return _mapper.Map<PostDto>(post);  
    }
}
