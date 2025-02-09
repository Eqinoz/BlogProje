using ProjectGSO.Models;

namespace ProjectGSO.Services.Abstrack
{
    public interface IAuthService
    {
        Users Login(string mail, string password);
        bool Register(string firstName, string lastName, string mail, string password);
    }
}
