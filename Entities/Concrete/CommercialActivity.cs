using Entities.Abstract;


namespace Entities.Concrete
{
    public class CommercialActivity:IEntity
    {
        public int Id { get; set; }
        public string Job { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
