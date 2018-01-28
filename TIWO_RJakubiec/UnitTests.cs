using System;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace TIWO_RJakubiec
{
    [TestFixture()]
    public class UnitTests
    {
        [Test]
        public void User_age_under_validator_minimum_age()
        {
            var userValidator = new UserValidator();
            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.GetAge()).Returns(17);            

            Assert.IsFalse(userValidator.Validate(userMock.Object));
        }

        [Test]
        public void User_age_above_validator_minimum_age()
        {
            var userValidator = new UserValidator();
            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.GetAge()).Returns(19);

            Assert.IsTrue(userValidator.Validate(userMock.Object));
        }

        [Test]
        public void User_data_is_null()
        {
            var userValidator = new UserValidator();

            Assert.Throws(typeof(ArgumentNullException),() => userValidator.Validate(null));
        }
    }
}