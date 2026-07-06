namespace Consumer
{
    public class PersonModel
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Nome: {Name}, Idade {Age}";
        }
    }
}