using HotelWebApp.Repositorio;
using Microsoft.AspNetCore.Mvc;
using HotelWebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.EntityFrameworkCore;

namespace HotelWebApp.Controllers
{

    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public IActionResult Index()
        {
            var hotels = _hotelRepository.GetHotels();
            return View(hotels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public IActionResult Create(Hotel hotel)
        {

            _hotelRepository.AddHotel(hotel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var hotel = _hotelRepository.GetHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hotel hotel)
        {
            try
            {
                _hotelRepository.UpdateHotel(hotel);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Hotel)entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível salvar as alterações. O item foi excluído por outra pessoa.");
                }
                else
                {
                    var databaseValues = (Hotel)databaseEntry.ToObject();

                    if (databaseValues.Nome != clientValues.Nome)
                    {
                        ModelState.AddModelError("Nome", $"A edição de {clientValues.Nome} falhou. O registro foi modificado por outra pessoa.");
                    }




                    hotel.RowVersion = databaseValues.RowVersion;
                    ModelState.Remove("RowVersion");
                }

                return View(hotel);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {

            var hotel = _hotelRepository.GetHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }
        public IActionResult Delete(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _hotelRepository.DeleteHotel(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }

        }
        [HttpGet]
        public IActionResult AdicionarQuarto(int hotelId)
        {
            
            ViewBag.HotelId = hotelId;
            return View();
        }

        [HttpPost]
        public IActionResult AdicionarQuarto(int hotelId, Quarto quarto)
        {
            try
            {
                
                _hotelRepository.AddQuartoToHotel(hotelId, quarto);

                
                return RedirectToAction("Details", "Hotel", new { id = hotelId });
            }
            catch (Exception ex)
            {
                
                ViewBag.ErrorMessage = "Erro ao adicionar quarto: " + ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult EditQuarto(int id)
        {
            var quarto = _hotelRepository.GetQuartoById(id);

            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQuarto(Quarto quarto)
        {
            try
            {
                _hotelRepository.UpdateQuarto(quarto);
                return RedirectToAction("Details", new { id = quarto.HotelId }); 
            }
            catch (Exception ex)
            {
                
                ViewBag.ErrorMessage = "Erro ao editar quarto: " + ex.Message;
                return View(quarto);
            }
        }

        [HttpGet]
        public IActionResult DetailsQuarto(int id)
        {
            var quarto = _hotelRepository.GetQuartoById(id);

            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        [HttpGet]
        public IActionResult DeleteQuarto(int id)
        {
            var quarto = _hotelRepository.GetQuartoById(id);

            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        [HttpPost, ActionName("DeleteQuarto")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQuartoConfirmed(int id)
        {
            try
            {
                _hotelRepository.DeleteQuarto(id);
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                ViewBag.ErrorMessage = "Erro ao excluir quarto: " + ex.Message;
                return View("DeleteQuarto", _hotelRepository.GetQuartoById(id));
            }
        }




    }
}


