using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Desenvolva o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            var hotels = _context.Hotels
                .Include(hotel => hotel.City)
                .Select(hotel => new HotelDto
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityId = hotel.CityId,
                    CityName = hotel.City.Name
                });

            return hotels.ToList();
        }

        // 5. Desenvolva o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            var city = _context.Cities.First(c => c.CityId == hotel.CityId);

            var newHotel = new Hotel
            {
                Name = hotel.Name,
                Address = hotel.Address,
                CityId = hotel.CityId
            };

            _context.Hotels.Add(newHotel);
            _context.SaveChanges();

            return new HotelDto
            {
                HotelId = newHotel.HotelId,
                Name = newHotel.Name,
                Address = newHotel.Address,
                CityId = newHotel.CityId,
                CityName = city.Name
            };
        }
    }
}