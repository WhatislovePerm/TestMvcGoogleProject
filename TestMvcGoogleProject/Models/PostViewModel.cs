namespace TestMvcGoogleProject.Models;

public class PostViewModel
{
    public int? Id { get; set; }
    public string Text { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    
    public string UserEmail { get; set; }
}