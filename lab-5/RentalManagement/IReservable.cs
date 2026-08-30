namespace RentalManagement;

public interface IReservable
{
    bool IsAvailable();
    void CancelReservation();
    void Reserve(string customer);
}