using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Services.Courses;

namespace GraphQLDemo.API.Schema.Queries
{
	public class Query
	{
		//This is how bogus is used
		//private readonly Faker<InstructorType> _instructorFaker;
		//private readonly Faker<StudentType> _studentFaker;
		//private readonly Faker<CourseType> _courseFaker;

		//public Query()
		//{
		//    _instructorFaker = new Faker<InstructorType>()
		//        .RuleFor(c => c.Id, f => Guid.NewGuid())
		//        .RuleFor(c => c.FirstName, f => f.Name.FirstName())
		//        .RuleFor(c => c.LastName, f => f.Name.LastName())
		//        .RuleFor(c => c.Salary, f => f.Random.Double(30, 100));

		//    _studentFaker = new Faker<StudentType>()
		//        .RuleFor(c => c.Id, f => Guid.NewGuid())
		//        .RuleFor(c => c.FirstName, f => f.Name.FirstName())
		//        .RuleFor(c => c.LastName, f => f.Name.LastName())
		//        .RuleFor(c => c.GPA, f => f.Random.Double(1, 4));

		//    _courseFaker = new Faker<CourseType>()
		//        .RuleFor(c => c.Id, f => Guid.NewGuid())
		//        .RuleFor(c => c.Name, f => f.Name.JobTitle())
		//        .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
		//        .RuleFor(c => c.Instructor, f => _instructorFaker.Generate())
		//        .RuleFor(c => c.Students, f => _studentFaker.Generate(5));
		//}

		//public IEnumerable<CourseType> GetCourses()
		//    => _courseFaker.Generate(5);

		//public async Task<CourseType> GetCourseById(Guid id)
		//{
		//    await Task.Delay(1000);

		//    var course = _courseFaker.Generate();

		//    course.Id = id;

		//    return course;
		//}


		private readonly CoursesRepository _coursesRepository;

		public Query(CoursesRepository coursesRepository)
		{
			_coursesRepository = coursesRepository;
		}

		public async Task<IEnumerable<CourseType>> GetCourses()
		{
			IEnumerable<CourseDto> CourseDtos = await _coursesRepository.GetAll();

			return CourseDtos.Select(courseDto => new CourseType()
			{
				Id = courseDto.Id,
				Name = courseDto.Name,
				Subject = courseDto.Subject,
				Instructor = new InstructorType()
				{
					Id = courseDto.Instructor.Id,
					FirstName = courseDto.Instructor.FirstName,
					LastName = courseDto.Instructor.LastName,
					Salary = courseDto.Instructor.Salary
				}
			});
		}

		public async Task<CourseType> GetCourseById(Guid id)
		{
			CourseDto courseDto = await _coursesRepository.GetById(id);

			return new CourseType()
			{
				Id = courseDto.Id,
				Name = courseDto.Name,
				Subject = courseDto.Subject,
				Instructor = new InstructorType()
				{
					Id = courseDto.Instructor.Id,
					FirstName = courseDto.Instructor.FirstName,
					LastName = courseDto.Instructor.LastName,
					Salary = courseDto.Instructor.Salary
				}
			};
		}

		//This is how we declare a query as deprecated
		//[GraphQLDeprecated("This query is deprecated.")]
		//public string Instructions => "TestInstructions";
	}
}
