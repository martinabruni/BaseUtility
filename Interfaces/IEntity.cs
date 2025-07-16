namespace BaseUtility
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
