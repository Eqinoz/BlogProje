namespace ProjectGSO.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AddeDateTime { get; set; }
        public string Tags { get; set; }
        public int LikeCount { get; set; }= 0;
        public int CommentCount { get; set; }=0;
        public int ViewCount { get; set; }= 0;
        public bool Status { get; set; }
    }
}
