namespace RentalManagement;

public class RentalCompany
{
    private readonly List<Vehicle> _vehicles = new();
    private readonly List<Reservation> _reservations = new();

    public event Action<string>? OnNewReservation;

    public IReadOnlyList<Vehicle> Vehicles => _vehicles.AsReadOnly();
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public void AddVehicle(Vehicle vehicle)
    {
        if (vehicle is null)
        {
            throw new ArgumentNullException(nameof(vehicle));
        }

        if (_vehicles.Any(v => v.Id == vehicle.Id))
        {
            throw new InvalidOperationException($"Vehicle with ID {vehicle.Id} already exists!");
        }

        _vehicles.Add(vehicle);
    }

    public void ReserveVehicle(int vehicleId, string customer)
    {
        var vehicle = FindVehicle(vehicleId);
        vehicle.Reserve(customer);

        var reservation = new Reservation(vehicleId, customer);
        _reservations.Add(reservation);

        OnNewReservation?.Invoke($"New reservation: Vehicle {vehicleId} reserved by {customer} at {reservation.ReservedAt:u}");
    }

    public void CancelReservation(int vehicleId)
    {
        var reservation = _reservations.LastOrDefault(r => r.VehicleId == vehicleId && !r.IsCancelled);

        if (reservation is null)
        {
            throw new InvalidOperationException($"No active reservation found for vehicle {vehicleId}!");
        }

        var vehicle = FindVehicle(vehicleId);
        vehicle.CancelReservation();
        reservation.Cancel();
    }

    public List<Vehicle> ListAvailableVehicles()
    {
        return _vehicles.Where(v => v.IsAvailable).ToList();
    }

    public List<Vehicle> SearchAvailableVehicles(Func<Vehicle, bool> predicate)
    {
        return _vehicles.Where(v => v.IsAvailable).Where(predicate).ToList();
    }

    private Vehicle FindVehicle(int vehicleId)
    {
        var vehicle = _vehicles.FirstOrDefault(v => v.Id == vehicleId);

        if (vehicle is null)
        {
            throw new KeyNotFoundException($"Vehicle with ID {vehicleId} was not found!");
        }

        return vehicle;
    }
}