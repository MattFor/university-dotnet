using RentalManagement;

var rentalCompany = new RentalCompany();

rentalCompany.AddVehicle(new Car(1, "Toyota", "Corolla", 2020, "Sedan"));
rentalCompany.AddVehicle(new Motorcycle(2, "Yamaha", "MT-07", 2021, 689));
rentalCompany.AddVehicle(new Car(3, "Skoda", "Octavia", 2022, "Hatchback"));

rentalCompany.OnNewReservation += message => Console.WriteLine(message);

Console.WriteLine("Available vehicles:");
foreach (var vehicle in rentalCompany.ListAvailableVehicles())
{
    Console.WriteLine(vehicle.DisplayInfo());
}

Console.WriteLine();
rentalCompany.ReserveVehicle(1, "John Doe");

Console.WriteLine();
Console.WriteLine("Available vehicles after reservation:");
foreach (var vehicle in rentalCompany.ListAvailableVehicles())
{
    Console.WriteLine(vehicle.DisplayInfo());
}