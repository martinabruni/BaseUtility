using System.Linq.Expressions;

namespace BaseUtility
{
    public class AGenericService<TDto, TDatabase, TKey> : IService<TDto, TDatabase, TKey>
        where TDto : class, IEntity<TKey>
        where TDatabase : class, IEntity<TKey>
        where TKey : notnull
    {
        protected readonly IRepository<TDatabase, TKey> _repository;
        protected readonly IMapper<TDatabase, TDto> _databaseToDtoMapper;
        protected readonly IMapper<TDto, TDatabase> _dtoToDatabaseMapper;
        protected readonly ResponseMessageOption _messages;

        public AGenericService(
            IRepository<TDatabase, TKey> repository,
            IMapper<TDatabase, TDto> databaseToDtoMapper,
            IMapper<TDto, TDatabase> dtoToDatabaseMapper,
            ResponseMessageOption messages)
        {
            _repository = repository;
            _databaseToDtoMapper = databaseToDtoMapper;
            _dtoToDatabaseMapper = dtoToDatabaseMapper;
            _messages = messages;
        }

        public virtual async Task<BusinessResponse<TDto>> CreateAsync(TDto dto)
        {
            if (dto is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.CreateAsync(_dtoToDatabaseMapper.Map(dto));

            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<TDto>> DeleteAsync(TKey id)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.DeleteAsync(id);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> FindAsync(Expression<Func<TDatabase, bool>> predicate)
        {
            if (predicate is null)
            {
                return BusinessResponse<IEnumerable<TDto>>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.FindAsync(predicate);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> GetAllAsync()
        {
            var repositoryRes = await _repository.GetAllAsync();
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<TDto>> GetByIdAsync(TKey id)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.GetByIdAsync(id);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<TDto>> UpdateAsync(TDto dto)
        {
            if (dto is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.UpdateAsync(_dtoToDatabaseMapper.Map(dto));
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }
    }
}
