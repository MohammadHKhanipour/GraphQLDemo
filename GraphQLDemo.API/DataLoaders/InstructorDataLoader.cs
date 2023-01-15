using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Services.Instructors;

namespace GraphQLDemo.API.DataLoaders
{
	public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDto>
	{
		private readonly InstructorsRepository _instructorsRepository;

		public InstructorDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions? options = null, InstructorsRepository instructorsRepository = null) : base(batchScheduler, options)
		{
			_instructorsRepository = instructorsRepository;
		}

		protected override async Task<IReadOnlyDictionary<Guid, InstructorDto>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
		{
			IEnumerable<InstructorDto> instructors = await _instructorsRepository.GetManyById(keys);

			return instructors.ToDictionary(i => i.Id);
		}
	}
}
