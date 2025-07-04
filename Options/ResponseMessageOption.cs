namespace BaseUtility
{
    public class ResponseMessageOption(string? entityName = "Entity")
    {
        public string InvalidRequest { get; set; } = "Invalid request.";
        public string EntityCreatedSuccessfully { get; set; } = $"{entityName} created successfully.";
        public string ErrorCreatingEntity { get; set; } = $"Error creating {entityName}";
        public string EntityNotFound { get; set; } = $"{entityName} not found.";
        public string EntityDeletedSuccessfully { get; set; } = $"{entityName} deleted successfully.";
        public string ErrorDeletingEntity { get; set; } = $"Error deleting {entityName}";
        public string EntitiesRetrievedSuccessfully { get; set; } = "Entities retrieved successfully.";
        public string ErrorRetrievingEntities { get; set; } = "Error retrieving entities";
        public string EntityRetrievedSuccessfully { get; set; } = $"{entityName} retrieved successfully.";
        public string ErrorRetrievingEntity { get; set; } = $"Error retrieving {entityName}";
        public string EntityUpdatedSuccessfully { get; set; } = $"{entityName} updated successfully.";
        public string ErrorUpdatingEntity { get; set; } = $"Error updating {entityName}";
    }
}
