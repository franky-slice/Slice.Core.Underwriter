using System;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.Interfaces
{
    public interface IEstimator : IDisposable
    {
        Task<int> GetValue(IItemOfValue subject);
    }
}