using BookLibrary.Models;
using BookLibrary.ReservationProcessing;

namespace BookLibrary.Libraries
{
    public class SmallLibrary
        (IReservationProcessor reservationProcessor, string name) : Library(reservationProcessor, name)
    {
        private readonly IReservationProcessor _reservationProcessor = reservationProcessor;
        private readonly string _name = name;

        public override string ProcessVisitorReservation(Book book, Visitor visitor)
        {
            Console.WriteLine($"Small library: {_name} is processing reservation");
            return _reservationProcessor.HandleReservation(book, visitor);
        }

        public override string ProcessVisitorCancelReservation(Book book, Visitor visitor)
        {
            Console.WriteLine($"Small library: {_name} is processing to cancel reservation");
            return _reservationProcessor.СancelReservation(book, visitor);
        }
    }
}
