using System;
using Xunit;
using FakeItEasy;
using IF3250_2022_24_APPTS_Backend.Services;
using IF3250_2022_24_APPTS_Backend.Models.User;
using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace IF3250_2022_24_APPTS_Backend.Tests
{
    public class TestLogin
    {
        AuthenticateRequest right_user = new AuthenticateRequest() { email = "test@gmail.com", password = "password", type = "applicant" };
        AuthenticateRequest wrong_user = new AuthenticateRequest() { email = "test@gmail.com", password = "wrong_password", type = "applicant" };
        AuthenticateResponse response = new AuthenticateResponse() { user_id = 1, email = "test@gmail.com", full_name = "Test", Token = "test1" };

        [Fact]
        public void Login_Success()
        {
            // Arrange
            var dataUser = A.Fake<IUserService>();
            A.CallTo(() => dataUser.Authenticate(right_user)).Returns(response);
            var controller = new UserController(dataUser);

            // Act
            var actionResult = controller.Authenticate(right_user);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnResponse = result.Value as AuthenticateResponse;
            Assert.Equal(returnResponse.email, right_user.email);
        }

        [Fact]
        public void Login_Fail()
        {
            // Arrange
            var dataUser = A.Fake<IUserService>();
            A.CallTo(() => dataUser.Authenticate(right_user)).Returns(response);
            var controller = new UserController(dataUser);

            // Act
            var actionResult = controller.Authenticate(wrong_user);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnResponse = result.Value as AuthenticateResponse;
            Assert.NotEqual(returnResponse.email, right_user.email);
        }
    }
}