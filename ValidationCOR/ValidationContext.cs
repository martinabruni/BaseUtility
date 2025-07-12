namespace BaseUtility
{
    public class ValidationContext<TContext>
        where TContext : class
    {
        public required TContext Context { get; set; }
    }
}
