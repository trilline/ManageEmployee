using ManageEmployees.Dtos.Leaverequest;

namespace ManageEmployees.Services.Contracts
{
    public interface ILeaveRequestService
    {
        /// <summary>
        /// Gets the leave requests.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadLeaveRequest>> GetLeaveRequests();

        /// <summary>
        /// Gets the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId);

        /// <summary>
        /// Creates the leave request asynchronous.
        /// </summary>
        /// <param name="createLeaveRequest">The create leave request.</param>
        /// <returns></returns>
        Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest createLeaveRequest);

        /// <summary>
        /// Updates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <param name="updateLeaveRequest">The update leave request.</param>
        /// <returns></returns>
        Task UpdateLeaveRequestAsync(int leaveRequestId, UpdateLeaveRequest updateLeaveRequest);

        /// <summary>
        /// Deletes the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task DeleteLeaveRequestByIdAsync(int leaveRequestId);

        /// <summary>
        /// Leaves the request exists asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task<bool> LeaveRequestExistsAsync(int leaveRequestId);
    }
}
