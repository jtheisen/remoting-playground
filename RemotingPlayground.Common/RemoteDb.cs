using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingPlayground
{
    public static class Db
    {
        public static IDbSetAccessor accessor = new DummyDbSetAccessor();

        public static IQueryable<T> Set<T>() where T : class => accessor.Set<T>();
    }

    public interface IDbSetAccessor
    {
        IQueryable<T> Set<T>() where T : class;
    }

    public class DummyDbSetAccessor : IDbSetAccessor
    {
        public IQueryable<T> Set<T>() where T : class => throw new NotImplementedException();
    }
}
