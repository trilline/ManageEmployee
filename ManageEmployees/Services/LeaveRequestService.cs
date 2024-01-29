using AutoMapper;
using ManageEmployees.Dtos.Leaverequest;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services
{
    /// <summary>
    /// Fournit les méthodes crud pour les objet de type LeaveRequest
    /// </summary>
    /// <seealso cref="ManageEmployees.Services.Contracts.ILeaveRequestService" />
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IStatusLeaveRequestRepository _statusLeaveRequestRepository;
        /// <summary>
        /// The leave request repository
        /// </summary>
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        /// <summary>
        /// The employee repository
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestService"/> class.
        /// </summary>
        /// <param name="leaveRequestRepository">The leave request repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">
        /// leaveRequestRepository
        /// or
        /// employeeRepository
        /// or
        /// mapper
        /// </exception>
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, IMapper mapper, IStatusLeaveRequestRepository statusLeaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository ?? throw new ArgumentNullException(nameof(leaveRequestRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _statusLeaveRequestRepository = statusLeaveRequestRepository;
        }

        /// <summary>
        /// Gets the leave requests.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadLeaveRequest>> GetLeaveRequests()
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsAsync();
            return leaveRequests.Select(lr => _mapper.Map<ReadLeaveRequest>(lr)).ToList();
        }

        /// <summary>
        /// Gets the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Leave request with ID {leaveRequestId} not found.</exception>
        public async Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId)
                ?? throw new Exception($"Leave request with ID {leaveRequestId} not found.");

            return _mapper.Map<ReadLeaveRequest>(leaveRequest);
        }

        public async Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest createLeaveRequest)
        {
            if (createLeaveRequest == null)
            {
                throw new ArgumentNullException(nameof(createLeaveRequest));
            }
            var employeetocreateemail = (await _employeeRepository.GetEmployeeByIdAsync(createLeaveRequest.EmployeeId)).Email;

            if (!await _employeeRepository.ExistAsync(employeetocreateemail))
            {
                throw new Exception($"Employee with ID {createLeaveRequest.EmployeeId} not found.");
            }

            var statusLeaveRequest = await _statusLeaveRequestRepository.GetStatusLeaveRequestByIdAsync(createLeaveRequest.StatusLeaveRequestId);

            if (!await _statusLeaveRequestRepository.ExistAsync(statusLeaveRequest.Statuslabel))
            {
                throw new Exception($"Leave Request statut with ID {createLeaveRequest.StatusLeaveRequestId} not found.");
            }

            if (createLeaveRequest.StartDate >= createLeaveRequest.EndDate)
            {
                throw new Exception("Start date must be before end date.");
            }

            if (createLeaveRequest.StartDate <= DateTime.Now || createLeaveRequest.EndDate <= DateTime.Now)
            {
                throw new Exception("You can not ask for a leave Request in the past.");
            }

            if (await _leaveRequestRepository.ExistAsync(createLeaveRequest.EmployeeId, createLeaveRequest.StartDate, createLeaveRequest.EndDate, createLeaveRequest.StatusLeaveRequestId))
            {
                throw new Exception("Leave request for the specified period already exists.");
            }

            var leaveRequestToCreate = _mapper.Map<Leaverequest>(createLeaveRequest);
            leaveRequestToCreate.Requestdate = DateTime.Now;
            leaveRequestToCreate.Statusleaverequest = statusLeaveRequest;
            var createdLeaveRequest = await _leaveRequestRepository.CreateLeaveRequestAsync(leaveRequestToCreate);

            return _mapper.Map<ReadLeaveRequest>(createdLeaveRequest);
        }

        public async Task UpdateLeaveRequestAsync(int leaveRequestId, UpdateLeaveRequest updateLeaveRequest)
        {
            if (updateLeaveRequest == null)
            {
                throw new ArgumentNullException(nameof(updateLeaveRequest));
            }

            var leaveRequestToUpdate = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId)
                ?? throw new Exception($"Leave request with ID {leaveRequestId} not found.");

            if (updateLeaveRequest.StartDate >= updateLeaveRequest.EndDate)
            {
                throw new Exception("Start date must be before end date.");
            }

            if (await _leaveRequestRepository.ExistAsync((int)leaveRequestToUpdate.Employeeid, updateLeaveRequest.StartDate, updateLeaveRequest.EndDate, updateLeaveRequest.StatusLeaveRequestId))
            {
                throw new Exception("Leave request for the specified period already exists.");
            }

            leaveRequestToUpdate.Startdate = updateLeaveRequest.StartDate;
            leaveRequestToUpdate.Enddate = updateLeaveRequest.EndDate;
            leaveRequestToUpdate.Statusleaverequestid = updateLeaveRequest.StatusLeaveRequestId;
            

            await _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequestToUpdate);
        }

        public async Task DeleteLeaveRequestByIdAsync(int leaveRequestId)
        {
            var leaveRequestToDelete = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId)
                ?? throw new Exception($"Leave request with ID {leaveRequestId} not found.");

            await _leaveRequestRepository.DeleteLeaveRequestByIdAsync(leaveRequestId);
        }

        public async Task<bool> LeaveRequestExistsAsync(int leaveRequestId)
        {
            return await _leaveRequestRepository.LeaveRequestExistsAsync(leaveRequestId);
        }
    }

}
