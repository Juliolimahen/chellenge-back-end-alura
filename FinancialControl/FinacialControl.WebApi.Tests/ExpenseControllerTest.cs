using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.WebApi.Controllers;
using FinancialControl.FakeData.ExpenseData;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.Manager.Services.Interface;

namespace FinacialControl.WebApi.Tests;

public class ExpenseControllerTest
{
    private readonly IExpenseService _manager;
    private readonly ExpenseController _controller;
    private readonly ExpenseDto _expenseDto;
    private readonly List<ExpenseDto> _listExpenseDto;
    private readonly CreateExpenseDto _createExpenseDto;

    public ExpenseControllerTest()
    {
        _manager = Substitute.For<IExpenseService>();
        _controller = new ExpenseController(_manager);

        _expenseDto = new ExpenseDtoFaker().Generate();
        _listExpenseDto = new ExpenseDtoFaker().Generate(10);
        _createExpenseDto = new CreateExpenseDtoFaker().Generate();
    }

    [Fact]
    public async Task Create_ValidExpense_ReturnsCreated()
    {
        // Arrange
        _manager.CreateExpenseAsync(Arg.Any<CreateExpenseDto>()).Returns(new ResponseDto<ExpenseDto> { Data = _expenseDto.CloneTipado() });

        // Act
        var result = (ObjectResult)await _controller.Create(_createExpenseDto);

        // Assert
        await _manager.Received().CreateExpenseAsync(Arg.Any<CreateExpenseDto>());
        result.StatusCode.Should().Be(StatusCodes.Status201Created);
        //result.Value.Should().BeEquivalentTo(_expenseDto);
    }

    [Fact]
    public async Task GetAll_WithExpenses_ReturnsOkWithExpenses()
    {
        // Arrange
        _manager.GetExpensesAsync(Arg.Any<string>()).Returns(new ResponseDto<IEnumerable<ExpenseDto>> { Data = _listExpenseDto });

        // Act
        var result = await _controller.GetAll(null);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var responseDto = Assert.IsType<ResponseDto<IEnumerable<ExpenseDto>>>(okObjectResult.Value);
        var returnedExpenses = responseDto.Data.ToList();
        returnedExpenses.Should().BeEquivalentTo(_listExpenseDto);
    }

    [Fact]
    public async Task GetById_ExistingExpense_ReturnsOkWithExpense()
    {
        // Arrange
        _manager.GetExpenseByIdAsync(_expenseDto.Id).Returns(new ResponseDto<ExpenseDto> { Data = _expenseDto });

        // Act
        var result = await _controller.GetById(_expenseDto.Id);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedResponse = Assert.IsType<ResponseDto<ExpenseDto>>(okObjectResult.Value);
        var returnedExpense = returnedResponse.Data;
        returnedExpense.Should().BeEquivalentTo(_expenseDto);
    }

    [Fact]
    public async Task Update_ExistingExpense_ReturnsOk()
    {
        // Arrange
        _manager.UpdateExpenseAsync(Arg.Any<ExpenseDto>()).Returns(new ResponseDto<ExpenseDto> { Data = _expenseDto.CloneTipado() });

        // Act
        var result = await _controller.Update(_expenseDto.Id, _expenseDto);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedResponse = Assert.IsType<ResponseDto<ExpenseDto>>(okObjectResult.Value);
        var returnedExpense = returnedResponse.Data;
        returnedExpense.Should().BeEquivalentTo(_expenseDto);
    }

    [Fact]
    public async Task Update_NonExistingExpense_ReturnsBadRequest()
    {
        // Arrange
        var id = 1; // Define um ID que não existe
        var updatedExpenseDto = new UpdateExpenseDto();
        _manager.UpdateExpenseAsync(Arg.Any<UpdateExpenseDto>()).Returns(new ResponseDto<ExpenseDto> { Success = false });

        // Act
        var result = await _controller.Update(id, updatedExpenseDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = (BadRequestObjectResult)result;
        badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetById_NonExistingExpense_ReturnsNotFound()
    {
        // Arrange
        var id = 1; // Defina um ID que não existe
        _manager.GetExpenseByIdAsync(id).Returns(new ResponseDto<ExpenseDto> { Data = null });

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Delete_ExistingExpense_ReturnsNoContent()
    {
        // Arrange
        _manager.GetExpenseByIdAsync(_expenseDto.Id).Returns(new ResponseDto<ExpenseDto> { Data = _expenseDto });

        // Act
        var result = await _controller.Delete(_expenseDto.Id);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task Delete_NonExistingExpense_ReturnsNotFound()
    {
        // Arrange
        var id = 1; // Defina um ID que não existe
        _manager.GetExpenseByIdAsync(id).Returns(new ResponseDto<ExpenseDto> { Data = null });

        // Act
        var result = await _controller.Delete(id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetAllExpenseByDate_ExpensesFound_ReturnsOkWithExpenses()
    {
        // Arrange
        var year = "2023"; // Defina o ano desejado
        var month = "10"; // Defina o mês desejado
        _manager.GetExpenseByDateAsync(year, month).Returns(new ResponseDto<IEnumerable<ExpenseDto>> { Data = _listExpenseDto });

        // Act
        var result = await _controller.GetAllExpenseByDate(year, month);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var responseDto = Assert.IsType<ResponseDto<IEnumerable<ExpenseDto>>>(okObjectResult.Value);
        var returnedExpenses = responseDto.Data.ToList();
        returnedExpenses.Should().BeEquivalentTo(_listExpenseDto);
    }

    [Fact]
    public async Task GetAllExpenseByDate_NoExpensesFound_ReturnsNotFound()
    {
        // Arrange
        var year = "2023";
        var month = "10";

        _manager.GetExpenseByDateAsync(year, month).Returns(new ResponseDto<IEnumerable<ExpenseDto>>
        {
            Data = new List<ExpenseDto>(),
            Success = false
        });

        // Act
        var result = await _controller.GetAllExpenseByDate(year, month);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
