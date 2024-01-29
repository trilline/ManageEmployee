using ManageEmployee.Models.Dtos.Department;
using ManageEmployee.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Departments
{
    public partial class EditDepartment
    {
        // Injecter le service HttpClient
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        
    
        // Propriété pour stocker les informations du département à éditer
        private Department departmentToEdit = new Department();

        [Parameter]
        [SupplyParameterFromQuery]
        public int DepartmentID { get; set; }

        // Méthode pour initialiser le composant
        protected override async Task OnInitializedAsync()
        {
            // Initialiser les informations du département à éditer
            await InitializeDepartment(DepartmentID);
        }

        // Méthode pour initialiser les informations du département à éditer
        private async Task InitializeDepartment(int DepartmentID)
        {
            try
            {
                if (DepartmentID < 0)
                {
                    // Si l'ID du département est inférieur à 1, cela signifie que l'utilisateur a essayé d'accéder à la page d'édition sans fournir d'ID de département valide
                    // Redirect le user vers la page de liste des départements
                    Navigation.NavigateTo("/departments", true);
                }
                //id par default si l'utilisateur n'a pas fourni d'ID de département valide
               var defaultDepartementID = DepartmentID == 0  ?  1: DepartmentID;
                // Récupérer le département à partir de l'ID en utilisant une requête HTTP
                departmentToEdit = await Http.GetFromJsonAsync<Department>($"api/department/{defaultDepartementID}");
            }
            catch (Exception ex)
            {
                // Gérer les erreurs, par exemple en affichant un message d'erreur à l'utilisateur
                Console.WriteLine($"Une erreur s'est produite lors de la récupération du département : {ex.Message}");
            }
        }

        // Méthode pour soumettre le formulaire d'édition
        private async Task SubmitForm()
        
        {
            // Envoyer une requête HTTP PUT à l'API avec les informations du département à éditer
            var updatedDepartment = new UpdateDepartment() { 
            
            Name = departmentToEdit.Name,
            Description = departmentToEdit.Description,
            Address = departmentToEdit.Address
            };
             await Http.PutAsJsonAsync($"api/department/{departmentToEdit.Departmentid}", updatedDepartment);

             Navigation.NavigateTo("/departments");
        }
    }
}