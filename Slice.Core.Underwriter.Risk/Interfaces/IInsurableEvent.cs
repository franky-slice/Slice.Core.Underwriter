using Slice.Core.Underwriter.Common.Models;

namespace Slice.Core.Underwriter.Risk.Interfaces
{
    public interface IInsurableEvent
    {
        int BaseRisk { get; set; }

        Threshold AcceptableRisk { get; set; }

        bool IsInsurable(int risk);
    }
}