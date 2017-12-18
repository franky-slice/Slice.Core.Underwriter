#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Common.Constants;
using Slice.Core.Underwriter.Data.Interfaces;
using Slice.Core.Underwriter.Data.Models.Weather;

namespace Slice.Core.Underwriter.Weather.Managers
{
    public interface IWeatherOverrideManager
    {
        IWeatherRepository<Override> GetRepository();

        Task<Override> GetByIdAsync(Guid id);

        Task<IEnumerable<Override>> GetByAsync(Expression<Func<Override, bool>> predicate);

        Task<IEnumerable<Override>> GetAllAsync();
        
        Task<Override> AddAsync(string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType);

        Task<Override> UpdateAsync(Guid id, string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType);
        
        Task DeleteById(Guid id);

        Task<IEnumerable<Override>> GetByAreaOn(string country, string area, DateTime? effectiveOn);
    }

    public class WeatherOverrideManager : IWeatherOverrideManager
    {
        private readonly IWeatherRepository<Override> _repository;

        public WeatherOverrideManager(IWeatherRepository<Override> repository)
        {
            _repository = repository;
        }

        public IWeatherRepository<Override> GetRepository() => _repository;

        public async Task<IEnumerable<Override>> GetAllAsync()
        {
            return await _repository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Override> GetByIdAsync(Guid id)
        {
            return await _repository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Override>> GetByAsync(Expression<Func<Override, bool>> predicate)
        {
            return await _repository.FindAllAsync(predicate).ConfigureAwait(false);
        }

        public async Task<Override> AddAsync(string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType)
        {
            var warning = new Override
            {
                Area = area,
                Country = country,
                StartsOn = startOn,
                EndsOn = endsOn,
            };

            var item = await _repository.AddAsync(warning).ConfigureAwait(false);
            return item;
        }
        
        public async Task<Override> UpdateAsync(Guid id, string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType)
        {
            var current = await _repository.GetAsync(id).ConfigureAwait(false);
            if (current == null)
            {
                throw new Exception("Item not found");
            }

            var updated = new Override
            {
                Area = area,
                Country = country,
                StartsOn = startOn,
                EndsOn = endsOn,
            };

            try
            {
                var item = await _repository.UpdateAsync(id, updated).ConfigureAwait(false);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteById(Guid id)
        {
            var current = await _repository.GetAsync(id).ConfigureAwait(false);
            if (current == null)
            {
                throw new Exception("Item not found");
            }

            await _repository.DeleteAsync(current).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Override>> GetByAreaOn(string country, string area, DateTime? effectiveOn)
        {
            if (!effectiveOn.HasValue)
            {
                effectiveOn = DateTime.UtcNow;
            }
            return await GetByAsync(x => x.Area == area && x.Country == country && x.StartsOn <= effectiveOn && x.EndsOn >= effectiveOn).ConfigureAwait(false);
        }
    }
}