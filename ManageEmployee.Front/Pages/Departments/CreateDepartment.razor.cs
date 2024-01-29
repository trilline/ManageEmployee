using ManageEmployee.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Departments
{
    public partial class CreateDepartment
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private Department newDepartment = new Department();
        private string? errorMessage;

        private async Task Create()
        {
            try
            {
                // HTTP POST pour cr�er le d�pt
                HttpResponseMessage response = await Http.PostAsJsonAsync("api/department", newDepartment);
                response.EnsureSuccessStatusCode(); // req ok

                // Redirect to /departments
                Navigation.NavigateTo("/departments");
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors de la cr�ation du d�partement : {ex.Message}";
            }
        }
    }
}