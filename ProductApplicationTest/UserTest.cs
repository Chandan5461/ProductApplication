using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApplication.Controllers;
using ProductApplication.Models.Domain;
using ProductApplication.Repostries;

namespace ProductApplicationTest
{
    public class GetUserIdTest
    {
        private MockRepository _repository;
        private Mock<IUserRepository> _userRepository;



        [SetUp]
        public void Setup()
        {
            this._repository = new MockRepository(MockBehavior.Strict);
            this._userRepository = this._repository.Create<IUserRepository>();
        }



        private UsersController CreateUserController()
        {
            return new UsersController(this._userRepository.Object);
        }



        [Test]
        public void GetUserId_ShouldReturnOkResult_WhenUserFound()
        {
            // Arrange
            var userController = this.CreateUserController();
            var user = new User() { Id = 1, Name = "Roshan", Address = "Mohali", City = "Landran" };
            int Id = 1;
            _userRepository.Setup(i => i.Get(Id)).Returns(user);



            // Act
            var result = userController.GetUsers(Id) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public void GetUserId_ShouldReturnNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userController = this.CreateUserController();
            User? user = null;
            int Id = 1;
            _userRepository.Setup(i => i.Get(Id)).Returns(user);



            // Act
            var result = userController.GetUsers(Id) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }





    }
}
