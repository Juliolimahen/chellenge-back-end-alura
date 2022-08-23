using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControlIntregrationTests.Service
{
    public class FinancialControlRepository : IFinancialControlRepository
    {

        private List<Expense> expenses = new List<Expense>()
        {
            new Expense
            {
                Description="Tenis",
                Value=200,
                Date=DateTime.Now,
                Category=Category.Outras
            },
             new Expense
            {
                Description="Livro",
                Value=300,
                Date=DateTime.Now,
                Category=Category.Educacao
            }
        };

        private List<Revenue> revenues = new List<Revenue>()
        {
            new Revenue
            {
                Description="Salário",
                Date=DateTime.Now,
                Value=2500
            },
            new Revenue
            {
                Description="Hora Extra",
                Date=DateTime.Now,
                Value=200
            }
        };

        public List<Expense> Expenses { get => expenses; }
        public List<Revenue> Revenues { get => revenues; }

        public bool AdicionarDespesa(Expense expense)
        {
            try
            {
                this.Expenses.Add(expense);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool AdicionarReceita(Revenue revenue)
        {
            try
            {
                this.Revenues.Add(revenue);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Expense> BuscarDespesas()
        {
            return this.Expenses;
        }

        public List<Revenue> BuscarReceitas()
        {
            return this.Revenues;
        }
    }
}
