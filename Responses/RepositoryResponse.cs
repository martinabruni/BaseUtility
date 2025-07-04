namespace BaseUtility
{
    public class RepositoryResponse<TData>
    {
        public TData? Data { get; set; }
        public string? Message { get; set; }
        public required RepositoryCode StatusCode { get; set; }

        public static RepositoryResponse<TData> EntityCannotBeNull() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.BadRequest,
            Message = "Entity cannot be null."
        };

        public static RepositoryResponse<TData> EntityCreatedSuccessfully(TData entity) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Created,
            Data = entity,
            Message = "Entity created successfully."
        };

        public static RepositoryResponse<TData> ErrorCreatingEntity() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.InternalServerError,
            Message = "Error creating entity"
        };

        public static RepositoryResponse<TData> IdCannotBeNull() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.BadRequest,
            Message = "ID cannot be null."
        };

        public static RepositoryResponse<TData> EntityNotFound() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.NotFound,
            Message = "Entity not found."
        };

        public static RepositoryResponse<TData> EntityDeletedSuccessfully(TData entity) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Ok,
            Data = entity,
            Message = "Entity deleted successfully."
        };

        public static RepositoryResponse<TData> ErrorDeletingEntity() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.InternalServerError,
            Message = "Error deleting entity"
        };

        public static RepositoryResponse<TData> PredicateCannotBeNull() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.BadRequest,
            Message = "Predicate cannot be null."
        };

        public static RepositoryResponse<TData> ErrorFindingEntities() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.InternalServerError,
            Message = "Error finding entities"
        };

        public static RepositoryResponse<TData> EntitiesRetrievedSuccessfully(TData entities) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Ok,
            Data = entities,
            Message = "Entities retrieved successfully."
        };

        public static RepositoryResponse<TData> ErrorRetrievingEntities() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.InternalServerError,
            Message = "Error retrieving entities"
        };

        public static RepositoryResponse<TData> EntityRetrievedSuccessfully(TData entity) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Ok,
            Data = entity,
            Message = "Entity retrieved successfully."
        };

        public static RepositoryResponse<TData> ErrorRetrievingEntity() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.InternalServerError,
            Message = "Error retrieving entity"
        };

        public static RepositoryResponse<TData> EntityUpdatedSuccessfully(TData entity) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Ok,
            Data = entity,
            Message = "Entity updated successfully."
        };

        public static RepositoryResponse<TData> ErrorUpdatingEntity() => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.InternalServerError,
            Message = "Error updating entity"
        };
    }
}
