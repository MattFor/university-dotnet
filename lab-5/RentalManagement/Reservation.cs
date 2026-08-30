namespace RentalManagement;

public class Reservation
{
    public int VehicleId { get; }
    public string Customer { get; }
    public DateTime ReservedAt { get; }
    public bool IsCancelled { get; private set; }
    public DateTime? CancelledAt { get; private set; }

    public Reservation(int vehicleId, string customer)
    {
        if (string.IsNullOrWhiteSpace(customer))
        {
            throw new ArgumentException("Customer cannot be empty!", nameof(customer));
        }

        VehicleId = vehicleId;
        Customer = customer;
        ReservedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException("Reservation is already cancelled!");
        }

        IsCancelled = true;
        CancelledAt = DateTime.UtcNow;
    }
}