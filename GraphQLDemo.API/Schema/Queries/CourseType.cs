using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Models;
using GraphQLDemo.API.Services.Instructors;

namespace GraphQLDemo.API.Schema.Queries
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Subject Subject { get; set; }

        //If we don't want this to be included in the schema we can use [GraphQLIgnore]
        public Guid InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            InstructorDto instructorDto = await instructorDataLoader.LoadAsync(InstructorId);

            return new InstructorType()
            {
                Id = instructorDto.Id,
                FirstName= instructorDto.FirstName,
                LastName= instructorDto.LastName,
                Salary = instructorDto.Salary
            };
        }
        public IEnumerable<StudentType>? Students { get; set; }
    }
}
