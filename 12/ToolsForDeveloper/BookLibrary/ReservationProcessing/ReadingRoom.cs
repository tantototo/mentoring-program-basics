using BookLibrary.Models;

namespace BookLibrary.ReservationProcessing
{
    public class ReadingRoom : IReservationProcessor
    {
        public string HandleReservation(Book book, Visitor visitor)
        {
            Thread.Sleep(3000);
            return $"Book '{book.Name}' successfully booked for the reading room " +
                $"by visitor {visitor.FirstName} {visitor.LastName} for n hours";
        }

        public string СancelReservation(Book book, Visitor visitor)
        {
            Thread.Sleep(3000);
            return $"Book '{book.Name}' successfully returned from the reading room " +
                $"by visitor {visitor.FirstName} {visitor.LastName}";
        }
    }
}
