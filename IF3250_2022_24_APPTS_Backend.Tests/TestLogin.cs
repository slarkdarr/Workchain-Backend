using System;
using Xunit;
using FakeItEasy;
using IF3250_2022_24_APPTS_Backend.Services;
using IF3250_2022_24_APPTS_Backend.Models.User;
using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Entities;

namespace IF3250_2022_24_APPTS_Backend.Tests
{
    public class TestUser
    {
        AuthenticateRequest right_login_user = new AuthenticateRequest() { email = "test@gmail.com", password = "password", type = "applicant" };
        AuthenticateRequest wrong_login_user = new AuthenticateRequest() { email = "test@gmail.com", password = "wrong_password", type = "applicant" };
        AuthenticateResponse login_response = new AuthenticateResponse() { user_id = 1, email = "test@gmail.com", full_name = "Test", Token = "test1" };
        RegisterRequest right_register_user = new RegisterRequest() { email = "test@gmail.com", password = "password", full_name = "Test", phone_number = "12345678", type = "applicant" };
        RegisterRequest wrong_register_user = new RegisterRequest() { email = "test@gmail.com", password = "password", full_name = "Test", phone_number = "12345678", type = "applicant" };
        User user = new User { user_id = 1, email = "test@gmail.com", password = "password", full_name = "Test", phone_number = "12345678", type = "applicant" };

        [Fact]
        public void Login_Success()
        {
            // Arrange
            var dataUser = A.Fake<IUserService>();
            A.CallTo(() => dataUser.Authenticate(right_login_user)).Returns(login_response);
            var controller = new UserController(dataUser);

            // Act
            var actionResult = controller.Authenticate(right_login_user);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnResponse = result.Value as AuthenticateResponse;
            Assert.Equal(returnResponse.email, right_login_user.email);
        }

        [Fact]
        public void Login_Fail()
        {
            // Arrange
            var dataUser = A.Fake<IUserService>();
            A.CallTo(() => dataUser.Authenticate(wrong_login_user)).Throws(new AppException("Email or password is incorrect"));
            var controller = new UserController(dataUser);

            // Act
            var actionResult = controller.Authenticate(wrong_login_user);

            // Assert
            var result = actionResult.Exception;
            Assert.IsType<AggregateException>(result);
        }

        [Fact]
        public void Register_Success()
        {
            // Arrange
            var dataUser = A.Fake<IUserService>();
            A.CallTo(() => dataUser.Register(right_register_user)).Returns(user);
            var controller = new UserController(dataUser);

            // Act
            var actionResult = controller.Register(right_register_user);

            // Assert
            var result = actionResult.Result;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Register_Fail()
        {
            // Arrange
            var dataUser = A.Fake<IUserService>();
            A.CallTo(() => dataUser.Register(wrong_register_user)).Throws(new AppException("Email test@gmail.com is already taken"));
            var controller = new UserController(dataUser);

            // Act
            var actionResult = controller.Register(wrong_register_user);

            // Assert
            var result = actionResult.Exception;
            Assert.IsType<AggregateException>(result);
        }
    }
}