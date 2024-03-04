using Post.Query.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Query.domain.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(PostEntity post);
        Task UpdateAsync(PostEntity post);
        Task DeleteAsync(Guid post);
        Task<PostEntity> GetByIdAsync(Guid postId);
        Task<List<PostEntity>> ListAllAsync();
        Task<List<PostEntity>> ListByAutherAsync(string author);
        Task<List<PostEntity>> ListwithLikesAsync(int numberOfLikes);
        Task<List<PostEntity>> ListWithCommentsAsync();
    }
}
