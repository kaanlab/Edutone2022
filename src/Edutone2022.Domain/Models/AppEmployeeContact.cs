
namespace Edutone2022.Domain.Models
{
    public sealed class AppEmployeeContact : Entity
    {
        public string Name { get; private set; }
        public string JobTitle { get; private set; }
        public string? Responsibility { get; private set; }
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public AppFile? Image { get; private set; }

        public AppEmployeeContact(string name, string jobTitle, string? responsibility = null, string? email = null, string? phoneNumber = null, AppFile? image = null) 
        {
            InitFill();

            Name = name;
            JobTitle = jobTitle;
            Responsibility = responsibility;
            Email = email;
            PhoneNumber = phoneNumber;
            Image = image;
        }

        public void Update(string name, string? jobTitle = null, string? responsibility = null, string? email = null, string? phoneNumber = null, AppFile? image = null)
        {
            Name = name;
            JobTitle = jobTitle ?? JobTitle;
            Responsibility = responsibility ?? Responsibility;
            Email = email ?? Email;
            PhoneNumber = phoneNumber ?? PhoneNumber;
            Image = image ?? Image;
        }
    }
}
