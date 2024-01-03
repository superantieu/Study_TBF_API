using AutoMapper;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Exceptions;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;
using Study_TBF_Stats.RequestFeatures;
using Study_TBF_Stats.Service.IService;

namespace Study_TBF_Stats.Service
{
    internal class ProjectService : IProjectService
    {

        private readonly IRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;
       
        
       


        public ProjectService(IRepositoryManager repository, IMapper mapper)
        {

            _mapper = mapper;
            _repositoryManager = repository;
            
            
        }


        public async Task<(IEnumerable<TbProjectDto> tbProjects, MetaData metaData)> GetAllProjectAsync(ProjectParameters projectParameters, bool trackChanges)
        {
            var projectMetaData = await _repositoryManager.ProjectRepository.GetAllProjectsAsync(projectParameters, trackChanges);
            var projectsDto = _mapper.Map<IEnumerable<TbProjectDto>>(projectMetaData);
            return (tbProjects: projectsDto, metaData: projectMetaData.MetaData);
        }

        public async Task<TbProjectDto> GetProjectAsync(int projectId, bool trackChanges)
        {
            
            var project = await ValidProjectExist( projectId, trackChanges);
            var projectDto = _mapper.Map<TbProjectDto>(project);
            return projectDto;
        }
        private async Task<TbProject> ValidProjectExist( int projectId, bool trackChanges)
        {
            var project = await _repositoryManager.ProjectRepository.GetProjectAsync(projectId, trackChanges);
            if (project is null)
            {
                throw new ProjectNotFoundException(projectId);
            }
            return project;
        }
    }
}
