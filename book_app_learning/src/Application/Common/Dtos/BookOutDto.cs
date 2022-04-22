namespace Application.Common.Dtos
{
    public class BookOutDto
    {
        public string Name { get; set; }

        public DateTime PublishingDate { get; set; }

        public int Pages { get; set; }

        public string Format { get; set; }

        public IEnumerable<string> Authors { get; set; }
    }
}