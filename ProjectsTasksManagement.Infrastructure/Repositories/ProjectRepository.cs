using Microsoft.EntityFrameworkCore;
using ProjectsTasksManagement.Application.Interfaces.Repositories;
using ProjectsTasksManagement.Domain.Entities;
using ProjectsTasksManagement.Infrastructure.DBContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Infrastructure.Repositories;

public class ProjectRepository(ApplicationDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public async Task<List<Project>> GetAllAsync()
    {
        return await Table.Include(p => p.Tasks)
                          .ToListAsync();
    }
}
