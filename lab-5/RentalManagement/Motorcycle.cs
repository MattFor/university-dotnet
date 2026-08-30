namespace RentalManagement;

public class Motorcycle : Vehicle
{
    public int EngineCapacity { get; }

    public Motorcycle(int id, string brand, string model, int year, int engineCapacity) : base(id, brand, model, year)
    {
        if (engineCapacity <= 0)
        {
            throw new ArgumentException("Engine capacity must be greater than zero!", nameof(engineCapacity));
        }

        EngineCapacity = engineCapacity;
    }

    public override string DisplayInfo()
    {
        return $"Motorcycle\t|- ID: {Id} -> Brand: {Brand} | Model: {Model} | Year: {Year} | EngineCapacity: {EngineCapacity}cc | Available: {IsAvailable}";
    }
}