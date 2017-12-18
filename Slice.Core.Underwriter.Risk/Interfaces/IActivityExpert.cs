using System;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.Interfaces
{
    public interface IActivityExpert : IDisposable
    {
        Task<int> GetRiskValue(IActivity subject);
    }
}