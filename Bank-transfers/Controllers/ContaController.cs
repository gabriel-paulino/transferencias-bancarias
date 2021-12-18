﻿using Bank.Transfers.Data;
using Bank.Transfers.Models;
using Bank.Transfers.ViewModel;
using Microsoft.AspNetCore.Mvc;
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

        // GET: Conta
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conta.ToListAsync());
        }

        // GET: Conta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // GET: Conta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Conta/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var conta = await _context.Conta.FindAsync(id);
        //    if (conta == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(conta);
        //}

        // POST: Conta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,TipoConta,Saldo,Credito,Nome")] Conta conta)
        //{
        //    if (id != conta.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(conta);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContaExists(conta.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(conta);
        //}

        public async Task<ViewResult> Mensagem(string mensagem)
            => View(new MensagemViewModel { Mensagem = mensagem });
        
        // GET: Conta/Sacar/5
        public async Task<ViewResult> Sacar(int id)
        {
            var sacar = new SacarViewModel { IdConta = id };
            return View(sacar);
        }

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

                return RedirectToAction(actionName:"Mensagem", routeValues: new { mensagem } );
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // GET: Conta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST: Conta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conta = await _context.Conta.FindAsync(id);
            _context.Conta.Remove(conta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaExists(int id)
        {
            return _context.Conta.Any(e => e.Id == id);
        }
    }
}
