using ManageEmployees.Dtos.Statusleaverequest;

namespace ManageEmployees.Services.Contracts
{
    public interface IStatusLeaveRequestService
    {
        /// <summary>
        /// Gets the status leave requests.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadStatusLeaveRequest>> GetStatusLeaveRequests();

        /// <summary>
        /// Creates the status leave request asynchronous.
        /// </summary>
        /// <param name="createStatusLeaveRequest">The create status leave request.</param>
        /// <returns></returns>
        Task<ReadStatusLeaveRequest> CreateStatusLeaveRequestAsync(CreateStatusLeaveRequest createStatusLeaveRequest);

        /// <summary>
        /// Gets the status leave request by identifier asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <returns></returns>
        Task<ReadStatusLeaveRequest> GetStatusLeaveRequestByIdAsync(int statusLeaveRequestId);

        /// <summary>
        /// Gets the status leave request by label asynchronous.
        /// </summary>
        /// <param name="statusLabel">The status label.</param>
        /// <returns></returns>
        //Task<ReadStatusLeaveRequest> GetStatusLeaveRequestByLabelAsync(string statusLabel);

        /// <summary>
        /// Updates the status leave request asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <param name="updateStatusLeaveRequest">The update status leave request.</param>
        /// <returns></returns>
        Task UpdateStatusLeaveRequestAsync(int statusLeaveRequestId, UpdateStatusLeaveRequest updateStatusLeaveRequest);

        /// <summary>
        /// Deletes the status leave request by identifier asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <returns></returns>
        Task DeleteStatusLeaveRequestByIdAsync(int statusLeaveRequestId);
    }
}
