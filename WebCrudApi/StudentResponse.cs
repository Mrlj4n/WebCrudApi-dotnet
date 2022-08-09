namespace WebCrudApi
{
    public class StudentResponse
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

    }
}
