using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class CommentProvider : baseProvider
    {
        public async Task<List<Comment>> GetAllAsync()
        {
            try
            {
                var data = await db.Comment.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Comment>();
            }
        }

        public async Task<Comment> AddNewCommentAsync(Comment newComment)
        {
            try
            {
                newComment.Id = Guid.NewGuid().ToString();
                newComment.CreatedAt = DateTime.Now;
                db.Comment.Add(newComment);
                await db.SaveChangesAsync();
                return newComment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Comment> UpdateCommentAsync(string id, Comment modifiedComment)
        {
            try
            {
                Comment existingComment = await db.Comment.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingComment != null)
                {
                    existingComment.Text = modifiedComment.Text;
                    existingComment.Rating = modifiedComment.Rating;
                    existingComment.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingComment;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteCommentAsync(string id)
        {
            try
            {
                Comment existingComment = await db.Comment.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingComment != null)
                {
                    db.Comment.Remove(existingComment);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
