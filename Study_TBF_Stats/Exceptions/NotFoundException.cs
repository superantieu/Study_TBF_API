namespace Study_TBF_Stats.Exceptions
{
    public abstract class NotFoundException: Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
