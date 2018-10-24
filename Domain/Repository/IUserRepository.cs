namespace Interview.Domain.Repository
{
    using System.Collections.Generic;
    using Interview.Domain.Models;

    public interface IUserRepository
    {
        User GetById(long id);
        User GetByUsername(string userName);
        void Save(User user);
        List<User> All();
    }
}
