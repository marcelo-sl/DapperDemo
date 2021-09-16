using DapperDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Data.Task>> GetTasksAsync();
        Task<Data.Task> GetTaskByIdAsync(int Id);
        Task<TaskContainer> GetTaskAndCounter();
        Task<int> SaveAsync(Data.Task newTask);
        Task<int> UpdateTaskStatusAsync(Data.Task task);
        Task<int> DeleteAsync(int id);
    }
}
