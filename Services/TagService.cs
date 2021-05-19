using BlazorServer.Data;
using BlazorServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomUser> _userManager;

        public TagService(ApplicationDbContext context,
            UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task AddTagToPostAsync(int tagId, int postId)
        {
            try
            {
                if (!await IsTagInPostAsync(tagId, postId))
                {
                    Tag tag = await _context.Tag.FindAsync(tagId);
                    PostCategory post = await _context.PostCategory.FindAsync(postId);
                    try
                    {
                        post.Tags.Add(tag);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error Adding user to project - message:{ex.Message}");
            }
        }

        public async Task<bool> IsTagInPostAsync(int tagId, int postId)
        {
            var post = await _context.PostCategory.Include(p => p.Tags).FirstAsync(c => c.Id == postId);
            var tag = post.Tags.Any(u => u.Id == tagId);
            return tag;
        }

        public async Task RemoveTagFromPostAsync(int tagId, int postId)
        {
            try
            {
                if (await IsTagInPostAsync(tagId, postId))
                {
                    Tag tag = await _context.Tag.FindAsync(tagId);
                    PostCategory post = await _context.PostCategory.FindAsync(postId);
                    try
                    {
                        post.Tags.Remove(tag);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error Remove user to project - message:{ex.Message}");
            }
        }
    }
}
