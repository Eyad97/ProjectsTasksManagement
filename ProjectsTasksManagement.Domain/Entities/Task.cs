using ProjectsTasksManagement.Domain.Constants;
using ProjectsTasksManagement.Domain.Interfaces;
using System;

namespace ProjectsTasksManagement.Domain.Entities;

public class Task : BaseEntity, ISoftDeletable
{
    public string Title { get; private set; }
    public DateTime DueDate { get; private set; }
    public string Status { get; private set; } = TaskStatus.New;
    public bool IsDeleted { get; set; }
    public Guid ProjectId { get; private set; }
    public virtual Project Project { get; private set; }

    public Task(string title, DateTime dueDate, Guid projectId)
    {
        SetTitle(title);
        SetDueDate(dueDate);
        SetProjectId(projectId);
    }

    public void SetTitle(string title) => Title = title;

    public void SetDueDate(DateTime dueDate) => DueDate = dueDate;

    public void SetStatus(string status) => Status = status;

    public void SetProjectId(Guid projectId) => ProjectId = projectId;
}
