using Microsoft.EntityFrameworkCore;

namespace BaseUtility
{
    public interface IDatabase<TContext>
        where TContext : DbContext
    {
        TContext Context { get; }
    }
}
