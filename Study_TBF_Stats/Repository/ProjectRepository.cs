using Microsoft.EntityFrameworkCore;
using Study_TBF_Stats.Contract;
using Study_TBF_Stats.Extensions;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Repository.IRepository;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Repository
{
    public class ProjectRepository : RepositoryBase<TbProject>, IProjectRepository

    {
        public ProjectRepository(StudyTbfSupContext studyTbfSupContext) : base(studyTbfSupContext) { }
        public void CreateProject(TbProject project)
        {
            Create(project);
        }  public void DeleteProject(TbProject project)
        {
            Delete(project);
        }
        public async Task<PagedList<TbProject>> GetAllProjectsAsync(ProjectParameters projectparameters, bool trackChanges)
        {
            var projects = await FindByCondtion(e=>true, trackChanges)
                .IsCompletedProject(projectparameters.Completed)
                .FilterProject(projectparameters.MinFloorArea, projectparameters.MaxFloorArea)
                .SearchProject(projectparameters.SearchTerm)
                .SortProject(projectparameters.orderBy)
                
                .ToListAsync();
            return PagedList<TbProject>.ToPagedList(projects, projectparameters.pageNumber, projectparameters.pageSize);
        }

        public async Task<TbProject> GetProjectAsync(int projectId, bool trackChanges)
        {
            return await FindByCondtion(e => e.ProjectId.Equals(projectId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<TbProject> GetProjectForInvoiceAsync(int projectId, bool trackChanges)
        {
            return await FindByCondtion(e => e.ProjectId.Equals(projectId), trackChanges).SingleOrDefaultAsync();
        }
    }
}
