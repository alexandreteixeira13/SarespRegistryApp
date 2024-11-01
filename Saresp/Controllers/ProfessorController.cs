using Microsoft.AspNetCore.Mvc;
using Saresp.Models;
using Saresp.Repository;
using Saresp.Repository.Contract;

namespace Saresp.Controllers
{
    public class ProfessorController : Controller
    {
        private IProfessorAplicadorRepository _professorRepository;

        public ProfessorController(IProfessorAplicadorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public IActionResult Index()
        {
            return View(_professorRepository.ObterTodosProfessores());
        }

        [HttpGet]
        public IActionResult CadastrarProfessor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProfessor(ProfessorAplicador professorAplicador)
        {
            if (ModelState.IsValid)
            {
                _professorRepository.Cadastrar(professorAplicador);
            }
            return View();
        }

        [HttpGet]

        public IActionResult AtualizarProfessor(int id)
        {
            return View(_professorRepository.ObterProfessor(id));
        }

        [HttpPost]

        public IActionResult AtualizarProfessor(ProfessorAplicador professorAplicador)
        {
            _professorRepository.Atualizar(professorAplicador);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExcluirProfessor(int id)
        {
            _professorRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public IActionResult DetalhesProfessor(int id)
        {
            return View(_professorRepository.ObterProfessor(id));
        }

        [HttpPost]

        public IActionResult DetalhesProfessor(ProfessorAplicador professorAplicador)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}

