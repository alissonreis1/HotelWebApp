using HotelWebApp.Models;
using Microsoft.EntityFrameworkCore;
using HotelWebApp.Data;

namespace HotelWebApp.Repositorio
{
    public class HotelRepository : IHotelRepository
    {
        private HotelContext _context;

        public HotelRepository(HotelContext context)
        {
            _context = context;
        }
        public void AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void DeleteHotel(int id)
        {
            var hotel = _context.Hotels.Find(id);
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }

        public Hotel GetHotelById(int id)
        {
            return _context.Hotels.Include(h => h.Quartos).FirstOrDefault(h => h.Id == id);
        }

        public IEnumerable<Hotel> GetHotels()
        {
            return _context.Hotels.Include(h => h.Quartos).ToList();
        }

        public void UpdateHotel(Hotel hotel)
        {
            var existingHotel = _context.Hotels.Find(hotel.Id);

            if (existingHotel == null)
            {
                throw new InvalidOperationException("O hotel não foi encontrado.");
            }

            _context.Entry(existingHotel).CurrentValues.SetValues(hotel);
            existingHotel.RowVersion = hotel.RowVersion;


            _context.SaveChanges();
        }
        public void AddQuartoToHotel(int hotelId, Quarto quarto)
        {
            var hotel = _context.Hotels.Include(h => h.Quartos).FirstOrDefault(h => h.Id == hotelId);

            if (hotel == null)
            {
                throw new InvalidOperationException("Hotel não encontrado.");
            }

            hotel.Quartos.Add(quarto);

            _context.SaveChanges();
        }
        public void UpdateQuarto(Quarto quarto)
        {
            var existingQuarto = _context.Quartos.Find(quarto.Id);

            if (existingQuarto == null)
            {
                throw new InvalidOperationException("O quarto não foi encontrado.");
            }

            _context.Entry(existingQuarto).CurrentValues.SetValues(quarto);
            _context.SaveChanges();
        }

        public Quarto GetQuartoById(int quartoId)
        {
            return _context.Quartos.Find(quartoId);
        }

        public void DeleteQuarto(int quartoId)
        {
            var quarto = _context.Quartos.Find(quartoId);

            if (quarto == null)
            {
                throw new InvalidOperationException("O quarto não foi encontrado.");
            }

            _context.Quartos.Remove(quarto);
            _context.SaveChanges();
        }
    }
}
