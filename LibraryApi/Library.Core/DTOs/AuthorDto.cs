#nullable disable

namespace Library.Core.DTOs
{
    public partial class AuthorDto
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
