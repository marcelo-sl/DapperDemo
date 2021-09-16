using Dapper;
using DapperDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private DbSession _db;

        public TaskRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<List<Data.Task>> GetTasksAsync()
        {
            using (var conn = _db.Connection)
            {
                string query = "select * from Tbl_Task";
                List<Data.Task> tasks = (await conn.QueryAsync<Data.Task>(query)).ToList();
                return tasks;
            }
        }

        public async Task<Data.Task> GetTaskByIdAsync(int Id)
        {
            using (var conn = _db.Connection)
            {
                string query = "select * from Tbl_Task where Id = @Id";
                Data.Task task = await conn.QueryFirstOrDefaultAsync<Data.Task>(query, new { Id });

                return task;
            }
        }

        public async Task<TaskContainer> GetTaskAndCounter()
        {
            using (var conn = _db.Connection)
            {
                string query = @"select count(*) from Tbl_Task
                                 select * from Tbl_Task";

                var reader = await conn.QueryMultipleAsync(query);

                return new TaskContainer
                {
                    Counter = (await reader.ReadAsync<int>()).FirstOrDefault(),
                    Tasks = (await reader.ReadAsync<Data.Task>()).ToList()
                };
            }
        }

        public async Task<int> SaveAsync(Data.Task newTask)
        {
            using (var conn = _db.Connection)
            {
                string command = "insert into Tbl_Task (Description, Done) values (@Description, @Done)";

                return await conn.ExecuteAsync(command, newTask);
            }
        }

        public async Task<int> UpdateTaskStatusAsync(Data.Task task)
        {
            using (var conn = _db.Connection)
            {
                string command = "update Tbl_Task set Done = @Done where Id = @Id";
                return await conn.ExecuteAsync(command, task);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                string command = "delete from Tbl_Task where Id = @id";
                return await conn.ExecuteAsync(command, new { id });
            }
        }
    }
}
