using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.Interfaces
{
    public interface IHasOwner
    {
        IPerson Owner { get; set; }
    }
}