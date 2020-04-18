using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.Services.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private ServiceProvider _serviceProvider;
        private Mock<IUserRepository> _userRepositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _serviceProvider = ServiceTestHelper.InitProvider(services =>
            {
                _userRepositoryMock = new Mock<IUserRepository>();
                services.AddTransient<IUserRepository>((serviceProvicer) => _userRepositoryMock.Object);
            });
        }

        [TestMethod]
        public async Task Login__When_wrong_password__Fails()
        {
            // arrange
            var user = new User
            {
                Username = "user",
                PasswordHash = "pILjMJ05xUXMsLE5VOFlm8DZ7kXMpttTSfpxbJYE3js=",
                Salt = "OKP9sFN0K+T1psvti4rQWQ=="
            };

            _userRepositoryMock.Setup(x => x.FindByLogin(user.Username)).Returns(Task.FromResult(user)).Verifiable();

            // act
            var userService = _serviceProvider.GetService<IUserService>();
            User returnedUser = await userService.Login(user.Username, "wrong parola");

            // assert
            Assert.IsNull(returnedUser);
        }

        [TestMethod]
        public async Task Login__When_password_correct___Works()
        {
            // arrange
            var user = new User
            {
                Username = "user",
                PasswordHash = "pILjMJ05xUXMsLE5VOFlm8DZ7kXMpttTSfpxbJYE3js=",
                Salt = "OKP9sFN0K+T1psvti4rQWQ=="
            };

            _userRepositoryMock.Setup(x => x.FindByLogin(user.Username)).Returns(Task.FromResult(user)).Verifiable();

            // act
            var userService = _serviceProvider.GetService<IUserService>();
            User returnedUser = await userService.Login(user.Username, "password");

            // assert
            Assert.AreEqual(user, returnedUser);
        }
    }
}
