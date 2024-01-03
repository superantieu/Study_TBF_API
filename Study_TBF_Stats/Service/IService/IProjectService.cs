using Study_TBF_Stats.Models.Dto;
using Study_TBF_Stats.RequestFeatures;

namespace Study_TBF_Stats.Service.IService
{
    public interface IProjectService
    {
        Task<(IEnumerable<TbProjectDto> tbProjects, MetaData metaData)> GetAllProjectAsync(ProjectParameters projectParameters, bool trackChanges);
        Task<TbProjectDto> GetProjectAsync(int productId, bool trackChanges);
    }
}
