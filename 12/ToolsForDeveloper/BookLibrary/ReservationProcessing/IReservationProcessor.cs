using BookLibrary.Models;

namespace BookLibrary.ReservationProcessing
{
    public interface IReservationProcessor
    {
        string HandleReservation(Book book, Visitor visitor);
        string СancelReservation(Book book, Visitor visitor);
    }
}
