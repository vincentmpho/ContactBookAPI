namespace ContactBookAPI.Models.DTOs
{
    public class AddContactRequestDTO
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public bool Favorite { get; set; }
    }
}
