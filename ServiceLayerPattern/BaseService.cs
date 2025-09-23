//using System.Linq.Expressions;

//namespace BaseUtility
//{
//    public abstract class BaseService<TDomain, TEntity, TKey> : IService<TDomain, TEntity, TKey>
//        where TDomain : class, IEntity<TKey>
//        where TEntity : class, IEntity<TKey>
//    {
//        protected readonly IRepository<TEntity, TKey> _repository;
//        protected readonly IMapper<TEntity, TDomain> _entityToDomainMapper;
//        protected readonly IMapper<TDomain, TEntity> _domainModelToEntityMapper;
//        protected readonly ResponseMessage _messages;

//        public BaseService(
//            IRepository<TEntity, TKey> repository,
//            IMapper<TEntity, TDomain> entityToDomainMapper,
//            IMapper<TDomain, TEntity> domainModelToEntityMapper,
//            ResponseMessage messages)
//        {
//            _repository = repository;
//            _entityToDomainMapper = entityToDomainMapper;
//            _domainModelToEntityMapper = domainModelToEntityMapper;
//            _messages = messages;
//        }

//        public virtual async Task<BusinessResponse<TDomain>> CreateAsync(TDomain domainModel, CancellationToken cancellationToken)
//        {
//            if (domainModel is null)
//            {
//                return BusinessResponse<TDomain>.BadRequest(_messages.InvalidRequest);
//            }

//            var repositoryRes = await _repository.CreateAsync(_domainModelToEntityMapper.Map(domainModel), cancellationToken);

//            return repositoryRes.ToBusinessResponse(_entityToDomainMapper);
//        }

//        public virtual async Task<BusinessResponse<TDomain>> DeleteAsync(TKey id, CancellationToken cancellationToken)
//        {
//            if (id is null)
//            {
//                return BusinessResponse<TDomain>.BadRequest(_messages.InvalidRequest);
//            }

//            var repositoryRes = await _repository.DeleteAsync(id, cancellationToken);
//            return repositoryRes.ToBusinessResponse(_entityToDomainMapper);
//        }

//        public virtual async Task<BusinessResponse<IEnumerable<TDomain>>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
//        {
//            if (predicate is null)
//            {
//                return BusinessResponse<IEnumerable<TDomain>>.BadRequest(_messages.InvalidRequest);
//            }

//            var repositoryRes = await _repository.FindAsync(predicate, cancellationToken);
//            return repositoryRes.ToBusinessResponse(_entityToDomainMapper);
//        }

//        public virtual async Task<BusinessResponse<IEnumerable<TDomain>>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            var repositoryRes = await _repository.GetAllAsync(cancellationToken);
//            return repositoryRes.ToBusinessResponse(_entityToDomainMapper);
//        }

//        public virtual async Task<BusinessResponse<TDomain>> GetByIdAsync(TKey id, CancellationToken cancellationToken)
//        {
//            if (id is null)
//            {
//                return BusinessResponse<TDomain>.BadRequest(_messages.InvalidRequest);
//            }

//            var repositoryRes = await _repository.GetByIdAsync(id, cancellationToken);
//            return repositoryRes.ToBusinessResponse(_entityToDomainMapper);
//        }

//        public virtual async Task<BusinessResponse<TDomain>> UpdateAsync(TDomain domainModel, CancellationToken cancellationToken)
//        {
//            if (domainModel is null)
//            {
//                return BusinessResponse<TDomain>.BadRequest(_messages.InvalidRequest);
//            }

//            var repositoryRes = await _repository.UpdateAsync(_domainModelToEntityMapper.Map(domainModel), cancellationToken);
//            return repositoryRes.ToBusinessResponse(_entityToDomainMapper);
//        }
//    }
//}
