﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        T? GetById(int id);

        List<T> List();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void SaveChanges();
    }
}
