using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Post.Query.domain.Entities;
using Post.Query.domain.Repositories;
using Post.Query.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory;
        public PostRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(PostEntity post)
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            contex.Posts.Add(post);
            await contex.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid postId)
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            var post = await GetByIdAsync(postId);

            if (post == null)
            {
                return;
            }
            contex.Posts.Remove(post);
            await contex.SaveChangesAsync();
        }

        public async Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            return await contex.Posts
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.PostId == postId);
        }

        public async Task<List<PostEntity>> ListAllAsync()
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            return await contex.Posts
                .AsNoTracking()
                .Include(x => x.Comments)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            return await contex.Posts
                .AsNoTracking()
                .Include(x => x.Comments)
                .AsNoTracking()
                .Where(x => x.Author.Contains(author, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithCommentsAsync()
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            return await contex.Posts
                .AsNoTracking()
                .Include(x => x.Comments)
                .AsNoTracking()
                .Where(x => x.Comments != null && x.Comments.Count != 0)
                .ToListAsync();
        }

        public async Task<List<PostEntity>> ListwithLikesAsync(int numberOfLikes)
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            return await contex.Posts
                .AsNoTracking()
                .Include(x => x.Comments)
                .AsNoTracking()
                .Where(x => x.Likes >= numberOfLikes)
                .ToListAsync();
        }

        public async Task UpdateAsync(PostEntity post)
        {
            using DatabaseContext contex = _contextFactory.CreateDbContext();
            contex.Posts.Update(post);

            await contex.SaveChangesAsync();
        }
    }
}