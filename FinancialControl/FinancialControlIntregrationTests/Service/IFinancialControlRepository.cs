using FinancialControl.Dtos;
using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControlIntregrationTests.Service
{
    public interface IFinancialControlRepository
    {
        public List<Revenue> BuscarReceitas();
        public List<Expense> BuscarDespesas();
    }
}
