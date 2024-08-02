﻿
using Car.Application.Repositories;
using Car.Domain.Entities;

namespace Car.Application.Abstract
{
    public interface IMarkaReadRepository : IReadRepository<Category>
    {
    }
}
