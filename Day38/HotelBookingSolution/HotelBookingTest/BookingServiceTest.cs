using AutoMapper;
using HotelBookingApp.Contexts;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Repositories;
using HotelBookingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingTest
{
    public class BookingServiceTest
    {
        private DbContextOptions options;
        private HotelContext context;
        private BookingRepository bookingRepository;
        private RoomRepository roomRepository;
        private UserRepository userRepository;
        private Moq.Mock<ILogger<BookingRepository>> bookingLogger;
        private Moq.Mock<ILogger<RoomRepository>> roomLogger;
        private Moq.Mock<IMapper> mapper;
        private Moq.Mock<IEmailService> emailService;
        private BookingService bookingService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
                .UseInMemoryDatabase("BookingTestDb")
                .Options;
            context = new HotelContext(options);
            bookingLogger = new Moq.Mock<ILogger<BookingRepository>>();
            roomLogger = new Moq.Mock<ILogger<RoomRepository>>();
            mapper = new Moq.Mock<IMapper>();
            emailService = new Moq.Mock<IEmailService>();

            bookingRepository = new BookingRepository(context, bookingLogger.Object);
            roomRepository = new RoomRepository(context, roomLogger.Object);
            bookingService = new BookingService(bookingRepository, roomRepository, mapper.Object, emailService.Object,userRepository);
        }

        [Test]
        public async Task TestAddBooking()
        {
            var room = new Room { RoomId = 1, Price = 100 };
            await roomRepository.Add(room);

            var bookingDTO = new BookingDTO
            {
                RoomId = room.RoomId,
                UserId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = Booking.BookingStatus.Pending
            };
            var booking = new Booking
            {
                RoomId = room.RoomId,
                UserId = 1,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                Status = bookingDTO.Status
            };
            mapper.Setup(m => m.Map<Booking>(bookingDTO)).Returns(booking);

            var addedBooking = await bookingService.AddBooking(bookingDTO);

            Assert.AreEqual(addedBooking.RoomId, booking.RoomId);
            Assert.AreEqual(addedBooking.UserId, booking.UserId);
        }

        [Test]
        public async Task TestDeleteBooking()
        {
            var room = new Room { RoomId = 1, Price = 100 };
            await roomRepository.Add(room);

            var booking = new Booking
            {
                RoomId = room.RoomId,
                UserId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = Booking.BookingStatus.Pending
            };
            await bookingRepository.Add(booking);

            var deletedBooking = await bookingService.DeleteBooking(booking.BookingId);

            Assert.AreEqual(deletedBooking.BookingId, booking.BookingId);
        }

        [Test]
        public async Task TestGetBookingsByUserId()
        {
            var room = new Room { RoomId = 1, Price = 100 };
            await roomRepository.Add(room);

            var booking = new Booking
            {
                RoomId = room.RoomId,
                UserId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = Booking.BookingStatus.Pending
            };
            await bookingRepository.Add(booking);

            var bookings = await bookingService.GetBookingsByUserId(1);

            Assert.IsTrue(bookings.Any(b => b.BookingId == booking.BookingId));
        }

        [Test]
        public async Task TestUpdateBooking()
        {
            var room = new Room { RoomId = 1, Price = 100 };
            await roomRepository.Add(room);

            var bookingDTO = new BookingDTO
            {
                RoomId = room.RoomId,
                UserId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = Booking.BookingStatus.Pending
            };
            var booking = new Booking
            {
                BookingId = 1,
                RoomId = room.RoomId,
                UserId = 1,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                Status = bookingDTO.Status
            };
            mapper.Setup(m => m.Map<Booking>(bookingDTO)).Returns(booking);

            await bookingRepository.Add(booking);
            bookingDTO.Status = Booking.BookingStatus.Confirmed;

            var updatedBooking = await bookingService.UpdateBooking(bookingDTO);

            Assert.AreEqual(updatedBooking.Status, Booking.BookingStatus.Confirmed);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
