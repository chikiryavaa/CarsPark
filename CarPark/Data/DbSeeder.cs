using CarPark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPark.Data
{
    public static class DbSeeder
    {
        public static void Seed(ParkContext ctx)
        {

            if (!ctx.Vehicles.Any())
            {
                var vehicles = new List<Vehicle>
                {
                    new Vehicle { Brand = "Toyota", Model = "Camry", LicensePlate = "A123BC", EngineKey = "Petrol" },
                    new Vehicle { Brand = "Ford", Model = "Focus", LicensePlate = "B456CD", EngineKey = "Petrol" },
                    new Vehicle { Brand = "Volkswagen", Model = "Passat", LicensePlate = "C789DE", EngineKey = "Diesel" },
                    new Vehicle { Brand = "Renault", Model = "Duster", LicensePlate = "D321EF", EngineKey = "Diesel" }
                };

                ctx.Vehicles.AddRange(vehicles);
                ctx.SaveChanges();
            }

            if (!ctx.ServiceTypes.Any())
            {
                var services = new List<ServiceType>
                {
                    new ServiceType { EngineKey = "Petrol", Name = "Замена масла" },
                    new ServiceType { EngineKey = "Petrol", Name = "Проверка свечей зажигания" },
                    new ServiceType { EngineKey = "Diesel", Name = "Замена масла" },
                    new ServiceType { EngineKey = "Diesel", Name = "Проверка свечей накала" }
                };

                ctx.ServiceTypes.AddRange(services);
                ctx.SaveChanges();
            }

            if (!ctx.Records.Any())
            {
                var vehicles = ctx.Vehicles.ToList();

                var records = new List<MaintenanceRecord>
                {
                    new MaintenanceRecord
                    {
                        VehicleId = vehicles.First(v => v.EngineKey == "Petrol").Id,
                        Date = DateTime.Today.AddDays(-5),
                        ServiceName = "Замена масла"
                    },
                    new MaintenanceRecord
                    {
                        VehicleId = vehicles.First(v => v.EngineKey == "Diesel").Id,
                        Date = DateTime.Today.AddDays(-10),
                        ServiceName = "Проверка свечей накала"
                    }
                };

                ctx.Records.AddRange(records);
                ctx.SaveChanges();
            }
        }
    }
}
