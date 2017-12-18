#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Data.Interfaces;
using Slice.Core.Underwriter.Data.Models.Rce;

namespace Slice.Core.Underwriter.Business.Managers
{
    public interface IDwellingRceManager
    {
        IDwellingRepository<RceByAdministrativeLevel1> GetRepository();

        Task<RceByAdministrativeLevel1> GetByIdAsync(Guid id);

        Task<IEnumerable<RceByAdministrativeLevel1>> GetByAsync(Expression<Func<RceByAdministrativeLevel1, bool>> predicate);

        Task<IEnumerable<RceByAdministrativeLevel1>> GetAllAsync();
        
        Task<RceByAdministrativeLevel1> GetByArea(string country, string area);
    }

    public class DwellingRceManager : IDwellingRceManager
    {
        private readonly IDwellingRepository<RceByAdministrativeLevel1> _repository;

        public DwellingRceManager(IDwellingRepository<RceByAdministrativeLevel1> repository)
        {
            _repository = repository;
        }

        public IDwellingRepository<RceByAdministrativeLevel1> GetRepository() => _repository;

        public async Task<IEnumerable<RceByAdministrativeLevel1>> GetAllAsync()
        {
            return await _repository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<RceByAdministrativeLevel1> GetByIdAsync(Guid id)
        {
            return await _repository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<RceByAdministrativeLevel1>> GetByAsync(Expression<Func<RceByAdministrativeLevel1, bool>> predicate)
        {
            return await _repository.FindAllAsync(predicate).ConfigureAwait(false);
        }
       
        public async Task<RceByAdministrativeLevel1> GetByArea(string country, string area)
        {
            var values = await GetByAsync(x => x.AdministrativeLevel1 == area && x.Country == country).ConfigureAwait(false);
            return values.FirstOrDefault();
        }
    }
}