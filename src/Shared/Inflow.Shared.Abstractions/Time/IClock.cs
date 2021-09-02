using System;

namespace Inflow.Shared.Abstractions.Time
{
    public interface IClock
    {
        DateTime CurrentDate();
    }
}