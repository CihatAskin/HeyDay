using Heyday.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Infrastructure.Conventers;

public class OrderByConverter : IMapsterConverter<string?, string[]>
{
    public string[] Convert(string? item)
    {
        if (!string.IsNullOrWhiteSpace(item))
        {
            return item
                .Split(',')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim()).ToArray();
        }

        return Array.Empty<string>();
    }

    public string? ConvertBack(string[]? item)
    {
        return item?.Any() == true ? string.Join(",", item) : null;
    }
}