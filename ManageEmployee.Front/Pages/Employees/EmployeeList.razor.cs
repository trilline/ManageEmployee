using ManageEmployee.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Employees
{
    public partial class EmployeeList
    {
        [Inject]
        private HttpClient HttpClient { get; set; }
        [Inject]
        NavigationManager Navigation { get; set; }

        private List<Employee> employees;

        protected override async Task OnInitializedAsync()
        {
            employees = await HttpClient.GetFromJsonAsync<List<Employee>>("api/employees");
        }

        private async Task EditEmployee(int employeeId)
        {
            // Rediriger vers la page de modification de l'employ� avec l'ID en param�tre
            Navigation.NavigateTo($"/employees/edit/{employeeId}");
        }

        private async Task DeleteEmployee(int employeeId)
        {
            // Envoyer une requ�te DELETE � l'API pour supprimer l'employ� avec l'ID en param�tre
            await HttpClient.DeleteAsync($"api/employees/{employeeId}");

            // Rafra�chir la liste des employ�s apr�s la suppression
            employees = await HttpClient.GetFromJsonAsync<List<Employee>>("api/employees");
        }

        private void AddEmployee()
        {
            // Rediriger vers la page de cr�ation d'un nouvel employ�
            Navigation.NavigateTo("/employees/create");
        }
    }
}