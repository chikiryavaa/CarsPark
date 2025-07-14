using System.Collections.Generic;

namespace CarPark.Models
{
    public abstract class EngineType
    {
        public abstract string Key { get; }
        public abstract string Name { get; }
    }

    public class PetrolEngine : EngineType
    {
        public override string Key => "Petrol";
        public override string Name => "Бензиновый";
       
    }

    public class DieselEngine : EngineType
    {
        public override string Key => "Diesel";
        public override string Name => "Дизельный";
        
    }

    public static class EngineFactory
    {
        public static EngineType Create(string key)
        {
            switch (key)
            {
                case "Petrol": return new PetrolEngine();
                case "Diesel": return new DieselEngine();
                default: return new PetrolEngine();
            }
        }
        public static List<EngineType> AllTypes()
            => new List<EngineType>
               { new PetrolEngine(), new DieselEngine() };
    }
}
