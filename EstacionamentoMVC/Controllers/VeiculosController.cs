using Microsoft.AspNetCore.Mvc;
using EstacionamentoMVC.Data;
using EstacionamentoMVC.Models;
using System.Linq;
using System.Linq.Expressions;

namespace EstacionamentoMVC.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly EstacionamentoContext _context;

        public VeiculosController(EstacionamentoContext context)
        {
            _context = context;
        }

        // GET: /Veiculos
        public IActionResult Index()
        {
            var veiculos = _context.Veiculos.ToList();
            return View(veiculos);
        }

        // GET: /Veiculos/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: /Veiculos/Criar
        [HttpPost]
        public IActionResult Criar(Veiculo v)
        {
            if (ModelState.IsValid)
            {
                v.Placa = v.Placa.ToUpper();
                v.HoraEntrada = DateTime.Now;
                _context.Add(v);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Veículo registrado com sucesso!";
                return RedirectToAction("Index");
            }
             TempData["MensagemErro"] = "Erro ao registrar o veículo. Verifique os dados.";
                return View(v);
             }
        
        // GET: /Veiculos/Saida/id
        public IActionResult Saida(int id)
        {
            var v = _context.Veiculos.Find(id);

            if (v != null && v.HoraSaida == null)
            {
                v.HoraSaida = DateTime.Now;
                _context.Update(v);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Saída registrada com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Veículo não encontrado ou já foi retirado.";
            }

            return RedirectToAction("Index");
        }

        // GET: /Veiculos/Excluir/id
        public IActionResult Excluir(int id)
        {
            var veiculo = _context.Veiculos.Find(id);

            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Veículo excluído com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Veículo não encontrado para exclusão.";
            }

            return RedirectToAction("Index");
        }
        // GET: /Veiculos/Editar/id
        public IActionResult Editar(int id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if (veiculo == null)
            {
                TempData["MensagemErro"] = "Veículo não encontrado para edição.";
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }

        // POST: /Veiculos/Editar/id
        [HttpPost]
        public IActionResult Editar(int id, Veiculo v)
        {
            if (id != v.Id)
            {
                TempData["MensagemErro"] = "ID inválido.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    v.Placa = v.Placa.ToUpper();
                    _context.Update(v);
                    _context.SaveChanges();
                    TempData["MensagemSucesso"] = "Veículo editado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["MensagemErro"] = "Erro ao editar o veículo.";
                }
            }
            return View(v);
        }
    }
}
