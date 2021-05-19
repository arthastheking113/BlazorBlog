using BlazorServer.Data;
using BlazorServer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class CarServiceTest
    {
        private readonly ApplicationDbContext _context;

        public CarServiceTest(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Car>> GetCarsAsync()
        {
            List<Car> cars = await _context.Car.ToListAsync();
            return cars;
        }
    }
}
