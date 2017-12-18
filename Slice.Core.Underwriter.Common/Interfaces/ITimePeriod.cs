using System;

namespace Slice.Core.Underwriter.Common.Interfaces
{
    public interface ITimePeriod
    {
        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }
    }
}