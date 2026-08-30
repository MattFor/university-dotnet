namespace RentalManagement;

public abstract class Vehicle : IReservable
{
    public int Id { get; }
    public int Year { get; }
    public string Brand { get; }
    public string Model { get; }
    public string? ReservedBy { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    protected Vehicle(int id, string brand, string model, int year)
    {
        if (string.IsNullOrWhiteSpace(brand))
        {
            throw new ArgumentException("Brand cannot be empty!", nameof(brand));
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            throw new ArgumentException("Model cannot be empty!", nameof(model));
        }

        // No chyba że time traveller ¯\_(ツ)_/¯
        if (year < 1886)
        {
            throw new ArgumentException("Year is invalid!", nameof(year));
        }

        Id = id;
        Year = year;
        Brand = brand;
        Model = model;
    }

    public abstract string DisplayInfo();

    public void Reserve(string customer)
    {
        if (string.IsNullOrWhiteSpace(customer))
        {
            throw new ArgumentException("Customer cannot be empty!", nameof(customer));
        }

        if (!IsAvailable)
        {
            throw new InvalidOperationException($"Vehicle {Id} is already reserved!");
        }

        IsAvailable = false;
        ReservedBy = customer;
    }

    public void CancelReservation()
    {
        if (IsAvailable)
        {
            throw new InvalidOperationException($"Vehicle {Id} is not reserved!");
        }

        IsAvailable = true;
        ReservedBy = null;
    }

    bool IReservable.IsAvailable() => IsAvailable;
}