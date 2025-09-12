namespace BaseUtility
{
    public abstract class BaseDomain<TKey> : IEntity<TKey>, IAuditable
    {
        public required TKey Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
