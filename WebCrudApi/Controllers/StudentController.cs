using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
                new Student{StudentId=1,
                    Name="Name1", LastName="LastName1",
                    Place="Place1"},
                new Student{StudentId=2,Name="Name2", LastName="LastName2",Place="Place2" }
        };

        private readonly DataContext dataContext;
        public StudentController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return Ok(await dataContext.Students.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Student>>> Get(int id)
        {
            var student = await dataContext.Students.FindAsync(id);
            if (student == null) BadRequest("Student not found.");


            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(Student student)
        {
            dataContext.Students.Add(student);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Students.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student student)
        {
            var student1 = await dataContext.Students.FindAsync(student.StudentId);
            if (student1 == null) BadRequest("Student not found.");

            student1.Name = student.Name;
            student1.LastName = student.LastName;
            student1.Place = student.Place;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Students.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            var student1 = await dataContext.Students.FindAsync(id);

            if (student1 == null) BadRequest("Student not found.");

            dataContext.Students.Remove(student1);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Students.ToListAsync());
        }
    }
}
