using ManageEmployee.Models.Entities;
using ManageEmployees.Dtos.Attendance;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Attendances
{
    public partial class EditAttendance
    {
        /// <summary>
        /// Gets or sets the HTTP.
        /// </summary>
        /// <value>
        /// The HTTP.
        /// </value>
        [Inject]
        HttpClient Http { get; set; }

        /// <summary>
        /// Gets or sets the navigation.
        /// </summary>
        /// <value>
        /// The navigation.
        /// </value>
        [Inject]
        NavigationManager Navigation { get; set; }

        /// <summary>
        /// Gets or sets the attendance to edit.
        /// </summary>
        /// <value>
        /// The attendance to edit.
        /// </value>
        private Attendance AttendanceToEdit { get; set; }

        private string arrivalDateTimeString { get; set; }
        private string departureDateTimeString { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int AttendanceID { get; set; }

        // Méthode pour charger les données de l'assiduité à éditer
        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (AttendanceID < 0)
                {
                    // Si l'ID du département est inférieur à 1, cela signifie que l'utilisateur a essayé d'accéder à la page d'édition sans fournir d'ID de département valide
                    // Redirect le user vers la page de liste des départements
                    Navigation.NavigateTo("/attendances", true);
                }
                //id par default si l'utilisateur n'a pas fourni d'ID de département valide
                var defaultattendanceID = AttendanceID == 0 ? 1 : AttendanceID;
                // Récupérer le département à partir de l'ID en utilisant une requête HTTP
                AttendanceToEdit = await Http.GetFromJsonAsync<Attendance>($"api/attendance/{defaultattendanceID}");
            }
            catch (Exception ex)
            {
                // Gérer les erreurs, par exemple en affichant un message d'erreur à l'utilisateur
                Console.WriteLine($"Une erreur s'est produite lors de la récupération de la présence : {ex.Message}");
            }

            //TODO Convertir les dates en format chaîne de caractères pour les champs de formulaire 
            //arrivalDateTimeString = AttendanceToEdit.Arrivaldate.ToString("yyyy-MM-ddTHH:mm");
            //departureDateTimeString = AttendanceToEdit.Departuredate?.ToString("yyyy-MM-ddTHH:mm") ?? "";
        }

        // Méthode pour soumettre le formulaire d'édition
        private async Task SubmitForm()
        {

            var updatedAttendance = new UpdateAttendance()
            {

              EmployeeId = (int)AttendanceToEdit.Employeeid,
              Arrivaldate = AttendanceToEdit.Arrivaldate,
              Departuredate = AttendanceToEdit.Departuredate

            };
            await Http.PutAsJsonAsync($"api/attendance/{AttendanceToEdit.Attendanceid}", updatedAttendance);

            // Rediresct vers la liste des présences après la modification
            Navigation.NavigateTo("/attendances");
        }
    }
}