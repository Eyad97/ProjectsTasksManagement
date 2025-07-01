using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectsTasksManagement.Domain.Entities;

public class BaseEntity
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();
}
