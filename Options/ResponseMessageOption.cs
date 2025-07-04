namespace BaseUtility
{
    public class ResponseMessageOption
    {
        public string InvalidRequest { get; set; } = "Invalid request.";
        public string EntityCreatedSuccessfully { get; set; } = "Entity created successfully.";
        public string ErrorCreatingEntity { get; set; } = "Error creating entity";
        public string EntityNotFound { get; set; } = "Entity not found.";
        public string EntityDeletedSuccessfully { get; set; } = "Entity deleted successfully.";
        public string ErrorDeletingEntity { get; set; } = "Error deleting entity";
        public string ErrorFindingEntities { get; set; } = "Error finding entities";
        public string EntitiesRetrievedSuccessfully { get; set; } = "Entities retrieved successfully.";
        public string ErrorRetrievingEntities { get; set; } = "Error retrieving entities";
        public string EntityRetrievedSuccessfully { get; set; } = "Entity retrieved successfully.";
        public string ErrorRetrievingEntity { get; set; } = "Error retrieving entity";
        public string EntityUpdatedSuccessfully { get; set; } = "Entity updated successfully.";
        public string ErrorUpdatingEntity { get; set; } = "Error updating entity";
    }
}
