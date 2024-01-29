using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories
{
    public class StatusLeaveRequestRepository : IStatusLeaveRequestRepository
    {
        private readonly ManageEmployeeDbContext _context;

        public StatusLeaveRequestRepository(ManageEmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Statusleaverequest>> GetStatusLeaveRequestsAsync()
        {
            return await _context.Statusleaverequests.ToListAsync();
        }

        public async Task<Statusleaverequest> CreateStatusLeaveRequestAsync(Statusleaverequest statusLeaveRequest)
        {
            await _context.Statusleaverequests.AddAsync(statusLeaveRequest);
            await _context.SaveChangesAsync();
            return statusLeaveRequest;
        }

        public async Task<Statusleaverequest> GetStatusLeaveRequestByIdAsync(int statusLeaveRequestId)
        {
            return await _context.Statusleaverequests.FirstOrDefaultAsync(x => x.Statusleaverequestid == statusLeaveRequestId);
        }

        public async Task<Statusleaverequest> GetStatusLeaveRequestByLabelAsync(string statusLabel)
        {
            return await _context.Statusleaverequests.FirstOrDefaultAsync(x => x.Statuslabel == statusLabel);
        }

        public async Task UpdateStatusLeaveRequestAsync(Statusleaverequest statusLeaveRequestToUpdate)
        {
            _context.Statusleaverequests.Update(statusLeaveRequestToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStatusLeaveRequestByIdAsync(int statusLeaveRequestId)
        {
            var statusLeaveRequestToDelete = await _context.Statusleaverequests.FindAsync(statusLeaveRequestId);
            _context.Statusleaverequests.Remove(statusLeaveRequestToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string statusLabel)
        {

            return await _context.Statusleaverequests.AnyAsync(x => x.Statuslabel == statusLabel);
        }
    }
}
