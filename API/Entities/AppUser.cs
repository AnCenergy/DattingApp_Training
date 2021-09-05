namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }  // Name is crucial for entity framework to  understand that this is going to be pk
        public string UserName { get; set; } // stick with Capital "N" for Name as ASPnet core identity, that needs it to be this wya, so refactoring is less

        // To explicitiy set the property, propfull can be used

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}