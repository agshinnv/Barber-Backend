namespace BarberProject.ViewModels.Blogs
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Service { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
