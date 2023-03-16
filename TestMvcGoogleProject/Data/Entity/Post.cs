using TestMvcGoogleProject.Models;

namespace TestMvcGoogleProject.Data.Entity
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public Post()
        {}

        public Post(CreatePostViewModel createPostViewModel)
        {
            Title = createPostViewModel.Title;
            Text = createPostViewModel.Text;
        }
        public Post(PostViewModel postViewModel)
        {
            Id = postViewModel.Id ?? 0;
            Title = postViewModel.Title;
            Text = postViewModel.Text;
            Date = postViewModel.Date;
        } 
        
    }
}

