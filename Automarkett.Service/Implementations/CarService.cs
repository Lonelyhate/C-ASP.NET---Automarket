using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Enum;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModals.Car;
using Automarkett.Service.Interfaces;

namespace Automarkett.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<BaseResponse<Car>> CreateCar(CarViewModal car)
        {
            var baseResponse = new BaseResponse<Car>();

            try
            {
                var carCr = new Car()
                {
                    Description = car.Description,
                    DateCreate = car.DateCreate,
                    Model = car.Model,
                    Name = car.Name,
                    Price = car.Price,
                    Speed = car.Speed,
                    TypeCar = (TypeCar)Convert.ToInt32(car.TypeCar)
                };
                var response = await _carRepository.Create(carCr);
                if (response != true)
                {
                    baseResponse.Description = "Car not created";
                    baseResponse.StatusCode = StatusCode.InternalServeError;
                    return baseResponse;
                }

                baseResponse.Data = carCr;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"CarCreate - {ex.Message}",
                    StatusCode = StatusCode.InternalServeError
                };
            }
        }

        public async Task<BaseResponse<Car>> GetCar(int id)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.Get(id);
                if (car is null)
                {
                    baseResponse.Description = "Car not found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    return baseResponse;
                }

                baseResponse.Data = car;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServeError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();
            try
            {
                var cars = await _carRepository.Select();

                if (cars.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = cars;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[GetCars] : {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteCar(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var response = await _carRepository.Get(id);

                if (response is null)
                {
                    baseResponse.Data = false;
                    baseResponse.Description = "Car is not found";
                    baseResponse.StatusCode = StatusCode.InternalServeError;

                    return baseResponse;
                }

                await _carRepository.Delete(response);
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = $"DeleteCar - {ex.Message}",
                    StatusCode = StatusCode.InternalServeError
                };
            }
        }

        public async Task<BaseResponse<Car>> GetCarByName(string str)
        {
            var baseResponse = new BaseResponse<Car>();

            try
            {
                var car = await _carRepository.GetByName(str);

                if (car is null)
                {
                    baseResponse.Description = "Car is not found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    return baseResponse;
                }

                baseResponse.Data = car;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"GetCarByName - {ex.Message}",
                    StatusCode = StatusCode.InternalServeError
                };
            }
        }

        public async Task<BaseResponse<Car>> Edit(int id, CarViewModal model)
        {
            var baseResponse = new BaseResponse<Car>();
            
            try
            {
                var car = await _carRepository.Get(id);
                if (car is null)
                {
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    baseResponse.Description = "Car not found";
                    return baseResponse;
                }

                car.Description = model.Description;
                car.Model = model.Model;
                car.Name = model.Name;
                car.Price = model.Price;
                car.Speed = model.Speed;
                car.DateCreate = model.DateCreate;

                await _carRepository.Update(car);
                baseResponse.Data = car;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"Edit car - {ex.Message}",
                    StatusCode = StatusCode.InternalServeError
                };
            }
        }
    }
}