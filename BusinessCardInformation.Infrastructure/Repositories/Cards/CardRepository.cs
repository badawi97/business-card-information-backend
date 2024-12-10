using BusinessCardInformation.Domain.Cards.Entities;
using BusinessCardInformation.Domain.Cards.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardInformation.Infrastructure.Repositories.Cards
{
    public class CardRepository : ICardRepository
    {
        private readonly BusinessCardInformationDbContext _context;
        public CardRepository(BusinessCardInformationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Card>> GetListAsync(
            string? name,
            DateTime? DateOfBirth,
            string? phone,
            int? gender,
            string? email,
            int skipCount,
            int maxResultCount,
            string? sorting
            )
        {
            var query = _context.Cards.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(card => card.Name != null ? card.Name.Contains(name) : true);
            }

            if (DateOfBirth.HasValue)
            {
                query = query.Where(card => card.DateOfBirth != null ? card.DateOfBirth.Value.Date == DateOfBirth.Value.Date : true);
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(card => card.Phone != null ? card.Phone.Contains(phone) : true);
            }

            if (gender.HasValue)
            {
                query = query.Where(card => card.Gender == gender.Value);
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(card => card.Email != null ? card.Email.Contains(email) : true);
            }
            query = query.Skip(skipCount).Take(maxResultCount);

            return await query.ToListAsync();

        }

        public async Task<Card> CreateAsync(Card card)
        {

            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<Card> UpdateAsync(Guid id, Card card)
        {
            var existingCard = await GetByIdAsync(id);
            existingCard.Update(card);
            _context.Cards.Update(existingCard);
            await _context.SaveChangesAsync();
            return existingCard;
        }

        public async Task DeleteAsync(Guid id)
        {
            var card = await GetByIdAsync(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Cards.CountAsync();
        }

        public async Task<Card> GetByIdAsync(Guid id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(card => card.Id == id);
            if (card == null)
            {
                throw new KeyNotFoundException($"Card with Id {id} was not found.");
            }
            return card;
        }
    }
}
