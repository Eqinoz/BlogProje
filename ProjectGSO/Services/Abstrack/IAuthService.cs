using ProjectGSO.Models;
using ProjectGSO.Models.DTO;

namespace ProjectGSO.Services.Abstrack
{
    public interface IAuthService
    {
        UsersDTO Login(string mail, string password);
        bool Register(string firstName, string lastName, string mail, string password);
    }
}
