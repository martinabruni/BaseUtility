namespace BaseUtility
{
    public interface IAuthorizedEntity<TOwnerKey>
    {
        TOwnerKey? UserId { get; set; }
    }
}
