#nullable disable

namespace Library.Core.DTOs
{
    public partial class AuthorBookDto:AuthorDto
    {
        public BookDto IdBookNavigation { get; set; }
    }
}
