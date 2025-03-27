using TodoList.WebAppp.Models;

namespace ToDoList.Web.Service
{
    public class TareaService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7138/api/";

        public TareaService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<List<TareaModelsView>> GetTasksAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Tarea/Get-Tareas");
                response.EnsureSuccessStatusCode();

                var tasks = await response.Content.ReadFromJsonAsync<List<TareaModelsView>>();
                return tasks ?? new List<TareaModelsView>();
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error obteniendo las tareas", ex);
            }
        }

        public async Task<TareaModelsView> GetTaskByTitleAsync(string title)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Tarea/Get-TareaByTitle{Uri.EscapeDataString(title)}");
                response.EnsureSuccessStatusCode();

                var task = await response.Content.ReadFromJsonAsync<TareaModelsView>();
                return task ?? throw new Exception("Task not found");
            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo un error obteniendo las tareas por titulo: {title}", ex);
            }
        }

        public async Task<OperationResult> CreateTaskAsync(ModelCreate task)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Tarea/Set-Tarea", task);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<OperationResult>();
                return result ?? new OperationResult { Success = false, Message = "Failed to create task" };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error creando la tarea: {ex.Message}"
                };
            }
        }

        public async Task<OperationResult> UpdateTaskAsync(TareaModelsView task)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Tarea/Update-Tarea", task);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<OperationResult>();
                return result ?? new OperationResult { Success = false, Message = "Failed to update task" };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error : {ex.Message}"
                };
            }
        }

        public async Task<OperationResult> DeleteTaskAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Tarea/Delete-Tarea/{id}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<OperationResult>();
                return result ?? new OperationResult { Success = false, Message = "Failed to delete task" };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error borrando las tareas: {ex.Message}"
                };
            }
        }
    }
}