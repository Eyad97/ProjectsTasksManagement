using ProjectsTasksManagement.Domain.Interfaces;
using System.Collections.Generic;

namespace ProjectsTasksManagement.Domain.Entities;

public class Project : BaseEntity, ISoftDeletable
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<Task> Tasks { get; private set; }

    public Project(string name, string description)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name) => Name = name;
    public void SetDescription(string description) => Description = description; 
}
