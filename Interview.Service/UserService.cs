namespace Interview.Service
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Interview.Domain.Models;
    using Interview.Domain.Repository;
    using Interview.Domain.Service;
    using Interview.Domain.Service.Dto;
    using Interview.Repository;

    public class UserService : IUserService
    {
        private IUserRepository _repository;
        
        public UserService() : this(new UserRepository())
        {
        }

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Response Save(User user)
        {
            //Place Validation logic here
            //Check username is between 3-30 characters and make sure the username is unique

            //return response if username fails business rules

            try
            {
                if (String.IsNullOrEmpty(user.Username))
                {
                    throw new Exception("username cannot be blank");
                }
                //1.validation check user name characters length
                int maxChar = Convert.ToInt16(ConfigurationManager.AppSettings["MaxCharacters"]);
                int minChar = Convert.ToInt16(ConfigurationManager.AppSettings["MinCharacters"]);
                if (user.Username.Trim().Length >= maxChar || user.Username.Trim().Length < minChar)
                {
                    throw new Exception("username should be between 3 and 30"); 
                }
                //2.check username should be unique
                List<User> users = FindAll();
                
                foreach (var item in users)
                {
                    if (item.Username.ToUpper() == user.Username.Trim().ToUpper())
                    {
                        throw new Exception("username is already exist");
                    }
                }

                //we can also check by sending username to database
                //var result= _repository.GetByUsername(user.Username.Trim().ToUpper());


                //first name cannot be empty
                if (String.IsNullOrEmpty(user.Firstname) || user.Firstname.Trim().Length == 0)
                {
                    throw new Exception("Firstname cannot be blank");
                }

                //last name cannot be empty
                if (String.IsNullOrEmpty(user.Lastname) || user.Lastname.Trim().Length == 0)
                {
                    throw new Exception("Lastname cannot be blank");
                }
                _repository.Save(user);
            }
            catch (Exception ex)
            {
                return new Response
                           {
                               Success = false,
                               Errors = new List<ValidationError>()
                                            {
                                                new ValidationError { ErrorMessage = ex.Message }
                                            }
                           };
            }

            return new Response()
            {
                Success = true
            };
        }

        public List<User> FindAll()
        {
            return _repository.All();
        }

        public User FindBy(int id)
        {
            return null;
        }
    }
}
