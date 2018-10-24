namespace Interview.Web.Model
{
    using System.Collections.Generic;

    using Domain.Models;
    public class UserListModel
    {
        public List<User> Users { get; set; }
        public User CreatingUser { get; set; }
    }
}