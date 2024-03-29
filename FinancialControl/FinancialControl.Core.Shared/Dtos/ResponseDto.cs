﻿namespace FinancialControl.Core.Shared.Dtos;

public class ResponseDto<TDto> where TDto : class
{
    public bool Success { get; set; } = true;
    public TDto? Data { get; set; }
    public List<string> Erros { get; set; } = new();

    public ResponseDto()
    {
    }

    public ResponseDto(bool success, TDto? data, List<string> erros)
    {
        Success = success;
        Data = data;
        Erros = erros;
    }
}
