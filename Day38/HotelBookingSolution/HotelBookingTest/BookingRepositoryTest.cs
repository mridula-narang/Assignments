using AutoMapper;
using AutoMapper.Features;
using HotelBookingApp.Contexts;
using HotelBookingApp.Models;
using HotelBookingApp.Repositories;
using HotelBookingApp.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static HotelBookingApp.Models.Booking;
using static HotelBookingApp.Models.Room;

namespace HotelBookingTest
{
    public class BookingRepositoryTest
    {
        DbContextOptions options;
        HotelContext context;
        BookingRepository bookingRepository;
        RoomRepository roomRepository;
        Moq.Mock<ILogger<BookingRepository>> loggerBooking;
        Moq.Mock<ILogger<RoomRepository>> loggerRoomRepository;
        Moq.Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            loggerBooking = new Moq.Mock<ILogger<BookingRepository>>();
            loggerRoomRepository = new Moq.Mock<ILogger<RoomRepository>>();
            mapper = new Moq.Mock<IMapper>();
            bookingRepository = new BookingRepository(context, loggerBooking.Object);
            roomRepository = new RoomRepository(context, loggerRoomRepository.Object);
        }

        [Test]
        public async Task TestAdd()
        {
            var room = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            var addedRoom = await roomRepository.Add(room);
            var booking = new Booking
            {
                CheckInDate = new DateTime(2021, 12, 1),
                CheckOutDate = new DateTime(2021, 12, 5),
                RoomId = 1,
                UserId = 1,
                HotelId = 1,
                Status = BookingStatus.Pending,
                NumberOfGuests = 2
            };
            var addedBooking = await bookingRepository.Add(booking);
            Assert.IsTrue(addedBooking.BookingId == booking.BookingId);
        }

        [Test]
        public async Task TestDelete()
        {
            var room = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            var addedRoom = await roomRepository.Add(room);
            var booking = new Booking
            {
                CheckInDate = new DateTime(2021, 12, 1),
                CheckOutDate = new DateTime(2021, 12, 5),
                RoomId = 1,
                UserId = 1,
                HotelId = 1,
                Status = BookingStatus.Pending,
                NumberOfGuests = 2
            };
            var addedBooking = await bookingRepository.Add(booking);
            var deletedBooking = await bookingRepository.Delete(addedBooking.BookingId);
            Assert.IsTrue(deletedBooking.BookingId == addedBooking.BookingId);
        }

        [Test]
        public async Task TestGet()
        {
            var room = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            var addedRoom = await roomRepository.Add(room);
            var booking = new Booking
            {
                CheckInDate = new DateTime(2021, 12, 1),
                CheckOutDate = new DateTime(2021, 12, 5),
                RoomId = 1,
                UserId = 1,
                HotelId = 1,
                Status = BookingStatus.Pending,
                NumberOfGuests = 2
            };
            var addedBooking = await bookingRepository.Add(booking);
            var getBooking = await bookingRepository.Get(addedBooking.BookingId);
            Assert.IsTrue(getBooking.BookingId == addedBooking.BookingId);
        }

        [Test]
        public async Task TestGetAll()
        {
            var room = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            var addedRoom = await roomRepository.Add(room);
            var booking = new Booking
            {
                CheckInDate = new DateTime(2021, 12, 1),
                CheckOutDate = new DateTime(2021, 12, 5),
                RoomId = 1,
                UserId = 1,
                HotelId = 1,
                Status = BookingStatus.Pending,
                NumberOfGuests = 2
            };
            var addedBooking = await bookingRepository.Add(booking);
            var bookings = await bookingRepository.GetAll();
            Assert.IsTrue(bookings.Count() == 1);
        }

        [Test]
        public async Task TestUpdate()
        {
            var room = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            var addedRoom = await roomRepository.Add(room);
            var booking = new Booking
            {
                CheckInDate = new DateTime(2021, 12, 1),
                CheckOutDate = new DateTime(2021, 12, 5),
                RoomId = 1,
                UserId = 1,
                HotelId = 1,
                Status = BookingStatus.Pending,
                NumberOfGuests = 2
            };
            var addedBooking = await bookingRepository.Add(booking);
            addedBooking.Status = BookingStatus.Confirmed;
            var updatedBooking = await bookingRepository.Update(addedBooking.BookingId, addedBooking);
            Assert.IsTrue(updatedBooking.Status == BookingStatus.Confirmed);
        }

        [Test]
        public async Task TestUpdateThrowsNotFoundException()
        {
            var room = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            var addedRoom = await roomRepository.Add(room);
            var booking = new Booking
            {
                CheckInDate = new DateTime(2021, 12, 1),
                CheckOutDate = new DateTime(2021, 12, 5),
                RoomId = 1,
                UserId = 1,
                HotelId = 1,
                Status = BookingStatus.Pending,
                NumberOfGuests = 2
            };
            var addedBooking = await bookingRepository.Add(booking);
            Assert.ThrowsAsync<NotFoundException>(() => bookingRepository.Update(2, new Booking
            {
                CheckInDate = new DateTime(2021, 12, 1),
                CheckOutDate = new DateTime(2021, 12, 5),
                RoomId = 1,
                UserId = 1,
                HotelId = 1,
                Status = BookingStatus.Pending,
                NumberOfGuests = 2
            }));
        }

        //test for not found exception and collection empty exception
        [Test]
        public async Task NotFoundException()
        {
            // Assert
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await bookingRepository.Get(999)));
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await bookingRepository.Get(2)));
        }

        [Test]
        public async Task CollectionEmptyException()
        {
            // Assert
            await Task.Run(() => Assert.ThrowsAsync<CollectionEmptyException>(async () => await bookingRepository.GetAll()));
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
