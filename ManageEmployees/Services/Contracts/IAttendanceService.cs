using ManageEmployees.Dtos.Attendance;

namespace ManageEmployees.Services.Contracts
{
    public interface IAttendanceService
    {
        /// <summary>
        /// Gets the attendances asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadAttendance>> GetAttendancesAsync();

        /// <summary>
        /// Gets the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        Task<ReadAttendance> GetAttendanceByIdAsync(int attendanceId);

        /// <summary>
        /// Gets the attendance between two dates asynchronous.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        Task<List<ReadAttendance>> GetAttendanceBetweenTwoDatesAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the attendance starting before date asynchronous.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        Task<List<ReadAttendance>> GetAttendanceStartingBeforeDateAsync(DateTime startDate);

        /// <summary>
        /// Gets the attendance ending before date asynchronous.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        Task<List<ReadAttendance>> GetAttendanceEndingBeforeDateAsync(DateTime endDate);

        /// <summary>
        /// Updates the attendance asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <param name="updateAttendance">The update attendance.</param>
        /// <returns></returns>
        Task UpdateAttendanceAsync(int attendanceId, UpdateAttendance updateAttendance);

        /// <summary>
        /// Creates the attendance asynchronous.
        /// </summary>
        /// <param name="createAttendance">The create attendance.</param>
        /// <returns></returns>
        Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance createAttendance);

        /// <summary>
        /// Deletes the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        Task DeleteAttendanceByIdAsync(int attendanceId);
    }
}
