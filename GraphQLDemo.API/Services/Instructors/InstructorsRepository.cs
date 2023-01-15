using GraphQLDemo.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Instructors
{
	public class InstructorsRepository
	{
		private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

		public InstructorsRepository(IDbContextFactory<SchoolDbContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<InstructorDto> GetById(Guid instructorId)
		{
			using (SchoolDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Instructors.FirstOrDefaultAsync(c => c.Id == instructorId);
			}
		}

		public async Task<IEnumerable<InstructorDto>> GetManyById(IReadOnlyList<Guid> instructorIds)
		{
			using (SchoolDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Instructors.Where(i => instructorIds.Contains(i.Id)).ToListAsync();
			}
		}
	}
}
