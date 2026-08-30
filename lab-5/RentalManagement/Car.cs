namespace RentalManagement;

public class Car : Vehicle
{
    public string BodyType { get; }

    public Car(int id, string brand, string model, int year, string bodyType) : base(id, brand, model, year)
    {
        if (string.IsNullOrWhiteSpace(bodyType))
        {
            throw new ArgumentException("Body type cannot be empty!", nameof(bodyType));
        }

        BodyType = bodyType;
    }

    public override string DisplayInfo()
    {
        return $"Car\t\t|- ID: {Id} -> Brand: {Brand} | Model: {Model} | Year: {Year} | BodyType: {BodyType} | Available: {IsAvailable}";
    }
}