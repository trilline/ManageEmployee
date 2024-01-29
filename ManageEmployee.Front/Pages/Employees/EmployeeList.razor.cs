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
            // Rediriger vers la page de modification de l'employé avec l'ID en paramètre
            Navigation.NavigateTo($"/employees/edit/{employeeId}");
        }

        private async Task DeleteEmployee(int employeeId)
        {
            // Envoyer une requête DELETE à l'API pour supprimer l'employé avec l'ID en paramètre
            await HttpClient.DeleteAsync($"api/employees/{employeeId}");

            // Rafraîchir la liste des employés après la suppression
            employees = await HttpClient.GetFromJsonAsync<List<Employee>>("api/employees");
        }

        private void AddEmployee()
        {
            // Rediriger vers la page de création d'un nouvel employé
            Navigation.NavigateTo("/employees/create");
        }
    }
}