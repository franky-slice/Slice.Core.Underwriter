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
using Slice.Core.Underwriter.Data.Interfaces;
using Slice.Core.Underwriter.Data.Models;
using Slice.Core.Underwriter.Weather.Constants;

namespace Slice.Core.Underwriter.Weather.Managers
{
    public interface IWeatherWarningManager
    {
        IWeatherRepository<Warning> GetRepository();

        Task<IEnumerable<Warning>> GetByAsync(Expression<Func<Warning, bool>> predicate);

        Task<IEnumerable<Warning>> GetAllAsync();

        Task<Warning> GetByIdAsync(int id);
        
        Task<Warning> AddAsync(string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType);

        Task<Warning> UpdateAsync(int id, string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType);
        
        Task DeleteById(int id);

        Task<IEnumerable<Warning>> GetByAreaOn(string country, string area, DateTime? effectiveOn);
    }

    public class WeatherWarningManager : IWeatherWarningManager
    {
        private readonly IWeatherRepository<Warning> _repository;

        public WeatherWarningManager(IWeatherRepository<Warning> repository)
        {
            _repository = repository;
        }

        public IWeatherRepository<Warning> GetRepository() => _repository;

        public async Task<IEnumerable<Warning>> GetAllAsync()
        {
            return await _repository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Warning> GetByIdAsync(int id)
        {
            return await _repository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Warning>> GetByAsync(Expression<Func<Warning, bool>> predicate)
        {
            return await _repository.FindAllAsync(predicate).ConfigureAwait(false);
        }

        public async Task<Warning> AddAsync(string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType)
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

            var item = await _repository.AddAsync(warning).ConfigureAwait(false);
            return item;
        }
        
        public async Task<Warning> UpdateAsync(int id, string country, string area, DateTime searchedOn, DateTime startOn, DateTime endsOn, WarningType warningType)
        {
            var current = await _repository.GetAsync(id).ConfigureAwait(false);
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
                var item = await _repository.UpdateAsync(id, updated).ConfigureAwait(false);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteById(int id)
        {
            var current = await _repository.GetAsync(id).ConfigureAwait(false);
            if (current == null)
            {
                throw new Exception("Item not found");
            }

            await _repository.DeleteAsync(current).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Warning>> GetByAreaOn(string country, string area, DateTime? effectiveOn)
        {
            if (!effectiveOn.HasValue)
            {
                effectiveOn = DateTime.UtcNow;
            }
            return await GetByAsync(x => x.Area == area && x.Country == country && x.StartsOn <= effectiveOn && x.EndsOn >= effectiveOn).ConfigureAwait(false);
        }
    }
}