﻿using System;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
		{
            _storeContext = storeContext;
		}

        public async Task<T> GetByIdAsync(int id)
        {
            return await _storeContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetEntityByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), spec);
        }

       

        // --- TO CREATE A GENERIC REPOSITORY WE USE
        // ----- 1. GENERIC RETURNED TYPES (Task<T>)
        // ----- 2. MAKE USE OF THE SET<T> METHOD SO THE CORRECT DBSET TYPE WILL BE SELECTED DURING RUNTIME
        // ----- 3. THE GENERIC CLASS DECLARATION USES THE <T> TO IDEINTIFY IT AS GENERIC CLASS AND CONSTRAINTS CAN BE ADDED USING THE 'where' CLAUSE, FOLLOWED BY THE CLASS THE USER CAN SET AS BASE.
    }
}

