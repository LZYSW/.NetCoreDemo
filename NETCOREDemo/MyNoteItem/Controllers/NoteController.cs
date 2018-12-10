using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNoteItem.Models;
using MyNoteItem.Repository;
using MyNoteItem.ViewModels;

namespace MyNoteItem.Controllers
{
    public class NoteController : Controller
    {
        private INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public async Task<IActionResult> Index()
        {
            var  notes = await _noteRepository.ListAsync();
            return View(notes);
        }
        public async Task<IActionResult> Add(NoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _noteRepository.AddAsync(new Note{
                Title=model.Title,
                Content=model.Content,
                Create=DateTime.Now
            });
            return RedirectToAction("Index");
        }
        
    }
}