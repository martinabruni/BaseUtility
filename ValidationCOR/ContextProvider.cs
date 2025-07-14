namespace BaseUtility
{
    public class ContextProvider<TContext>
        where TContext : class
    {
        public required TContext Context { get; set; }
    }
}
