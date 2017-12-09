#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Business.Constants;
using Slice.Core.Underwriter.Data.Interfaces;
using Slice.Core.Underwriter.Data.Models;

namespace Slice.Core.Underwriter.Business.Managers
{
    public interface IWeatherManager
    {
        Task<Warning> AddWarningAsync(string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType);

        Task<Warning> UpdateWarningAsync(int id, string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType);
        
        Task DeleteWarningById(int id);

        IWeatherRepository<Override> GetOverridesRepository();

        IWeatherRepository<Warning> GetWarningsRepository();
    }

    public class WeatherManager : IWeatherManager
    {
        private readonly IWeatherRepository<Override> _overridesRepository;

        private readonly IWeatherRepository<Warning> _warningsRepository;

        public WeatherManager(IWeatherRepository<Override> overridesRepository, IWeatherRepository<Warning> warningsRepository)
        {
            _overridesRepository = overridesRepository;
            _warningsRepository = warningsRepository;
        }

        public IWeatherRepository<Override> GetOverridesRepository() => _overridesRepository;

        public IWeatherRepository<Warning> GetWarningsRepository() => _warningsRepository;

        public async Task<Warning> AddWarningAsync(string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType)
        {
            var warning = new Warning
            {
                Area = area,
                Country = country,
                StartsOn = startOn,
                EndsOn = endsOn,
                SearchedOn = searchedOn,
                Type = warningType.ToString()
            };

            var item = await _warningsRepository.AddAsync(warning).ConfigureAwait(false);
            return item;
        }
        
        public async Task<Warning> UpdateWarningAsync(int id, string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType)
        {
            var current = await _warningsRepository.GetAsync(id).ConfigureAwait(false);
            if (current == null)
            {
                throw new Exception("Item not found");
            }

            var updated = new Warning
            {
                Id = id, 
                Area = area,
                Country = country,
                StartsOn = startOn,
                EndsOn = endsOn,
                SearchedOn = searchedOn,
                Type = warningType.ToString()
            };

            try
            {
                var item = await _warningsRepository.UpdateAsync(id, updated).ConfigureAwait(false);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteWarningById(int id)
        {
            var current = await _warningsRepository.GetAsync(id).ConfigureAwait(false);
            if (current == null)
            {
                throw new Exception("Item not found");
            }

            await _warningsRepository.DeleteAsync(current).ConfigureAwait(false);
        }
    }
}