using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public interface ITagService
    {
        public Task AddTagToPostAsync(int tagId, int postId);

        public Task RemoveTagFromPostAsync(int tagId, int postId);

        public Task<bool> IsTagInPostAsync(int tagId, int postId);
    }
}
