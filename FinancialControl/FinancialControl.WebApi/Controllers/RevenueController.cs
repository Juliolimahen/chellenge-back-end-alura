﻿using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Revenue;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    /// <summary>
    /// End point responsavel por buscar todas as receitas cadastradas, sendo possivel buscar por uma descrição especifica.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromQuery] string? description)
    {
        var response = await _revenueService.GetRevenuesAsync(description);

        return response.Success && response.Data.Any()
            ? Ok(response)
            : NotFound(new ResponseDto<IEnumerable<RevenueDto>>
            {
                Success = false,
                Erros = new List<string> { "No revenues found for this description." }
            });
    }

    /// <summary>
    /// End point responsavel por buscar uma receita por Id. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetReceita")]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _revenueService.GetRevenueByIdAsync(id);

        if (response.Success && response.Data != null)
        {
            return Ok(response);
        }

        return NotFound(new ResponseDto<RevenueDto>
        {
            Success = false,
            Erros = new List<string> { "Revenue not found for this Id." }
        });
    }

    /// <summary>
    /// End point responsavel por casdatrar um nova receita. 
    /// </summary>
    /// <param name="expenseDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRevenueDto revenueDto)
    {
        if (revenueDto == null)
        {
            return BadRequest(new ResponseDto<RevenueDto>
            {
                Success = false,
                Erros = new List<string> { "Invalid revenue data. Please provide valid data." }
            });
        }

        var response = await _revenueService.CreateRevenueAsync(revenueDto);

        return response.Success ? CreatedAtAction("GetById", new { id = response.Data.Id }, response) : BadRequest(response);
    }

    /// <summary>
    /// End point responsavel por editar uma receita. 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expenseDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] RevenueDto revenueDto)
    {
        if (revenueDto == null || id != revenueDto.Id)
        {
            return BadRequest(new ResponseDto<RevenueDto>
            {
                Success = false,
                Erros = new List<string> { "Invalid revenue data. Please provide valid data." }
            });
        }

        var response = await _revenueService.UpdateRevenueAsync(revenueDto);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// End point responsavel por deletar uma receita.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _revenueService.GetRevenueByIdAsync(id);

        if (response.Data != null)
        {
            await _revenueService.DeleteRevenueAsync(id);
            return NoContent();
        }

        return NotFound(new ResponseDto<RevenueDto>
        {
            Success = false,
            Erros = new List<string> { "Revenue not found." }
        });
    }

    /// <summary>
    /// End point responsavel por buscar as receita por mês e ano.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    [HttpGet("{year}/{month}")]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<RevenueDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllRevenueByDate([FromRoute] string year, [FromRoute] string month)
    {
        var response = await _revenueService.GetRevenueByDateAsync(year, month);

        return response.Success && response.Data.Any()
            ? Ok(response)
            : NotFound(new ResponseDto<IEnumerable<RevenueDto>>
            {
                Success = false,
                Erros = new List<string> { "No revenues found for this date." }
            });
    }
}
