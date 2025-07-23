using System.Linq.Expressions;

namespace BaseUtility
{
    public abstract class BaseService<TDto, TDatabase, TKey> : IService<TDto, TDatabase, TKey>
        where TDto : class, IEntity<TKey>
        where TDatabase : class, IEntity<TKey>
    {
        protected readonly IRepository<TDatabase, TKey> _repository;
        protected readonly IMapper<TDatabase, TDto> _databaseToDtoMapper;
        protected readonly IMapper<TDto, TDatabase> _dtoToDatabaseMapper;
        protected readonly ResponseMessage _messages;

        public BaseService(
            IRepository<TDatabase, TKey> repository,
            IMapper<TDatabase, TDto> databaseToDtoMapper,
            IMapper<TDto, TDatabase> dtoToDatabaseMapper,
            ResponseMessage messages)
        {
            _repository = repository;
            _databaseToDtoMapper = databaseToDtoMapper;
            _dtoToDatabaseMapper = dtoToDatabaseMapper;
            _messages = messages;
        }

        public virtual async Task<BusinessResponse<TDto>> CreateAsync(TDto dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.CreateAsync(_dtoToDatabaseMapper.Map(dto), cancellationToken);

            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<TDto>> DeleteAsync(TKey id, CancellationToken cancellationToken)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.DeleteAsync(id, cancellationToken);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> FindAsync(Expression<Func<TDatabase, bool>> predicate, CancellationToken cancellationToken)
        {
            if (predicate is null)
            {
                return BusinessResponse<IEnumerable<TDto>>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.FindAsync(predicate, cancellationToken);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var repositoryRes = await _repository.GetAllAsync(cancellationToken);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<TDto>> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.GetByIdAsync(id, cancellationToken);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }

        public virtual async Task<BusinessResponse<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.InvalidRequest);
            }

            var repositoryRes = await _repository.UpdateAsync(_dtoToDatabaseMapper.Map(dto), cancellationToken);
            return repositoryRes.ToBusinessResponse(_databaseToDtoMapper);
        }
    }
}
