using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IStatusLeaveRequestRepository
    {
        /// <summary>
        /// Gets the status leave requests asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<Statusleaverequest>> GetStatusLeaveRequestsAsync();

        /// <summary>
        /// Creates the status leave request asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequest">The status leave request.</param>
        /// <returns></returns>
        Task<Statusleaverequest> CreateStatusLeaveRequestAsync(Statusleaverequest statusLeaveRequest);

        /// <summary>
        /// Gets the status leave request by identifier asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <returns></returns>
        Task<Statusleaverequest> GetStatusLeaveRequestByIdAsync(int statusLeaveRequestId);

        /// <summary>
        /// Gets the status leave request by label asynchronous.
        /// </summary>
        /// <param name="statusLabel">The status label.</param>
        /// <returns></returns>
        Task<Statusleaverequest> GetStatusLeaveRequestByLabelAsync(string statusLabel);

        /// <summary>
        /// Updates the status leave request asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestToUpdate">The status leave request to update.</param>
        /// <returns></returns>
        Task UpdateStatusLeaveRequestAsync(Statusleaverequest statusLeaveRequestToUpdate);

        /// <summary>
        /// Deletes the status leave request by identifier asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <returns></returns>
        Task DeleteStatusLeaveRequestByIdAsync(int statusLeaveRequestId);

        /// <summary>
        /// Exists the asynchronous.
        /// </summary>
        /// <param name="statusLabel">The status label.</param>
        /// <returns></returns>
        Task<bool> ExistAsync(string statusLabel);
    }
}
