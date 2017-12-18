#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Common.Helpers;
using Slice.Core.Underwriter.Risk.ActivityExperts;
using Slice.Core.Underwriter.Risk.Estimators;
using Slice.Core.Underwriter.Risk.Interfaces;

namespace Slice.Core.Underwriter.Risk.Services
{
    public interface IRiskService
    {
        Task<double> GetRiskBase(IInsurableEvent subject);

        Task<double> GetRiskFromActivity(IHasActivity subject);

        Task<double> GetRiskFromOwner(IHasOwner subject);

        Task<double> GetRiskFromValuables(IHasValuables subject);

        Task<double> GetRiskCompounded(IEnumerable<double> associatedRisks);
    }

    public class RiskService : IRiskService
    {
        public async Task<double> GetRiskBase(IInsurableEvent subject)
        {
            return (double) subject.BaseRisk / 100;
        }

        public async Task<double> GetRiskFromActivity(IHasActivity subject)
        {
            double result;
            using (var expert = ActivityExpertFactory.GetExpert(subject.Activity))
            {
                var value = await expert.GetRiskValue(subject.Activity).ConfigureAwait(false);
                result = (double) value / 100;
            }

            return result;
        }

        public async Task<double> GetRiskFromOwner(IHasOwner subject)
        {
            var owner = subject.Owner;
            var level = await ValueHelper.GetRandomDouble().ConfigureAwait(false);
            return level;
        }

        public async Task<double> GetRiskFromValuables(IHasValuables subject)
        {
            var totalValue = 0;
            double result = 0;

            foreach (var item in subject.Valuables)
            {
                using (var estimator = EstimatorFactory.GetEstimator(item))
                {
                    var value = await estimator.GetValue(item).ConfigureAwait(false);
                    totalValue += value;
                }
            }

            if (totalValue > 100)
            {
                result = 1.0;
            }

            return result;
        }

        public async Task<double> GetRiskCompounded(IEnumerable<double> associatedRisks)
        {
            return await Task.Run(() =>
            {
                double result = 0;
                if (associatedRisks.Any())
                {
                    result = associatedRisks.Sum(x => x) / associatedRisks.Count();
                }

                return result;
            }).ConfigureAwait(false);
        }

        #region Helpers

        #endregion
    }
}