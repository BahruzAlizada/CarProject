﻿using Car.Application.Repositories;
using Car.Domain.Entities;

namespace Car.Application.Abstract
{
    public interface ISeatReadRepository : IReadRepository<Seat>
    {
    }
}
