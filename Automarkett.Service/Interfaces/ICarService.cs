using System.Collections.Generic;
using System.Threading.Tasks;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModals.Car;

namespace Automarkett.Service.Interfaces
{
    public interface ICarService
    {
        Task<BaseResponse<IEnumerable<Car>>> GetCars();

        Task<BaseResponse<Car>> GetCar(int id);

        Task<BaseResponse<Car>> CreateCar(CarViewModal car);

        Task<BaseResponse<bool>> DeleteCar(int id);

        Task<BaseResponse<Car>> GetCarByName(string str);

        Task<BaseResponse<Car>> Edit(int id, CarViewModal model);
    }
}