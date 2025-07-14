namespace BaseUtility
{
    public class ResponseMessage
    {
        public string InvalidRequest { get; set; } = "Invalid request.";
        public string EntityCreatedSuccessfully { get; set; } = "Entity created successfully.";
        public string ErrorCreatingEntity { get; set; } = "Error creating Entity";
        public string EntityNotFound { get; set; } = "Entity not found.";
        public string EntityDeletedSuccessfully { get; set; } = "Entity deleted successfully.";
        public string ErrorDeletingEntity { get; set; } = "Error deleting Entity";
        public string EntitiesRetrievedSuccessfully { get; set; } = "Entities retrieved successfully.";
        public string ErrorRetrievingEntities { get; set; } = "Error retrieving entities";
        public string EntityRetrievedSuccessfully { get; set; } = "Entity retrieved successfully.";
        public string ErrorRetrievingEntity { get; set; } = "Error retrieving Entity";
        public string EntityUpdatedSuccessfully { get; set; } = "Entity updated successfully.";
        public string ErrorUpdatingEntity { get; set; } = "Error updating Entity";
        public string InvalidCredentials { get; set; } = "Invalid credentials provided.";
        public string NotImplemented { get; set; } = "This feature is not implemented yet.";
        public string NotLoggedIn { get; set; } = "Login to perform this action.";
        public string ErrorMapping { get; set; } = "Error mapping data.";
        public string EntityAlreadyExists { get; set; } = "Entity already exists.";
    }
}
