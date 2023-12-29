using Study_TBF_Stats.Models;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Repository.IRepository
{
    public interface IProjectRepository
    {
        Task<PagedList<TbProject>> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges);
        Task<TbProject> GetProjectAsync(Guid projectId, bool trackChanges);
        Task<TbProject> GetProjectForInvoiceAsync(Guid projectId, bool trackChanges);

        void CreateProject(TbProject project);

        void DeleteProject(TbProject project);

    }
}
