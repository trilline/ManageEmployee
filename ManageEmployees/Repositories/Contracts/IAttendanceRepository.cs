using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IAttendanceRepository
    {
        /// <summary>
        /// Gets the attendances asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<Attendance>> GetAttendancesAsync();

        /// <summary>
        /// Gets the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        Task<Attendance> GetAttendanceByIDAsync(int attendanceId);

        /// <summary>
        /// Gets the attendance between two dates asynchronous.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        Task<List<Attendance>> GetAttendanceBetweenTwoDatesAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the attendance starting before date asynchronous.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        Task<List<Attendance>> GetAttendanceStartingBeforeDateAsync(DateTime startDate);

        /// <summary>
        /// Gets the attendance ending before date asynchronous.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        Task<List<Attendance>> GetAttendanceEndingBeforeDateAsync(DateTime endDate);

        /// <summary>
        /// Updates the attendance asynchronous.
        /// </summary>
        /// <param name="attendanceToUpdate">The attendance to update.</param>
        /// <returns></returns>
        Task UpdateAttendanceAsync(Attendance attendanceToUpdate);

        /// <summary>
        /// Creates the attendance asynchronous.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        Task<Attendance> CreateAttendanceAsync(Attendance attendance);

        /// <summary>
        /// Deletes the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        Task DeleteAttendanceByIdAsync(int attendanceId);

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(int employeeId, DateTime startDate, DateTime? endDate);

    }
}
