﻿using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface IBrandService
    {
        public Task Insert(Brand brand);
        public Task<List<Brand>> GetAll();
        public Task Update(Brand brand);
        public Task Delete(int id);
        public Task<Brand> GetById(int id);
    }
}
