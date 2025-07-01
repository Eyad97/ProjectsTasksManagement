using Microsoft.EntityFrameworkCore;
using ProjectsTasksManagement.Application.Interfaces.Repositories;
using ProjectsTasksManagement.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Infrastructure.Repositories;

public class TaskRepository(ApplicationDbContext context) : BaseRepository<Domain.Entities.Task>(context), ITaskReposirory
{
    public async Task<List<Domain.Entities.Task>> GetByProjectAsync(Guid projectId)
    {
        return await Table.Where(t => t.ProjectId == projectId)
                          .ToListAsync();
    }

    public async Task<List<Domain.Entities.Task>> GetAllOverdueAsync()
    {
        return await Table.Where(t => t.DueDate < DateTime.Now)
                          .Where(t => t.Status != Domain.Constants.TaskStatus.Overdue)
                          .ToListAsync();
    }
}
