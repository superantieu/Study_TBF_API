namespace Study_TBF_Stats.Exceptions
{
    public sealed class MaxAreaRangeBadRequestException : BadRequestException
    {
        public MaxAreaRangeBadRequestException(): base("Max Area can't be less than Min Area") { }
    }
}
