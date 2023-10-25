using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.WebApi.Controllers;
using FinancialControl.FakeData.RevenueData;
using FinancialControl.Manager.Services.Interface;
using FinancialControl.Core.Shared.Dtos.Revenue;

namespace FinacialControl.WebApi.Tests;

public class RevenueControllerTest
{
    private readonly IRevenueService _manager;
    private readonly RevenueController _controller;
    private readonly RevenueDto _revenueDto;
    private readonly List<RevenueDto> _listRevenueDto;
    private readonly CreateRevenueDto _createRevenueDto;

    public RevenueControllerTest()
    {
        _manager = Substitute.For<IRevenueService>();
        _controller = new RevenueController(_manager);

        _revenueDto = new RevenueDtoFaker().Generate();
        _listRevenueDto = new RevenueDtoFaker().Generate(10);
        _createRevenueDto = new CreateRevenueDtoFaker().Generate();
    }

    [Fact]
    public async Task Create_ValidRevenue_ReturnsCreated()
    {
        // Arrange
        _manager.CreateRevenueAsync(Arg.Any<CreateRevenueDto>()).Returns(new ResponseDto<RevenueDto> { Data = _revenueDto.CloneTipado() });

        // Act
        var result = (ObjectResult)await _controller.Create(_createRevenueDto);

        // Assert
        await _manager.Received().CreateRevenueAsync(Arg.Any<CreateRevenueDto>());
        result.StatusCode.Should().Be(StatusCodes.Status201Created);
        //result.Value.Should().BeEquivalentTo(_revenueDto);
    }

    [Fact]
    public async Task GetAll_WithRevenues_ReturnsOkWithRevenues()
    {
        // Arrange
        _manager.GetRevenuesAsync(Arg.Any<string>()).Returns(new ResponseDto<IEnumerable<RevenueDto>> { Data = _listRevenueDto });

        // Act
        var result = await _controller.GetAll(null);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var responseDto = Assert.IsType<ResponseDto<IEnumerable<RevenueDto>>>(okObjectResult.Value);
        var returnedRevenues = responseDto.Data.ToList();
        returnedRevenues.Should().BeEquivalentTo(_listRevenueDto);
    }

    [Fact]
    public async Task GetById_ExistingRevenue_ReturnsOkWithRevenue()
    {
        // Arrange
        _manager.GetRevenueByIdAsync(_revenueDto.Id).Returns(new ResponseDto<RevenueDto> { Data = _revenueDto });

        // Act
        var result = await _controller.GetById(_revenueDto.Id);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedResponse = Assert.IsType<ResponseDto<RevenueDto>>(okObjectResult.Value);
        var returnedRevenue = returnedResponse.Data;
        returnedRevenue.Should().BeEquivalentTo(_revenueDto);
    }

    [Fact]
    public async Task Update_ExistingRevenue_ReturnsOk()
    {
        // Arrange
        _manager.UpdateRevenueAsync(Arg.Any<RevenueDto>()).Returns(new ResponseDto<RevenueDto> { Data = _revenueDto.CloneTipado() });

        // Act
        var result = await _controller.Update(_revenueDto.Id, _revenueDto);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedResponse = Assert.IsType<ResponseDto<RevenueDto>>(okObjectResult.Value);
        var returnedRevenue = returnedResponse.Data;
        returnedRevenue.Should().BeEquivalentTo(_revenueDto);
    }

    [Fact]
    public async Task Update_NonExistingRevenue_ReturnsBadRequest()
    {
        // Arrange
        var id = 1; // Define um ID que não existe
        var updatedRevenueDto = new UpdateRevenueDto();
        _manager.UpdateRevenueAsync(Arg.Any<UpdateRevenueDto>()).Returns(new ResponseDto<RevenueDto> { Success = false });

        // Act
        var result = await _controller.Update(id, updatedRevenueDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = (BadRequestObjectResult)result;
        badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetById_NonExistingRevenue_ReturnsNotFound()
    {
        // Arrange
        var id = 1; // Defina um ID que não existe
        _manager.GetRevenueByIdAsync(id).Returns(new ResponseDto<RevenueDto> { Data = null });

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Delete_ExistingRevenue_ReturnsNoContent()
    {
        // Arrange
        _manager.GetRevenueByIdAsync(_revenueDto.Id).Returns(new ResponseDto<RevenueDto> { Data = _revenueDto });

        // Act
        var result = await _controller.Delete(_revenueDto.Id);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task Delete_NonExistingRevenue_ReturnsNotFound()
    {
        // Arrange
        var id = 1; // Defina um ID que não existe
        _manager.GetRevenueByIdAsync(id).Returns(new ResponseDto<RevenueDto> { Data = null });

        // Act
        var result = await _controller.Delete(id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetAllRevenueByDate_RevenuesFound_ReturnsOkWithRevenues()
    {
        // Arrange
        var year = "2023"; // Defina o ano desejado
        var month = "10"; // Defina o mês desejado
        _manager.GetRevenueByDateAsync(year, month).Returns(new ResponseDto<IEnumerable<RevenueDto>> { Data = _listRevenueDto });

        // Act
        var result = await _controller.GetAllRevenueByDate(year, month);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var responseDto = Assert.IsType<ResponseDto<IEnumerable<RevenueDto>>>(okObjectResult.Value);
        var returnedRevenues = responseDto.Data.ToList();
        returnedRevenues.Should().BeEquivalentTo(_listRevenueDto);
    }

    [Fact]
    public async Task GetAllRevenueByDate_NoRevenuesFound_ReturnsNotFound()
    {
        // Arrange
        var year = "2023";
        var month = "10";

        _manager.GetRevenueByDateAsync(year, month).Returns(new ResponseDto<IEnumerable<RevenueDto>>
        {
            Data = new List<RevenueDto>(),
            Success = false
        });

        // Act
        var result = await _controller.GetAllRevenueByDate(year, month);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
