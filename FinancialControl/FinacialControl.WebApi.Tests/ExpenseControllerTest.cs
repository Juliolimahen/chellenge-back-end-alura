using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.FakeData.ExpenseData;
using FinancialControl.Manager.Services.Interface;
using FinancialControl.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

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
    public async Task PostExpenses_Created()
    {
        // Arrange
        var test = _manager.CreateExpenseAsync(Arg.Any<CreateExpenseDto>()).Returns(new ResponseDto<ExpenseDto> { Data = _expenseDto.CloneTipado() });

        // Act
        var result = (ObjectResult)await _controller.Create(_createExpenseDto);

        // Assert
        await _manager.Received().CreateExpenseAsync(Arg.Any<CreateExpenseDto>());
        result.StatusCode.Should().Be(StatusCodes.Status201Created);
        //result.Value.Should().BeEquivalentTo(_expenseDto);
    }

    [Fact]
    public async Task GetAllExpenses_Ok()
    {
        // Arrange
        //var expectedExpenses = new List<ExpenseDto> { new ExpenseDto(), new ExpenseDto() };
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
    public async Task GetById_ExistingExpense_Ok()
    {
        // Arrange
        var id = 1; // Set the desired ID
        _manager.GetExpenseByIdAsync(id).Returns(_expenseDto);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedExpense = Assert.IsType<ExpenseDto>(okObjectResult.Value);
        returnedExpense.Should().BeEquivalentTo(_expenseDto);
    }

    [Fact]
    public async Task Update_ExistingExpense_Ok()
    {
        // Arrange
        _manager.UpdateExpenseAsync(Arg.Any<ExpenseDto>()).Returns(new ResponseDto<ExpenseDto> { Data = _expenseDto.CloneTipado() });

        // Act
        var result = await _controller.Update(_expenseDto.Id, _expenseDto);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedUpdatedExpense = Assert.IsType<ExpenseDto>(okObjectResult.Value);
        returnedUpdatedExpense.Should().BeEquivalentTo(_expenseDto);
    }

    [Fact]
    public async Task Update_NonExistingExpense_BadRequest()
    {
        // Arrange
        var id = 1; // Set a non-existing ID
        var updatedExpenseDto = new UpdateExpenseDto { /* Define properties for the updated object */ };
        _manager.UpdateExpenseAsync(Arg.Any<UpdateExpenseDto>()).Returns(new ResponseDto<ExpenseDto> { Success = false });

        // Act
        var result = await _controller.Update(id, updatedExpenseDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = (BadRequestObjectResult)result;
        badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetById_NonExistingExpense_NotFound()
    {
        // Arrange
        var id = 1; // Set a non-existing ID
        _manager.GetExpenseByIdAsync(id).Returns((ExpenseDto)null);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    [Fact]
    public async Task Delete_ExistingExpense_Ok()
    {
        // Arrange
        var id = 1; // Defina o ID desejado para uma despesa existente
        var expenseDto = new ExpenseDto { Id = id }; // Crie um objeto de despesa com o ID
        _manager.GetExpenseByIdAsync(id).Returns(expenseDto);

        // Act
        var result = await _controller.Delete(id);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedExpense = Assert.IsType<ExpenseDto>(okObjectResult.Value);
        returnedExpense.Should().BeEquivalentTo(expenseDto);
    }

    [Fact]
    public async Task Delete_NonExistingExpense_NotFound()
    {
        // Arrange
        var id = 1; // Defina um ID que não existe
        _manager.GetExpenseByIdAsync(id).Returns((ExpenseDto)null);

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetAllExpenseByDate_ExpensesFound_Ok()
    {
        // Arrange
        var year = "2023"; // Defina o ano desejado
        var month = "10"; // Defina o mês desejado
        var expectedExpenses = new List<ExpenseDto> { new ExpenseDto(), new ExpenseDto() };
        _manager.GetExpenseByDateAsync(year, month).Returns(new ResponseDto<IEnumerable<ExpenseDto>> { Data = expectedExpenses });

        // Act
        var result = await _controller.GetAllExpenseByDate(year, month);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var responseDto = Assert.IsType<ResponseDto<IEnumerable<ExpenseDto>>>(okObjectResult.Value);
        var returnedExpenses = responseDto.Data.ToList();
        returnedExpenses.Should().BeEquivalentTo(expectedExpenses);
    }

    [Fact]
    public async Task GetAllExpenseByDate_NoExpensesFound_NotFound()
    {
        // Arrange
        var year = "2023"; // Defina o ano desejado
        var month = "10"; // Defina o mês desejado
        _manager.GetExpenseByDateAsync(year, month).Returns((ResponseDto<IEnumerable<ExpenseDto>>)null);

        // Act
        var result = await _controller.GetAllExpenseByDate(year, month);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
