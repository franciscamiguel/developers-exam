namespace Infrastructure.Interfaces;

public interface IEmailService
{
    void SendEmail(string to, string message);   
}