using Entities.Abstract;


namespace Entities.DTOs
{
    public class CustomerCommercialActivityDto:IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

    }
}
