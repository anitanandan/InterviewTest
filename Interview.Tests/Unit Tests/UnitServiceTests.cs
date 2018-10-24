namespace Interview.Tests.Unit_Tests
{
    using Interview.Domain.Models;
    using Interview.Service;
    using NUnit.Framework;
    using System.Configuration;

    [TestFixture]
    public class UnitServiceTests
    {
        User user = new User
        {
            Username = "AnitaNandannew",
            Firstname = "Anita",
            Lastname = "Nandan",
        };
        [Test]
        public void When_UserName_AlreadyExist_MatchErrorMsg()
        {
            user.Username = "AnitaNandan";
            UserService userservice = new UserService();
            Domain.Service.Dto.Response result = userservice.Save(user);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "username is already exist");

        }
        [Test]
        public void When_FirstName_Empty_MatchErrorMsg()
        {
            user.Firstname = "";
            UserService userservice = new UserService();
            Domain.Service.Dto.Response result = userservice.Save(user);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "Firstname cannot be blank");

        }
        [Test]
        public void When_LastName_Empty_MatchErrorMsg()
        {
            user.Firstname = "Anita";
            user.Lastname = "";
            UserService userservice = new UserService();
            Domain.Service.Dto.Response result = userservice.Save(user);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "Lastname cannot be blank");

        }
        [Test]
        public void When_UserName_lengthlessthan3_MatchErrorMsg()
        {
            user.Username = "an";
            UserService userservice = new UserService();
            Domain.Service.Dto.Response result = userservice.Save(user);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "username should be between 3 and 30");

        }
        [Test]
        public void When_UserName_lengthMorethan30_MatchErrorMsg()
        {
            user.Username = "usernamelenghtismorethanthirycharacters";
            UserService userservice = new UserService();
            Domain.Service.Dto.Response result = userservice.Save(user);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "username should be between 3 and 30");

        }
    }
}
