using DisasterAlleviation_Foundation.Models;

internal interface IUserRepository
{
    void AddUser(ApplicationUser model);
    ApplicationUser GetUserByEmail(string email);
}