using AutoMapper;
using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.Data.Repositories.Interface;
using FinancialControl.Manager.Services.Interface;

namespace FinancialControl.Manager.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpensesAsync(string? description)
    {
        var expenses = !string.IsNullOrEmpty(description)
            ? await _expenseRepository.GetAllAsync(x => x.Description.Contains(description))
            : await _expenseRepository.GetAllAsync();

        return new ResponseDto<IEnumerable<ExpenseDto>>
        {
            Data = _mapper.Map<IEnumerable<ExpenseDto>>(expenses)
        };
    }

    public async Task<ResponseDto<ExpenseDto>> GetExpenseByIdAsync(int id)
    {
        var expenseEntity = await _expenseRepository.GetByIdAsync(id);
        return _mapper.Map<ResponseDto<ExpenseDto>>(expenseEntity);
    }

    public async Task<ResponseDto<ExpenseDto>> CreateExpenseAsync(CreateExpenseDto expenseDto)
    {
        var existingExpense = await _expenseRepository.FirstOrDefaultAsync(x =>
            x.Description == expenseDto.Description
            && x.Date.Month == expenseDto.Date.Month
            && x.Date.Year == expenseDto.Date.Year);

        if (existingExpense != null)
        {
            return new ResponseDto<ExpenseDto>
            {
                Success = false,
                Erros = new List<string>
                {
                    $"There is already an expense with the description {expenseDto.Description} for the date {expenseDto.Date.Month}/{expenseDto.Date.Year}"
                }
            };
        }

        var expenseEntity = _mapper.Map<Expense>(expenseDto);
        var expense = await _expenseRepository.CreateAsync(expenseEntity);
        expenseDto.Id = expenseEntity.Id;
        return new ResponseDto<ExpenseDto>();
    }

    public async Task<ResponseDto<ExpenseDto>> UpdateExpenseAsync(ExpenseDto expenseDto)
    {
        var existingExpense = await _expenseRepository.FirstOrDefaultAsync(x =>
            x.Description == expenseDto.Description
            && x.Date.Month == expenseDto.Date.Month
            && x.Date.Year == expenseDto.Date.Year);

        if (existingExpense != null)
        {
            return new ResponseDto<ExpenseDto>
            {
                Success = false,
                Erros = new List<string>
                {
                    $"There is already an expense with the description {expenseDto.Description} for the date {expenseDto.Date.Month}/{expenseDto.Date.Year}"
                }
            };
        }

        var expenseEntity = _mapper.Map<Expense>(expenseDto);
        await _expenseRepository.UpdateAsync(expenseEntity);
        return new ResponseDto<ExpenseDto>();
    }

    public async Task DeleteExpenseAsync(int id)
    {
        var expenseEntity = await _expenseRepository.GetByIdAsync(id);
        await _expenseRepository.DeleteAsync(expenseEntity.Id);
    }

    public async Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpenseByDateAsync(string year, string month)
    {
        var expenses = await _expenseRepository.GetAllAsync(x =>
            x.Date.Year.ToString() == year && x.Date.Month.ToString() == month);

        return new ResponseDto<IEnumerable<ExpenseDto>>
        {
            Data = _mapper.Map<IEnumerable<ExpenseDto>>(expenses)
        };
    }
}
