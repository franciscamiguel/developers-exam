using Domain.Dtos;

namespace Domain.Entities
{
    public class User : Entity<User>
    {

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public int Age { get; set; }

        protected User()
        {

        }

        public User(string name, string surname, string email, string login, string password, int age)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Password = password;
            Age = age;
        }

        public void Udate(UserUpdateDto user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Login = user.Login;
            Password = user.Password;
            Age = user.Age;
        }

        public override bool IsValid()
            => Validate(this).IsValid;
    }
}
