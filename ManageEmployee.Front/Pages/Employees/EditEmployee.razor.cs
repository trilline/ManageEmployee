using ManageEmployee.Models.Entities;
using ManageEmployees.Dtos.Employee;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Employees
{
    public partial class EditEmployee
    {
        [Inject]
        private HttpClient HttpClient { get; set; }
        [Inject]
        NavigationManager Navigation { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        private Employee employee;

        protected override async Task OnInitializedAsync()
        {
            // Récupérer les informations de l'employé à partir de l'ID en paramètre
            employee = await HttpClient.GetFromJsonAsync<Employee>($"api/employees/{EmployeeId}");
        }

        private async Task SubmitForm()
        {
            var UpdatedEmployee = new UpdateEmployee
            {
                Firstname = employee.Firstname,
                Lastname = employee.Lastname,
                Email = employee.Email,
                Phonenumber = employee.Phonenumber,
                Birthday = employee.Birthday,
                Position = employee.Position,


            };
            // Envoyer une requête PUT à l'API avec les informations mises à jour de l'employé
            await HttpClient.PutAsJsonAsync($"api/employees/{EmployeeId}", UpdatedEmployee);

            // Rediriger vers la liste des employés après la modification
            Navigation.NavigateTo("/employees");
        }
    }
}