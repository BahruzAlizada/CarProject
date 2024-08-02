﻿using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.Repositories;

namespace Car.Persistence.EntityFramework
{
    public class EngineTypeWriteRepository : WriteRepository<EngineType>,IEngineTypeWriteRepository
    {
    }
}
