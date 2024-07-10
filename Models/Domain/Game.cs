namespace Game_Universe.API.Models.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public float Rating { get; set; }

        public string Platform { get; set; }

        public int Age { get; set; }

        public decimal Price { get; set; }

        public string Label { get; set; }

        public bool  IsVisible { get; set; }    



    }
}
