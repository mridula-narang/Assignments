﻿namespace PizzaStoreAPI.Interfaces
{
    public interface IRepository <K,T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(K key);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(K key);
    }
}
