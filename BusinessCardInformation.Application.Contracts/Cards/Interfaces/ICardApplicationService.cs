using BusinessCardInformation.Application.Contracts.Cards.Dto;
using BusinessCardInformation.Application.Contracts.Common;
using BusinessCardInformation.Application.Contracts.DI;
using Microsoft.AspNetCore.Http;

namespace BusinessCardInformation.Application.Contracts.Cards.Interfaces
{
    public interface ICardApplicationService : ITransient
    {
        Task<CardDto> GetAsync(Guid id);
        Task<List<CardDto>> GetListAsync();
        Task<CardDto> CreateAsync(CreateCardDto input);
        Task<CardDto> UpdateAsync(Guid id, UpdateCardDto input);
        Task DeleteAsync(Guid id);
        Task<FileResultDto> ExportAsync(string format);
        CardDto Import(IFormFile file);
        Task<CardDto> ImportFromQrCodeAsync(IFormFile file);
    }
}
