using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Linq.Expressions;


namespace TiendaServicios.Api.Libro.Tests
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumerable(IEnumerable<T> enumarable) : base(enumarable) { }
        public AsyncEnumerable(Expression expression) : base(expression) { }
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken=default)
        {
            return  new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
    
    }
}
