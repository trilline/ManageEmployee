using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories
{
    /// <summary>
    /// dépot de toutes les méthodes des ops crud sur LeaveRequest
    /// </summary>
    /// <seealso cref="ManageEmployees.Repositories.Contracts.ILeaveRequestRepository" />
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ManageEmployeeDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public LeaveRequestRepository(ManageEmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the leave requests asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Leaverequest>> GetLeaveRequestsAsync()
        {
            return await _context.Leaverequests.Include(x => x.Statusleaverequest).ToListAsync();
        }

        /// <summary>
        /// Creates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequest">The leave request.</param>
        /// <returns></returns>
        public async Task<Leaverequest> CreateLeaveRequestAsync(Leaverequest leaveRequest)
        {
            await _context.Leaverequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();
            return leaveRequest;
        }

        /// <summary>
        /// Gets the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        public async Task<Leaverequest> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            return await _context.Leaverequests
                .Include(x => x.Statusleaverequest)
                .FirstOrDefaultAsync(x => x.Leaverequestid == leaveRequestId);
        }

        /// <summary>
        /// Updates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestToUpdate">The leave request to update.</param>
        public async Task UpdateLeaveRequestAsync(Leaverequest leaveRequestToUpdate)
        {
            _context.Leaverequests.Update(leaveRequestToUpdate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        public async Task DeleteLeaveRequestByIdAsync(int leaveRequestId)
        {
            var leaveRequestToDelete = await _context.Leaverequests.FindAsync(leaveRequestId);
            _context.Leaverequests.Remove(leaveRequestToDelete);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Exists the asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(int employeeId, DateTime startDate, DateTime endDate, int StatusLeaveRequestID)
        {
            return await _context.Leaverequests
                .AnyAsync(x => x.Employeeid == employeeId && x.Startdate <= endDate && x.Enddate >= startDate && x.Statusleaverequestid == StatusLeaveRequestID);
        }
        public async Task<bool> LeaveRequestExistsAsync(int leaveRequestId)
        {
            return await _context.Leaverequests.AnyAsync(x => x.Leaverequestid == leaveRequestId);
        }
    }

}
