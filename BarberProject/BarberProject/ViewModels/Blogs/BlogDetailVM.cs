namespace BarberProject.ViewModels.Blogs
{
    public class BlogDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Service { get; set; }
        public DateTime CreateDate { get; set; }
        public List<BlogImageVM> BlogImages { get; set; }
    }
}
