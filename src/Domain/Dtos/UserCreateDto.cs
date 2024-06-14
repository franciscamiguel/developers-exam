namespace Domain.Dtos;

public class UserCreateDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
}

public class UserUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
}

public class UserDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public long Idade { get; set; }
}
