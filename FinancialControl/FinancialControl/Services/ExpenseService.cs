using AutoMapper;
using FinancialControl.Dtos;
using FinancialControl.Models;
using FinancialControl.Repositories.Interface;


namespace FinancialControl.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpenses(string? description)
    {
        ResponseDto<IEnumerable<ExpenseDto>> response = new();

        IEnumerable<Expense> expenses;

        if (!string.IsNullOrEmpty(description))
            expenses = await _expenseRepository.GetAll(x => x.Description.Contains(description));
        else
            expenses = await _expenseRepository.GetAll();

        response.Data = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);

        return response;
    }

    public async Task<ExpenseDto> GetExpenseById(int id)
    {
        var expenseEntity = await _expenseRepository.GetById(id);
        return _mapper.Map<ExpenseDto>(expenseEntity);
    }

    public async Task<ResponseDto<ExpenseDto>> CreateExpense(CreateExpenseDto expenseDto)
    {
        ResponseDto<ExpenseDto> response = new();
        #region Query validation month
        var exists = await _expenseRepository.FirstOrDefaultAsync(
            x => x.Description == expenseDto.Description
            && x.Date.Month == expenseDto.Date.Month
            && x.Date.Year == expenseDto.Date.Year);

        if (exists is not null)
        {
            response.Success = false;
            response.Erros.Add($"there is already an expense with the description {expenseDto.Description} for the date {expenseDto.Date.Month}/{expenseDto.Date.Year}");
            return response;
        }
        #endregion

        var expenseEntity = _mapper.Map<Expense>(expenseDto);
        await _expenseRepository.Create(expenseEntity);
        expenseDto.Id = expenseEntity.Id;
        return response;
    }

    public async Task<ResponseDto<ExpenseDto>> UpdateExpense(ExpenseDto expenseDto)
    {
        ResponseDto<ExpenseDto> response = new();
        #region Query validation month
        var exists = await _expenseRepository.FirstOrDefaultAsync(
            x => x.Description == expenseDto.Description
            && x.Date.Month == expenseDto.Date.Month
            && x.Date.Year == expenseDto.Date.Year);

        if (exists is not null)
        {
            response.Success = false;
            response.Erros.Add($"there is already an expense with the description {expenseDto.Description} for the date {expenseDto.Date.Month}/{expenseDto.Date.Year}");
            return response;
        }
        #endregion

        var expenseEntity = _mapper.Map<Expense>(expenseDto);
        await _expenseRepository.Update(expenseEntity);
        return response;
    }

    public async Task DeleteExpense(int id)
    {
        var expenseEntity = _expenseRepository.GetById(id).Result;
        await _expenseRepository.Delete(expenseEntity.Id);
    }

    public async Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpenseByDate(string year, string month)
    {
        ResponseDto<IEnumerable<ExpenseDto>> response = new();

        var expenses = await _expenseRepository.GetAll(x => x.Date.Year.ToString() == year && x.Date.Month.ToString() == month);
        if (!expenses.Any())
        {
            response.Success = false;
            response.Erros.Add($"No expenses found on this date {month}/{year}");
            return response;
        }

        response.Data = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
        return response;
    }
}
