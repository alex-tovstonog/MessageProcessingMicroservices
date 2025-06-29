using System.ComponentModel.DataAnnotations;

namespace MessageProcessing.Shared.Mpa.Mpapi.Dto
{
    public class MessageDto
    {
        [Required]
        public int Number { get; set; }

        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        public string? Optional { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        public override string ToString()
        {
            return $"MessageDto: Number={Number}, Text={Text}{(Optional == null ? "" : ", Optional=" + Optional)}, Date={Date}";
        }
    }
}
