#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Risk.Interfaces;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.ActivityExperts
{
    public abstract class BaseActivityExpert : IActivityExpert
    {
        private bool _disposed;

        protected int RiskLevel;

        public abstract Task<int> GetRiskValue(IActivity subject);

        #region IDisposible

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}