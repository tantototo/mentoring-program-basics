using BookLibrary.Models;

namespace BookLibrary.ReservationProcessing
{
    public class HomeRoom : IReservationProcessor
    {
        public string HandleReservation(Book book, Visitor visitor)
        {
            Thread.Sleep(3000);
            return $"Book '{book.Name}' successfully booked " +
                $"by visitor {visitor.FirstName} {visitor.LastName} for n days";
        }

        public string СancelReservation(Book book, Visitor visitor)
        {
            Thread.Sleep(3000);
            return $"Book '{book.Name}' successfully returned " +
                $"by visitor {visitor.FirstName} {visitor.LastName}";
        }
    }
}
