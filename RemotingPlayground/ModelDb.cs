using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RemotingPlayground
{
    public class ConcreteDbSetAccessor : IDbSetAccessor
    {
        public IQueryable<T> Set<T>() where T : class => new ModelDb().Set<T>();
    }

    public class ModelDb : DbContext
    {
        public ModelDb()
            : this(null)
        {
        }

        public ModelDb(String nameOrConnectionString)
            : base(nameOrConnectionString ?? @"Data Source=tcp:ao1p1e06mh.database.windows.net;user=who;password=Qwerty42;database=remotingplay")
        {
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Company> Companies { get; set; }
    }
}
