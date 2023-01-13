using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Courses;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Mutations
{
	public class Mutation
	{
		private readonly CoursesRepository _coursesRepository;

		public Mutation(CoursesRepository coursesRepository)
		{
			_coursesRepository = coursesRepository;
		}

		public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
		{
			CourseDto courseDto = new()
			{
				Name = courseInput.Name,
				Subject = courseInput.Subject,
				InstructorId = courseInput.InstructorId
			};

			courseDto = await _coursesRepository.Create(courseDto);

			CourseResult course = new()
			{
				Id = courseDto.Id,
				InstructorId = courseDto.InstructorId,
				Subject = courseDto.Subject,
				Name = courseDto.Name
			};

			await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

			return course;
		}

		public async Task<CourseResult> UpdateCourse(Guid courseId, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
		{
			CourseDto courseDto = new()
			{
				Id = courseId,
				Name = courseInput.Name,
				Subject = courseInput.Subject,
				InstructorId = courseInput.InstructorId
			};

			courseDto = await _coursesRepository.Update(courseDto);

			CourseResult course = new()
			{
				Id = courseDto.Id,
				InstructorId = courseDto.InstructorId,
				Subject = courseDto.Subject,
				Name = courseDto.Name
			};

			string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
			await topicEventSender.SendAsync(updateCourseTopic, course);

			return course;
		}

		public async Task<bool> DeleteCourse(Guid id)
		{
			try
			{
				return await _coursesRepository.Delete(id);
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
