using BlazorServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public interface ICarServices
    {
        public Task<List<Car>> GetCarsAsync();

        public Task CreateAsync();
    }
}
