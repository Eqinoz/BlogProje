using System.Net.NetworkInformation;

namespace ProjectGSO.Models.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AddeDateTime { get; set; }
        public string Tags { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int ViewCount { get; set; }
        public bool Status { get; set; }
    }
}
