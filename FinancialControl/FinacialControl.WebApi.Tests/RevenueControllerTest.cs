using FinancialControl.Core.Shared.Dtos.Revenue;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Manager.Services.Interface;
using FinancialControl.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using FluentAssertions;
using FinancialControl.FakeData.ExpenseData;
using FinancialControl.Core.Shared.Dtos.Expense;

namespace FinacialControl.WebApi.Tests
{
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
        public async Task PostRevenues_Created()
        {
            // Arrange
            var test = _manager.CreateRevenueAsync(Arg.Any<CreateRevenueDto>()).Returns(new ResponseDto<RevenueDto> { Data = _revenueDto.CloneTipado() });

            // Act
            var result = (ObjectResult)await _controller.Create(_createRevenueDto);

            // Assert
            await _manager.Received().CreateRevenueAsync(Arg.Any<CreateRevenueDto>());
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
            //result.Value.Should().BeEquivalentTo(_revenueDto);
        }

        [Fact]
        public async Task GetAllRevenues_Ok()
        {
            // Arrange
            //var expectedRevenues = new List<RevenueDto> { new RevenueDto(), new RevenueDto() };
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
        public async Task GetById_ExistingRevenue_Ok()
        {
            // Arrange
            var id = 1; // Set the desired ID
            _manager.GetRevenueByIdAsync(id).Returns(_revenueDto);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

            var returnedRevenue = Assert.IsType<RevenueDto>(okObjectResult.Value);
            returnedRevenue.Should().BeEquivalentTo(_revenueDto);
        }

        [Fact]
        public async Task Update_ExistingRevenue_Ok()
        {
            // Arrange
            _manager.UpdateRevenueAsync(Arg.Any<RevenueDto>()).Returns(new ResponseDto<RevenueDto> { Data = _revenueDto.CloneTipado() });

            // Act
            var result = await _controller.Update(_revenueDto.Id, _revenueDto);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

            var returnedUpdatedRevenue = Assert.IsType<RevenueDto>(okObjectResult.Value);
            returnedUpdatedRevenue.Should().BeEquivalentTo(_revenueDto);
        }

        [Fact]
        public async Task Update_NonExistingRevenue_BadRequest()
        {
            // Arrange
            var id = 1; // Set a non-existing ID
            var updatedRevenueDto = new UpdateRevenueDto { /* Define properties for the updated object */ };
            _manager.UpdateRevenueAsync(Arg.Any<UpdateRevenueDto>()).Returns(new ResponseDto<RevenueDto> { Success = false });

            // Act
            var result = await _controller.Update(id, updatedRevenueDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = (BadRequestObjectResult)result;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetById_NonExistingRevenue_NotFound()
        {
            // Arrange
            var id = 1; // Set a non-existing ID
            _manager.GetRevenueByIdAsync(id).Returns((RevenueDto)null);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = (NotFoundObjectResult)result;
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
        [Fact]
        public async Task Delete_ExistingRevenue_Ok()
        {
            // Arrange
            var id = 1; // Defina o ID desejado para uma despesa existente
            var revenueDto = new RevenueDto { Id = id }; // Crie um objeto de despesa com o ID
            _manager.GetRevenueByIdAsync(id).Returns(revenueDto);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

            var returnedRevenue = Assert.IsType<RevenueDto>(okObjectResult.Value);
            returnedRevenue.Should().BeEquivalentTo(revenueDto);
        }

        [Fact]
        public async Task Delete_NonExistingRevenue_NotFound()
        {
            // Arrange
            var id = 1; // Defina um ID que não existe
            _manager.GetRevenueByIdAsync(id).Returns((RevenueDto)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = (NotFoundObjectResult)result;
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetAllRevenueByDate_RevenuesFound_Ok()
        {
            // Arrange
            var year = "2023"; // Defina o ano desejado
            var month = "10"; // Defina o mês desejado
            var expectedRevenues = new List<RevenueDto> { new RevenueDto(), new RevenueDto() };
            _manager.GetRevenueByDateAsync(year, month).Returns(new ResponseDto<IEnumerable<RevenueDto>> { Data = expectedRevenues });

            // Act
            var result = await _controller.GetAllRevenueByDate(year, month);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);

            var responseDto = Assert.IsType<ResponseDto<IEnumerable<RevenueDto>>>(okObjectResult.Value);
            var returnedRevenues = responseDto.Data.ToList();
            returnedRevenues.Should().BeEquivalentTo(expectedRevenues);
        }

        [Fact]
        public async Task GetAllExpenseByDate_NoExpensesFound_NotFound()
        {
            // Arrange
            var year = "2023"; // Defina o ano desejado
            var month = "10"; // Defina o mês desejado
            _manager.GetRevenueByDateAsync(year, month).Returns((ResponseDto<IEnumerable<RevenueDto>>)null);

            // Act
            var result = await _controller.GetAllRevenueByDate(year, month);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = (NotFoundObjectResult)result;
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
