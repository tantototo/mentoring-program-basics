using BookLibrary.Models;
using BookLibrary.ReservationProcessing;

namespace BookLibrary
{
    public abstract class Library(IReservationProcessor reservationProcessor, string name)
    {
        private IReservationProcessor _reservationProcessor = reservationProcessor;
        private readonly string _name = name;

        public virtual string ProcessVisitorReservation(Book book, Visitor visitor)
        {
            throw new NotImplementedException("Please override this method in a concrete implementation");
        }

        public virtual string ProcessVisitorCancelReservation(Book book, Visitor visitor)
        {
            throw new NotImplementedException("Please override this method in a concrete implementation");
        }
    }
}
