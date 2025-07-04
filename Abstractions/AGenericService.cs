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

        public AGenericService(IRepository<TDatabase, TKey> repository, IMapper<TDatabase, TDto> databaseToDtoMapper, IMapper<TDto, TDatabase> dtoToDatabaseMapper)
        {
            _repository = repository;
            _databaseToDtoMapper = databaseToDtoMapper;
            _dtoToDatabaseMapper = dtoToDatabaseMapper;
        }

        public virtual async Task<BusinessResponse<TDto>> CreateAsync(TDto dto)
        {
            if (dto is null)
            {
                return BusinessResponse<TDto>.EntityCannotBeNull();
            }

            var repositoryRes = await _repository.CreateAsync(_dtoToDatabaseMapper.Map(dto));
            var businessRes = repositoryRes.ToBusinessResponse<TDto, TDatabase>();

            if (repositoryRes.Data is null)
            {
                return businessRes;
            }
            
            businessRes.Data = _databaseToDtoMapper.Map(repositoryRes.Data);
            return businessRes;
        }

        public virtual async Task<BusinessResponse<TDto>> DeleteAsync(TKey id)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.IdCannotBeNull();
            }
            
            var repositoryRes = await _repository.DeleteAsync(id);
            var businessRes = repositoryRes.ToBusinessResponse<TDto, TDatabase>();

            if (repositoryRes.Data is null)
            {
                return businessRes;
            }
            
            businessRes.Data = _databaseToDtoMapper.Map(repositoryRes.Data);
            return businessRes;
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> FindAsync(Expression<Func<TDatabase, bool>> predicate)
        {
            if (predicate is null)
            {
                return BusinessResponse<IEnumerable<TDto>>.PredicateCannotBeNull();
            }
            
            var repositoryRes = await _repository.FindAsync(predicate);
            var businessRes = repositoryRes.ToBusinessResponse<IEnumerable<TDto>, IEnumerable<TDatabase>>();

            if (repositoryRes.Data is null)
            {
                return businessRes;
            }

            //TODO: Centralize this logic in a common method
            if (repositoryRes.Data.ToList().Count == 0)
            {
                businessRes.Data = [];
                return businessRes;
            }

            businessRes.Data = repositoryRes.Data.Select(_databaseToDtoMapper.Map);
            return businessRes;
        }

        public virtual async Task<BusinessResponse<IEnumerable<TDto>>> GetAllAsync()
        {
            var repositoryRes = await _repository.GetAllAsync();
            var businessRes = repositoryRes.ToBusinessResponse<IEnumerable<TDto>, IEnumerable<TDatabase>>();

            if (repositoryRes.Data is null)
            {
                return businessRes;
            }

            //TODO: Centralize this logic in a common method
            if (repositoryRes.Data.ToList().Count == 0)
            {
                businessRes.Data = [];
                return businessRes;
            }
            
            businessRes.Data = repositoryRes.Data.Select(_databaseToDtoMapper.Map);
            return businessRes;
        }

        public virtual async Task<BusinessResponse<TDto>> GetByIdAsync(TKey id)
        {
            if (id is null)
            {
                return BusinessResponse<TDto>.IdCannotBeNull();
            }
            
            var repositoryRes = await _repository.GetByIdAsync(id);
            var businessRes = repositoryRes.ToBusinessResponse<TDto, TDatabase>();

            if (repositoryRes.Data is null)
            {
                return businessRes;
            }
            
            businessRes.Data = _databaseToDtoMapper.Map(repositoryRes.Data);
            return businessRes;
        }

        public virtual async Task<BusinessResponse<TDto>> UpdateAsync(TDto dto)
        {
            if (dto is null)
            {
                return BusinessResponse<TDto>.EntityCannotBeNull();
            }
            
            var repositoryRes = await _repository.UpdateAsync(_dtoToDatabaseMapper.Map(dto));
            var businessRes = repositoryRes.ToBusinessResponse<TDto, TDatabase>();

            if (repositoryRes.Data is null)
            {
                return businessRes;
            }
            
            businessRes.Data = _databaseToDtoMapper.Map(repositoryRes.Data);
            return businessRes;
        }
    }
}
