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
                return BusinessResponse<TDto>.BadRequest(_messages.EntityCannotBeNull);
            }

            var repositoryRes = await _repository.CreateAsync(_dtoToDatabaseMapper.Map(dto));
            if (repositoryRes.Data is null)
            {
                return BusinessResponse<TDto>.InternalServerError(_messages.ErrorCreatingEntity);
            }

            var mappedDto = _databaseToDtoMapper.Map(repositoryRes.Data);
            return BusinessResponse<TDto>.Created(_messages.EntityCreatedSuccessfully, mappedDto);
        }

        public virtual async Task<BusinessResponse<TDto>> DeleteAsync(TKey id)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.IdCannotBeNull);
            }

            var repositoryRes = await _repository.DeleteAsync(id);
            if (repositoryRes.Data is null)
            {
                return BusinessResponse<TDto>.InternalServerError(_messages.ErrorDeletingEntity);
            }

            var mappedDto = _databaseToDtoMapper.Map(repositoryRes.Data);
            return BusinessResponse<TDto>.Ok(_messages.EntityDeletedSuccessfully, mappedDto);
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> FindAsync(Expression<Func<TDatabase, bool>> predicate)
        {
            if (predicate is null)
            {
                return BusinessResponse<IEnumerable<TDto>>.BadRequest(_messages.PredicateCannotBeNull);
            }

            var repositoryRes = await _repository.FindAsync(predicate);
            if (repositoryRes.Data is null)
            {
                return BusinessResponse<IEnumerable<TDto>>.InternalServerError(_messages.ErrorFindingEntities);
            }

            var dtos = repositoryRes.Data.Select(_databaseToDtoMapper.Map).ToList();
            if (dtos.Count == 0)
            {
                return BusinessResponse<IEnumerable<TDto>>.Ok(_messages.EntitiesRetrievedSuccessfully, []);
            }

            return BusinessResponse<IEnumerable<TDto>>.Ok(_messages.EntitiesRetrievedSuccessfully, dtos);
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> GetAllAsync()
        {
            var repositoryRes = await _repository.GetAllAsync();
            if (repositoryRes.Data is null)
            {
                return BusinessResponse<IEnumerable<TDto>>.InternalServerError(_messages.ErrorRetrievingEntities);
            }

            var dtos = repositoryRes.Data.Select(_databaseToDtoMapper.Map).ToList();
            if (dtos.Count == 0)
            {
                return BusinessResponse<IEnumerable<TDto>>.Ok(_messages.EntitiesRetrievedSuccessfully, []);
            }

            return BusinessResponse<IEnumerable<TDto>>.Ok(_messages.EntitiesRetrievedSuccessfully, dtos);
        }

        public virtual async Task<BusinessResponse<TDto>> GetByIdAsync(TKey id)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.IdCannotBeNull);
            }

            var repositoryRes = await _repository.GetByIdAsync(id);
            if (repositoryRes.Data is null)
            {
                return BusinessResponse<TDto>.NotFound(_messages.EntityNotFound);
            }

            var mappedDto = _databaseToDtoMapper.Map(repositoryRes.Data);
            return BusinessResponse<TDto>.Ok(_messages.EntityRetrievedSuccessfully, mappedDto);
        }

        public virtual async Task<BusinessResponse<TDto>> UpdateAsync(TDto dto)
        {
            if (dto is null)
            {
                return BusinessResponse<TDto>.BadRequest(_messages.EntityCannotBeNull);
            }

            var repositoryRes = await _repository.UpdateAsync(_dtoToDatabaseMapper.Map(dto));
            if (repositoryRes.Data is null)
            {
                return BusinessResponse<TDto>.InternalServerError(_messages.ErrorUpdatingEntity);
            }

            var mappedDto = _databaseToDtoMapper.Map(repositoryRes.Data);
            return BusinessResponse<TDto>.Ok(_messages.EntityUpdatedSuccessfully, mappedDto);
        }
    }
}
