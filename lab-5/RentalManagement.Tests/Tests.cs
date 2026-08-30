namespace RentalManagement.Tests;

public class Tests
{
    [Fact]
    public void Car_ShouldStorePropertiesCorrectly()
    {
        var car = new Car(1, "Toyota", "Corolla", 2020, "Sedan");

        Assert.Equal(1, car.Id);
        Assert.Equal("Toyota", car.Brand);
        Assert.Equal("Corolla", car.Model);
        Assert.Equal(2020, car.Year);
        Assert.Equal("Sedan", car.BodyType);
        Assert.True(car.IsAvailable);
    }

    [Fact]
    public void ReserveVehicle_ShouldMakeVehicleUnavailable_AndRaiseEvent()
    {
        var company = new RentalCompany();
        company.AddVehicle(new Car(1, "Toyota", "Corolla", 2020, "Sedan"));

        string? eventMessage = null;
        company.OnNewReservation += message => eventMessage = message;

        company.ReserveVehicle(1, "John Doe");

        var vehicle = company.Vehicles.First();
        Assert.False(vehicle.IsAvailable);
        Assert.Equal("John Doe", vehicle.ReservedBy);
        Assert.NotNull(eventMessage);
        Assert.Contains("John Doe", eventMessage);
    }

    [Fact]
    public void CancelReservation_ShouldMakeVehicleAvailableAgain()
    {
        var company = new RentalCompany();
        company.AddVehicle(new Car(1, "Toyota", "Corolla", 2020, "Sedan"));

        company.ReserveVehicle(1, "John Doe");
        company.CancelReservation(1);

        var vehicle = company.Vehicles.First();
        Assert.True(vehicle.IsAvailable);
        Assert.Null(vehicle.ReservedBy);
    }

    [Fact]
    public void GetAvailableVehicles_Extension_ShouldReturnOnlyAvailableVehicles()
    {
        var vehicles = new List<Vehicle>
        {
            new Car(1, "Toyota", "Corolla", 2020, "Sedan"),
            new Motorcycle(2, "Yamaha", "MT-07", 2021, 689)
        };

        vehicles[0].Reserve("John");

        var available = vehicles.GetAvailableVehicles();

        Assert.Single(available);
        Assert.Equal(2, available[0].Id);
    }

    [Fact]
    public void SearchAvailableVehicles_ShouldUseLambdaAndFilterCorrectly()
    {
        var company = new RentalCompany();
        company.AddVehicle(new Car(1, "Toyota", "Corolla", 2020, "Sedan"));
        company.AddVehicle(new Car(2, "Honda", "Civic", 2021, "Hatchback"));
        company.AddVehicle(new Motorcycle(3, "Yamaha", "MT-07", 2022, 689));

        company.ReserveVehicle(2, "Alice");

        var result = company.SearchAvailableVehicles(v => v.Brand.StartsWith("T"));

        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
    }
}