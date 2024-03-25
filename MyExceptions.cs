using System;

namespace KMA.ProgrammingInCSharp24
{
    // Коли дата народження знаходиться в майбутньому
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException(string message) : base("Error! Birth date cannot be in the future.")
        {
        }
    }

    // Коли дата народження занадто далеко в минулому
    public class DistantPastBirthDateException : Exception
    {
        public DistantPastBirthDateException(string message) : base("Error! Birth date is too far in the past.")
        {
        }
    }

    // Коли введена невірна адреса електронної пошти
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base("Error! Invalid email address.")
        {
        }
    }
}
