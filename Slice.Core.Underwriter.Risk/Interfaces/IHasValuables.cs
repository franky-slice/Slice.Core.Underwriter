using System.Collections.Generic;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.Interfaces
{
    public interface IHasValuables
    {
        IEnumerable<IItemOfValue> Valuables { get; set; }
    }
}