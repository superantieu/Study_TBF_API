namespace Study_TBF_Stats.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int userId) : base($"Product with id: {userId} doesn't exist in the database.")
        {

        }
    }
}
