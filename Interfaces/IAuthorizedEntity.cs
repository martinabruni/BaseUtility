namespace BaseUtility
{
    public interface IAuthorizedEntity<TOwnerKey>
        where TOwnerKey : notnull
    {
        TOwnerKey UserId { get; set; }
    }
}
