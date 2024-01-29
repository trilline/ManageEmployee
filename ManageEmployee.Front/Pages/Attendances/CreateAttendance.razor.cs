using ManageEmployee.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Attendances
{
    public partial class CreateAttendance
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private Attendance newAttendance = new Attendance();
        private string? errorMessage;

        private async Task Create()
        {
            try
            {
                //  HTTP POST pour créer la présence 
                HttpResponseMessage response = await Http.PostAsJsonAsync("api/attendance", newAttendance);
                response.EnsureSuccessStatusCode(); // status ok

                // Redirect
                Navigation.NavigateTo("/attendances");
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors de la création de la présence : {ex.Message}";
            }
        }
    }
}