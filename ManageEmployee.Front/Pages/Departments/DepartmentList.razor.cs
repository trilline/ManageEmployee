using ManageEmployee.Models.Dtos.Department;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Departments
{
    public partial class DepartmentList
    {

        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private List<ReadDepartment> Departments = new();

        protected override async Task OnInitializedAsync()
        {
            Departments = await Http.GetFromJsonAsync<List<ReadDepartment>>("api/department");
        }

        private void EditDepartment(int DepartmentID)
        {
            //"/fr-fr/supervision/sensor/dashboard?AreaID=9"
            Navigation.NavigateTo($"/departments/edit?DepartmentID={DepartmentID}");
        }

        private async Task DeleteDepartment(int id)
        {
            await Http.DeleteAsync($"api/department/{id}");
            // Mettre � jour la liste des d�partements c�t� client apr�s la suppression r�ussie
            Departments.RemoveAll(d => d.Id == id);

            // Forcer le rendu du composant pour afficher la liste mise � jour des d�partements
            StateHasChanged();

        }

        private void GoToCreateDepartment()
        {
            Navigation.NavigateTo("/departments/create");
        }
    }
}