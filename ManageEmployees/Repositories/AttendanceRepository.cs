using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories
{
    public class AttendanceRepository: IAttendanceRepository
    {
        private readonly ManageEmployeeDbContext _context;

        public AttendanceRepository(ManageEmployeeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the attendances asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Attendance>> GetAttendancesAsync()
        {
            return await _context.Attendances.ToListAsync();
        }

        /// <summary>
        /// Gets the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        public async Task<Attendance> GetAttendanceByIDAsync(int attendanceId)
        {
            return await _context.Attendances.FirstOrDefaultAsync(a => a.Attendanceid == attendanceId);
        }

        /// <summary>
        /// Gets the attendance betweentwodates asynchronous.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public async Task<List<Attendance>> GetAttendanceBetweenTwoDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Attendances
                .Where(a => a.Arrivaldate >= startDate && a.Arrivaldate <= endDate)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the attendance stating before date asynchronous.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        public async Task<List<Attendance>> GetAttendanceStartingBeforeDateAsync(DateTime startDate)
        {
            return await _context.Attendances
                .Where(a => a.Arrivaldate < startDate)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the attendance endinging before date asynchronous.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public async Task<List<Attendance>> GetAttendanceEndingBeforeDateAsync(DateTime endDate)
        {
            return await _context.Attendances
                .Where(a => a.Departuredate.HasValue && a.Departuredate.Value < endDate)
                .ToListAsync();
        }

        /// <summary>
        /// Updates the attendance asynchronous.
        /// </summary>
        /// <param name="attendanceToUpdate">The attendance to update.</param>
        public async Task UpdateAttendanceAsync(Attendance attendanceToUpdate)
        {
            _context.Attendances.Update(attendanceToUpdate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates the attendance asynchronous.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        public async Task<Attendance> CreateAttendanceAsync(Attendance attendance)
        {
           // attendance.Arrivaldate = attendance.Arrivaldate.TimeOfDay
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        /// <summary>
        /// Deletes the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        public async Task DeleteAttendanceByIdAsync(int attendanceId)
        {
            var attendanceToDelete = await _context.Attendances.FindAsync(attendanceId);
            _context.Attendances.Remove(attendanceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int employeeId, DateTime startDate, DateTime? endDate)
        {
            return await _context.Attendances
                .AnyAsync(a =>
                    a.Employeeid == employeeId &&
                    a.Arrivaldate <= startDate &&
                    (!endDate.HasValue || a.Departuredate >= endDate)
                );
        }
    }
}
