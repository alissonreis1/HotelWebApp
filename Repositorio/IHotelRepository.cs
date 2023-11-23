using HotelWebApp.Models;
using HotelWebApp.Data;

namespace HotelWebApp.Repositorio
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetHotels();
        Hotel GetHotelById(int id);
        void AddHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
        void DeleteHotel(int id);

        void AddQuartoToHotel(int hotelId, Quarto quarto);
        void UpdateQuarto(Quarto quarto);
        Quarto GetQuartoById(int quartoId);
        void DeleteQuarto(int quartoId);
    }
}
