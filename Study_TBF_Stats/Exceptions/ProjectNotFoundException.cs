namespace Study_TBF_Stats.Exceptions
{
    public class ProjectNotFoundException : NotFoundException
    {
       
            public ProjectNotFoundException(int projectId) : base($"Product with id: {projectId} doesn't exist in the database.")
            {

            }
        
        
    }
}
