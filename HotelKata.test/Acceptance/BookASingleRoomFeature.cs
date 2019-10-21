using System;
using System.Collections.Generic;
using HotelKata.Booking;
using HotelKata.BookingPolicy;
using HotelKata.Company;
using HotelKata.Hotel;
using HotelKata.Room;
using Moq;
using Xunit;
using static HotelKata.Room.RoomType;
using static HotelKata.test.Acceptance.HotelBuilder;
using static HotelKata.test.Unit.BookingBuilder;
// ReSharper disable InconsistentNaming

namespace HotelKata.test.Acceptance
{
    public class BookASingleRoomFeature
    {
        private readonly ProductionHotelService hotelService;
        private readonly BookingRepository bookingRepository  = new InMemoryBookingRepository();
        private readonly BookingPolicyService bookingPolicyService;
        private readonly Guid employeeId = Guid.NewGuid();
        private static readonly DateTime Oct12th = DateTime.Parse("12/10/2019");
        private static readonly DateTime Oct19th = DateTime.Parse("19/10/2019");
        private static readonly DateTime Oct26th = DateTime.Parse("26/10/2019");
        private readonly BookingService bookingService;
        private readonly Mock<IdGenerator> mockIdGenerator = new Mock<IdGenerator>();
        private readonly IdGenerator productionIdGenerator = new ProductionIdGenerator();
        private readonly EmployeeRepository inMemoryEmployeeRepository = new InMemoryEmployeeRepository();
        private readonly BookingService bookingServiceWithStubbedIdGenerator;
        private readonly ProductionCompanyService companyService;
        private readonly Guid hotelId = Guid.NewGuid();

        public BookASingleRoomFeature()
        {
            companyService = new ProductionCompanyService(inMemoryEmployeeRepository);
            var bookingPolicyRepository = new InMemoryBookingPolicyRepository();
            bookingPolicyService = new ProductionBookingPolicyService(bookingPolicyRepository, companyService);
            HotelRepository hotelRepository = new InMemoryHotelRepository();
            hotelService = new ProductionHotelService(hotelRepository);
            bookingService = new BookingService(hotelService, bookingRepository, bookingPolicyService, productionIdGenerator);
            bookingServiceWithStubbedIdGenerator = new BookingService(hotelService, bookingRepository, bookingPolicyService, mockIdGenerator.Object);
        }

        [Fact]
        public void TheOneWhereISuccessfullyBookASingleRoom()
        {
            RegisterAHotel()
                .WithId(hotelId)
                .WithAStandardRoomAt(101)
                .To(hotelService);
            
            var bookingId = Guid.NewGuid();
            mockIdGenerator.Setup(it => it.GenerateId()).Returns(bookingId);

            var expectedBooking = aBooking()
                                    .WithId(bookingId)
                                    .WithEmployeeId(employeeId)
                                    .WithHotelId(hotelId)
                                    .From(Oct12th)
                                    .To(Oct19th)
                                    .Build();
            var actualBooking = bookingServiceWithStubbedIdGenerator.Book(employeeId, hotelId, Standard, Oct12th, Oct19th);
            
            Assert.Equal(expectedBooking, actualBooking);
        }

        [Fact]
        public void TheOneWhereThereAreNoRoomsAvailable()
        {
            RegisterAHotel()
                .WithId(hotelId)
                .WithAStandardRoomAt(101)
                .To(hotelService);
            
            bookingService.Book(employeeId, hotelId, Standard, Oct12th, Oct19th);

            Assert.Throws<RoomUnavailable>(() =>bookingService.Book(employeeId, hotelId, Standard, Oct12th, Oct19th));
        }

        [Fact]
        public void TheOneWhereTheEmployeeDoesNotHaveSufficientPrivilege()
        {
            RegisterAHotel()
                .WithId(hotelId)
                .WithAStandardRoomAt(101)
                .WithAMasterRoomAt(501)
                .To(hotelService);
            
            bookingPolicyService.SetEmployeePolicy(employeeId, new List<RoomType>() {Standard});

            Assert.Throws<InsufficientPrivilege>(() =>
                bookingService.Book(employeeId, hotelId, Master, Oct12th, Oct19th));
        }

        [Fact]
        public void TheOneWhereIMustCheckOutAtLeastOneDayAfterCheckingIn()
        {
            RegisterAHotel()
                .WithId(hotelId)
                .WithAStandardRoomAt(101)
                .To(hotelService);
            
            Assert.Throws<CheckoutDateInvalid>(() =>
                bookingService.Book(employeeId, hotelId, Master, Oct12th, Oct12th));
        }

        [Fact]
        public void TheOneWhereARoomIsBookedConsecutively()
        {
            RegisterAHotel()
                .WithId(hotelId)
                .WithAStandardRoomAt(101)
                .To(hotelService);
            
            bookingService.Book(employeeId, hotelId, Standard, Oct12th, Oct19th);
            bookingService.Book(employeeId, hotelId, Standard, Oct19th, Oct26th);
        }

        [Fact]
        public void TheOneWhereIdenticalBookingsAreNotTreatedAsASingleBooking()
        {
            RegisterAHotel()
                .WithId(hotelId)
                .WithAStandardRoomAt(101)
                .WithAStandardRoomAt(102)
                .To(hotelService);
            
            var firstBooking = bookingService.Book(employeeId, hotelId, Standard, Oct12th, Oct19th);
            var secondBooking = bookingService.Book(employeeId, hotelId, Standard, Oct12th, Oct19th);
            
            Assert.NotEqual(firstBooking, secondBooking);
        }

        [Fact]
        public void TheOneWhereMyBookingIsRejectedByTheCompanyPolicy()
        {
            RegisterAHotel()
                .WithId(hotelId)
                .WithAStandardRoomAt(101)
                .WithAMasterRoomAt(501)
                .To(hotelService);

            var companyId = Guid.NewGuid();
            companyService.AddEmployee(companyId, employeeId);
            bookingPolicyService.SetCompanyPolicy(companyId, new List<RoomType> {Standard});
            
            Assert.Throws<InsufficientPrivilege>(() =>
                bookingService.Book(employeeId, hotelId, Master, Oct12th, Oct19th));
        }
    }
}