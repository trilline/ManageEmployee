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

        // M�thode pour charger les donn�es de l'assiduit� � �diter
        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (AttendanceID < 0)
                {
                    // Si l'ID du d�partement est inf�rieur � 1, cela signifie que l'utilisateur a essay� d'acc�der � la page d'�dition sans fournir d'ID de d�partement valide
                    // Redirect le user vers la page de liste des d�partements
                    Navigation.NavigateTo("/attendances", true);
                }
                //id par default si l'utilisateur n'a pas fourni d'ID de d�partement valide
                var defaultattendanceID = AttendanceID == 0 ? 1 : AttendanceID;
                // R�cup�rer le d�partement � partir de l'ID en utilisant une requ�te HTTP
                AttendanceToEdit = await Http.GetFromJsonAsync<Attendance>($"api/attendance/{defaultattendanceID}");
            }
            catch (Exception ex)
            {
                // G�rer les erreurs, par exemple en affichant un message d'erreur � l'utilisateur
                Console.WriteLine($"Une erreur s'est produite lors de la r�cup�ration de la pr�sence : {ex.Message}");
            }

            //TODO Convertir les dates en format cha�ne de caract�res pour les champs de formulaire 
            //arrivalDateTimeString = AttendanceToEdit.Arrivaldate.ToString("yyyy-MM-ddTHH:mm");
            //departureDateTimeString = AttendanceToEdit.Departuredate?.ToString("yyyy-MM-ddTHH:mm") ?? "";
        }

        // M�thode pour soumettre le formulaire d'�dition
        private async Task SubmitForm()
        {

            var updatedAttendance = new UpdateAttendance()
            {

              EmployeeId = (int)AttendanceToEdit.Employeeid,
              Arrivaldate = AttendanceToEdit.Arrivaldate,
              Departuredate = AttendanceToEdit.Departuredate

            };
            await Http.PutAsJsonAsync($"api/attendance/{AttendanceToEdit.Attendanceid}", updatedAttendance);

            // Rediresct vers la liste des pr�sences apr�s la modification
            Navigation.NavigateTo("/attendances");
        }
    }
}