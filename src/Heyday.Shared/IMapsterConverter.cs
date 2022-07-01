using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Shared;

public interface IMapsterConverter<T, TD>
{
    public TD Convert(T item);

    public T ConvertBack(TD item);
}

public interface IMapsterConverterAsync<T, TD>
{
    public Task<TD> ConvertAsync(T item);

    public Task<T> ConvertBackAsync(TD item);
}