using Bank.Transfers.Data;
using Bank.Transfers.Models;
using Bank.Transfers.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Transfers.Controllers
{
    public class ContaController : Controller
    {
        private readonly ContaContext _context;

        public ContaController(ContaContext context)
        {
            _context = context;
        }

        public async Task<ViewResult> Index()
            =>  View(await _context.Conta.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            
            var conta = await _context.Conta.FirstOrDefaultAsync(m => m.Id == id);

            if (conta == null)
                return NotFound();
            
            return View(conta);
        }

        public ViewResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoConta,Saldo,Credito,Nome")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conta);
        }

        public ViewResult Mensagem(string mensagem)
            => View(new MensagemViewModel { Mensagem = mensagem });

        public ViewResult Sacar(int id)
            => View(new SacarViewModel { IdConta = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sacar(int id, [Bind("IdConta,ValorSaque")] SacarViewModel model)
        {
            if (id != model.IdConta)
                return NotFound();

            try
            {
                var conta = await _context.Conta.FindAsync(id);
                var (operacao, mensagem) = conta.Sacar(model.ValorSaque);

                if (operacao)
                {
                    _context.Update(conta);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(actionName: "Mensagem", routeValues: new { mensagem });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public ViewResult Depositar(int id)
            => View(new DepositarViewModel { IdConta = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Depositar(int id, [Bind("IdConta,ValorDeposito")] DepositarViewModel model)
        {
            if (id != model.IdConta)
                return NotFound();

            try
            {
                var conta = await _context.Conta.FindAsync(id);
                string mensagem = conta.Depositar(model.ValorDeposito);

                _context.Update(conta);
                await _context.SaveChangesAsync();

                return RedirectToAction(actionName: "Mensagem", routeValues: new { mensagem });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<ViewResult> Transferir(int id)
        {
            var contas = await _context.Conta.ToListAsync();

            var model = new TransferirViewModel 
            { 
                Contas = new SelectList
                    (
                        contas.Where(c => c.Id != id),
                        "Id",
                        "Nome"
                    ),
                ContaOrigemId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transferir(int id, [Bind("ContaDestinoId,ValorTransferencia")] TransferirViewModel model)
        {
            try
            {
                var contaOrigem = await _context.Conta.FindAsync(id);
                var contaDestino = await _context.Conta.FindAsync(model.ContaDestinoId);

                contaOrigem.Transferir(model.ValorTransferencia, contaDestino);

                _context.Update(contaOrigem);
                _context.Update(contaDestino);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}