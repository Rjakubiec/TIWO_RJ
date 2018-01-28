using System;

namespace TIWO_RJakubiec
{
    public interface IUser
    {
        int GetAge();
    }

    public class User : IUser
    {
        private string Name { get; set; }
        private int Age { get; set; }

        public int GetAge()
        {
            return Age;
        }
    }

    public class UserValidator
    {
        private const int MinimumAge = 18;

        public bool Validate(IUser user)
        {
            if (user == null) throw new ArgumentNullException();

            if (user.GetAge() < MinimumAge) return false;
            return true;
        }
    }
}