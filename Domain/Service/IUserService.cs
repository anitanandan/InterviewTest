namespace Interview.Domain.Service
{
    using System.Collections.Generic;

    using Interview.Domain.Models;
    using Interview.Domain.Service.Dto;

    public interface IUserService
    {
        List<User> FindAll();
        User FindBy(int id);
        Response Save(User user);
    }
}
