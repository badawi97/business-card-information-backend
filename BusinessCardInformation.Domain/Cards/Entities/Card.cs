
namespace BusinessCardInformation.Domain.Cards.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Photo { get; set; }
        public string? Address { get; set; }

        public Card Update(Card updatedCard)
        {
            if (!string.IsNullOrEmpty(updatedCard.Name))
            {
                Name = updatedCard.Name;
            }

            if (updatedCard.DateOfBirth.HasValue)
            {
                DateOfBirth = updatedCard.DateOfBirth.Value;
            }

            if (!string.IsNullOrEmpty(updatedCard.Phone))
            {
                Phone = updatedCard.Phone;
            }

            if (updatedCard.Gender.HasValue)
            {
                Gender = updatedCard.Gender.Value;
            }

            if (!string.IsNullOrEmpty(updatedCard.Email))
            {
                Email = updatedCard.Email;
            }
            return this;
        }
    }


}
