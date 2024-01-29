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

        
    
        // Propri�t� pour stocker les informations du d�partement � �diter
        private Department departmentToEdit = new Department();

        [Parameter]
        [SupplyParameterFromQuery]
        public int DepartmentID { get; set; }

        // M�thode pour initialiser le composant
        protected override async Task OnInitializedAsync()
        {
            // Initialiser les informations du d�partement � �diter
            await InitializeDepartment(DepartmentID);
        }

        // M�thode pour initialiser les informations du d�partement � �diter
        private async Task InitializeDepartment(int DepartmentID)
        {
            try
            {
                if (DepartmentID < 0)
                {
                    // Si l'ID du d�partement est inf�rieur � 1, cela signifie que l'utilisateur a essay� d'acc�der � la page d'�dition sans fournir d'ID de d�partement valide
                    // Redirect le user vers la page de liste des d�partements
                    Navigation.NavigateTo("/departments", true);
                }
                //id par default si l'utilisateur n'a pas fourni d'ID de d�partement valide
               var defaultDepartementID = DepartmentID == 0  ?  1: DepartmentID;
                // R�cup�rer le d�partement � partir de l'ID en utilisant une requ�te HTTP
                departmentToEdit = await Http.GetFromJsonAsync<Department>($"api/department/{defaultDepartementID}");
            }
            catch (Exception ex)
            {
                // G�rer les erreurs, par exemple en affichant un message d'erreur � l'utilisateur
                Console.WriteLine($"Une erreur s'est produite lors de la r�cup�ration du d�partement : {ex.Message}");
            }
        }

        // M�thode pour soumettre le formulaire d'�dition
        private async Task SubmitForm()
        
        {
            // Envoyer une requ�te HTTP PUT � l'API avec les informations du d�partement � �diter
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