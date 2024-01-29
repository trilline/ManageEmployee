using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface ILeaveRequestRepository
    {
        /// <summary>
        /// Gets the leave requests asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<Leaverequest>> GetLeaveRequestsAsync();

        /// <summary>
        /// Creates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequest">The leave request.</param>
        /// <returns></returns>
        Task<Leaverequest> CreateLeaveRequestAsync(Leaverequest leaveRequest);

        /// <summary>
        /// Gets the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task<Leaverequest> GetLeaveRequestByIdAsync(int leaveRequestId);

        /// <summary>
        /// Updates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestToUpdate">The leave request to update.</param>
        /// <returns></returns>
        Task UpdateLeaveRequestAsync(Leaverequest leaveRequestToUpdate);

        /// <summary>
        /// Deletes the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task DeleteLeaveRequestByIdAsync(int leaveRequestId);

        /// <summary>
        /// Exists the asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        Task<bool> ExistAsync(int employeeId, DateTime startDate, DateTime endDate, int StatusLeaveRequestID);

        /// <summary>
        /// Leaves the request exists asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task<bool> LeaveRequestExistsAsync(int leaveRequestId);
    }
}
