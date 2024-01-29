using ManageEmployee.Models.Entities;
using ManageEmployees.Dtos.Attendance;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManageEmployee.Front.Pages.Attendances
{
    public partial class AttendanceList
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private List<ReadAttendance> Attendances = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadAttendances();
        }

        /// <summary>
        /// Loads the attendances.
        /// </summary>
        private async Task LoadAttendances()
        {
            Attendances = await Http.GetFromJsonAsync<List<ReadAttendance>>("api/attendance");
        }
        private async Task EditAttendance(int attendanceID)
        {
            //"/fr-fr/supervision/sensor/dashboard?AreaID=9"
            Navigation.NavigateTo($"/attendance/edit?attendanceID={attendanceID}");
        }

        private async Task DeleteAttendance(int id)
        {
            await Http.DeleteAsync($"api/attendance/{id}");
            // Mettre à jour la liste des attendances côté client après la suppression réussie
            Attendances.RemoveAll(d => d.Attendanceid == id);

            // Forcer le rendu du composant pour afficher la liste mise à jour des attendances
            StateHasChanged();

        }

        private void GoToCreateAttandance()
        {
            Navigation.NavigateTo("/attendance/create");
        }
    }
}