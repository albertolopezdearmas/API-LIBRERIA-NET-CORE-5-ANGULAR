#nullable disable

namespace Library.Core.Entities
{
    public partial class Author : BaseEntity
    {
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Book IdBookNavigation { get; set; }
    }
}
