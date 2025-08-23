namespace GoTask.Domain.Interfaces.Repositories;

public interface ITasksReadOnlyRepository
{
    Task GetTasksAsync();
    
    Task GetTaskByIdAsync();
}