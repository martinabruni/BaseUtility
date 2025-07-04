namespace BaseUtility
{
    public class BusinessResponse<TData>
        where TData : class
    {
        public TData? Data { get; set; }
        public string? Message { get; set; }
        public required BusinessCode StatusCode { get; set; }

        public static BusinessResponse<TData> EntityCannotBeNull() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.BadRequest,
            Message = "Entity cannot be null."
        };

        public static BusinessResponse<TData> EntityCreatedSuccessfully(TData entity) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Created,
            Data = entity,
            Message = "Entity created successfully."
        };

        public static BusinessResponse<TData> ErrorCreatingEntity() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.InternalServerError,
            Message = "Error creating entity"
        };

        public static BusinessResponse<TData> IdCannotBeNull() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.BadRequest,
            Message = "ID cannot be null."
        };

        public static BusinessResponse<TData> EntityNotFound() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.NotFound,
            Message = "Entity not found."
        };

        public static BusinessResponse<TData> EntityDeletedSuccessfully(TData entity) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Ok,
            Data = entity,
            Message = "Entity deleted successfully."
        };

        public static BusinessResponse<TData> ErrorDeletingEntity() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.InternalServerError,
            Message = "Error deleting entity"
        };

        public static BusinessResponse<TData> PredicateCannotBeNull() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.BadRequest,
            Message = "Predicate cannot be null."
        };

        public static BusinessResponse<TData> ErrorFindingEntities() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.InternalServerError,
            Message = "Error finding entities"
        };

        public static BusinessResponse<TData> EntitiesRetrievedSuccessfully(TData entities) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Ok,
            Data = entities,
            Message = "Entities retrieved successfully."
        };

        public static BusinessResponse<TData> ErrorRetrievingEntities() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.InternalServerError,
            Message = "Error retrieving entities"
        };

        public static BusinessResponse<TData> EntityRetrievedSuccessfully(TData entity) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Ok,
            Data = entity,
            Message = "Entity retrieved successfully."
        };

        public static BusinessResponse<TData> ErrorRetrievingEntity() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.InternalServerError,
            Message = "Error retrieving entity"
        };

        public static BusinessResponse<TData> EntityUpdatedSuccessfully(TData entity) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Ok,
            Data = entity,
            Message = "Entity updated successfully."
        };

        public static BusinessResponse<TData> ErrorUpdatingEntity() => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.InternalServerError,
            Message = "Error updating entity"
        };
    }
}
