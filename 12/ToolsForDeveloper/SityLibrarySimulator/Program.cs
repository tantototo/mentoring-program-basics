using BookLibrary.Libraries;
using BookLibrary.Models;
using BookLibrary.ReservationProcessing;

/// We have multiple libraries,
/// where each type of library processes book reservations 
/// using different types of room services 
/// including a reading room and take-home options.

Console.WriteLine("Welcome to the Sity Library service!");

const string sadovaya = "Sadovaya lib";
const string zanevsky = "Zanevsky lib";
const string fontanka = "Fontanka River lib";

var homeRoom = new HomeRoom();
var readingRoom = new ReadingRoom();

var lib1 = new BigLibrary(homeRoom, sadovaya);
var lib2 = new BigLibrary(readingRoom, sadovaya);
var lib3 = new SmallLibrary(homeRoom, zanevsky);
var lib4 = new SmallLibrary(readingRoom, fontanka);

var book1 = new Book() { Name = "Lord of the Rings" };
var book2 = new Book() { Name = "Sapiens" };
var book3 = new Book() { Name = "It" };

var visitor1 = new Visitor() { FirstName = "Сucu", LastName = "Mber" };
var visitor2 = new Visitor() { FirstName = "Arti", LastName = "Choke" };
var visitor3 = new Visitor() { FirstName = "Cab", LastName = "Bage" };

lib1.ProcessVisitorReservation(book1, visitor1);
lib1.ProcessVisitorReservation(book2, visitor2);
lib1.ProcessVisitorCancelReservation(book1, visitor1);
lib2.ProcessVisitorReservation(book1, visitor2);

lib3.ProcessVisitorReservation(book3, visitor3);
lib3.ProcessVisitorCancelReservation(book3, visitor3);

Console.WriteLine(lib4.ProcessVisitorReservation(book2, visitor3));
Console.WriteLine(lib4.ProcessVisitorCancelReservation(book2, visitor3));
