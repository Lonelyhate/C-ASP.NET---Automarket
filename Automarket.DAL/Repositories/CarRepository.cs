using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automarket.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL.Repositories
{
    public class CarRepository : Interfaces.ICarRepository
    {
        private readonly ApplicationDbContext _db;

        public CarRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Car model)
        {
            await _db.Car.AddAsync(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Car> Get(int id)
        {
            return await _db.Car.FirstOrDefaultAsync(car => car.Id == id);
        }

        public async Task<List<Car>> Select()
        {
            return await _db.Car.ToListAsync();
        }

        public async Task<bool> Delete(Car model)
        {
            _db.Car.Remove(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Car> GetByName(string name)
        {
            return await _db.Car.FirstOrDefaultAsync(car => car.Name == name);
        }

        public async Task<Car> Update(Car model)
        {
            _db.Car.Update(model);
            await _db.SaveChangesAsync();

            return model;
        }
    }
}