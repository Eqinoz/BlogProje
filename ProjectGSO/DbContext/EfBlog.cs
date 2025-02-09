using Microsoft.Identity.Client;
using ProjectGSO.Models.DTO;

namespace ProjectGSO.DbContext
{
    public class EfBlog
    {
        public List<BlogDTO> GetDetailBlog()
        {
            using (BlogDbContext context = new BlogDbContext())
            {
                var result = from b in context.Blogs
                    join u in context.Users on b.AuthorId equals u.Id
                    select new BlogDTO
                    {
                        Id = b.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AvatarUrl = u.AvatarUrl,
                        Title = b.Title,
                        Description = b.Description,
                        Text = b.Text,
                        ImageUrl = b.ImageUrl,
                        AddeDateTime = b.AddeDateTime,
                        Tags = b.Tags,
                        LikeCount = b.LikeCount,
                        CommentCount = b.CommentCount,
                        ViewCount = b.ViewCount,
                        Status = b.Status

                    };
                return result.ToList();
            }
        }
    }
}
