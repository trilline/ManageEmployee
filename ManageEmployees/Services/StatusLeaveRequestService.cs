using AutoMapper;
using ManageEmployees.Dtos.Statusleaverequest;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services
{
    /// <summary>
    /// fournit l'ensemble des méthodes liées au StatusLeaveRequest
    /// </summary>
    /// <seealso cref="ManageEmployees.Services.Contracts.IStatusLeaveRequestService" />
    public class StatusLeaveRequestService: IStatusLeaveRequestService
    {

        private readonly IStatusLeaveRequestRepository _statusLeaveRequestRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusLeaveRequestService"/> class.
        /// </summary>
        /// <param name="statusLeaveRequestRepository">The status leave request repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">
        /// statusLeaveRequestRepository
        /// or
        /// mapper
        /// </exception>
        public StatusLeaveRequestService(IStatusLeaveRequestRepository statusLeaveRequestRepository, IMapper mapper)
        {
            _statusLeaveRequestRepository = statusLeaveRequestRepository ?? throw new ArgumentNullException(nameof(statusLeaveRequestRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the status leave requests.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadStatusLeaveRequest>> GetStatusLeaveRequests()
        {
            var statusLeaveRequests = await _statusLeaveRequestRepository.GetStatusLeaveRequestsAsync();
            return _mapper.Map<List<ReadStatusLeaveRequest>>(statusLeaveRequests);
        }

        /// <summary>
        /// Creates the status leave request asynchronous.
        /// </summary>
        /// <param name="createStatusLeaveRequest">The create status leave request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">createStatusLeaveRequest</exception>
        /// <exception cref="System.ArgumentException">Le libellé du statut ne peut pas être null ou vide. - StatusLabel</exception>
        /// <exception cref="System.Exception">
        /// Un statut avec le libellé {createStatusLeaveRequest.StatusLabel} existe déjà.
        /// or
        /// Echec de création du statut de demande de congé.
        /// </exception>
        public async Task<ReadStatusLeaveRequest> CreateStatusLeaveRequestAsync(CreateStatusLeaveRequest createStatusLeaveRequest)
        {
            if (createStatusLeaveRequest == null)
            {
                throw new ArgumentNullException(nameof(createStatusLeaveRequest));
            }

            if (string.IsNullOrWhiteSpace(createStatusLeaveRequest.Statuslabel))
            {
                throw new ArgumentException("Le libellé du statut ne peut pas être null ou vide.", nameof(createStatusLeaveRequest.Statuslabel));
            }

            if (await _statusLeaveRequestRepository.ExistAsync(createStatusLeaveRequest.Statuslabel))
            {
                throw new Exception($"Un statut avec le libellé {createStatusLeaveRequest.Statuslabel} existe déjà.");
            }

            var statusLeaveRequestToCreate = _mapper.Map<Statusleaverequest>(createStatusLeaveRequest);

            var createdStatusLeaveRequest = await _statusLeaveRequestRepository.CreateStatusLeaveRequestAsync(statusLeaveRequestToCreate);
            return _mapper.Map<ReadStatusLeaveRequest>(createdStatusLeaveRequest)
                   ?? throw new Exception("Echec de création du statut de demande de congé.");
        }

        /// <summary>
        /// Gets the status leave request by identifier asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">statusLeaveRequestId - L'identifiant du statut de demande de congé doit être supérieur à zéro.</exception>
        public async Task<ReadStatusLeaveRequest> GetStatusLeaveRequestByIdAsync(int statusLeaveRequestId)
        {
            if (statusLeaveRequestId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(statusLeaveRequestId), "L'identifiant du statut de demande de congé doit être supérieur à zéro.");
            }

            var statusLeaveRequest = await _statusLeaveRequestRepository.GetStatusLeaveRequestByIdAsync(statusLeaveRequestId);
            return _mapper.Map<ReadStatusLeaveRequest>(statusLeaveRequest);
        }

        /// <summary>
        /// Updates the status leave request asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <param name="updateStatusLeaveRequest">The update status leave request.</param>
        /// <exception cref="System.ArgumentNullException">updateStatusLeaveRequest</exception>
        /// <exception cref="System.ArgumentException">Le libellé du statut ne peut pas être null ou vide. - StatusLabel</exception>
        /// <exception cref="System.Exception">
        /// Un autre statut avec le libellé {updateStatusLeaveRequest.StatusLabel} existe déjà.
        /// or
        /// Aucun statut de demande de congé trouvé avec l'identifiant {statusLeaveRequestId}
        /// </exception>
        public async Task UpdateStatusLeaveRequestAsync(int statusLeaveRequestId, UpdateStatusLeaveRequest updateStatusLeaveRequest)
        {
            if (updateStatusLeaveRequest == null)
            {
                throw new ArgumentNullException(nameof(updateStatusLeaveRequest));
            }

            if (string.IsNullOrWhiteSpace(updateStatusLeaveRequest.Statuslabel))
            {
                throw new ArgumentException("Le libellé du statut ne peut pas être null ou vide.", nameof(updateStatusLeaveRequest.Statuslabel));
            }

            var existingStatusLeaveRequest = await _statusLeaveRequestRepository.GetStatusLeaveRequestByLabelAsync(updateStatusLeaveRequest.Statuslabel);

            if (existingStatusLeaveRequest != null && existingStatusLeaveRequest.Statusleaverequestid != statusLeaveRequestId)
            {
                throw new Exception($"Un autre statut avec le libellé {updateStatusLeaveRequest.Statuslabel} existe déjà.");
            }

            var statusLeaveRequestToUpdate = await _statusLeaveRequestRepository.GetStatusLeaveRequestByIdAsync(statusLeaveRequestId)
                ?? throw new Exception($"Aucun statut de demande de congé trouvé avec l'identifiant {statusLeaveRequestId}");

            _mapper.Map(updateStatusLeaveRequest, statusLeaveRequestToUpdate);

            await _statusLeaveRequestRepository.UpdateStatusLeaveRequestAsync(statusLeaveRequestToUpdate);
        }

        /// <summary>
        /// Deletes the status leave request by identifier asynchronous.
        /// </summary>
        /// <param name="statusLeaveRequestId">The status leave request identifier.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">statusLeaveRequestId - L'identifiant du statut de demande de congé doit être supérieur à zéro.</exception>
        /// <exception cref="System.Exception">Aucun statut de demande de congé trouvé avec l'identifiant {statusLeaveRequestId}</exception>
        public async Task DeleteStatusLeaveRequestByIdAsync(int statusLeaveRequestId)
        {
            if (statusLeaveRequestId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(statusLeaveRequestId), "L'identifiant du statut de demande de congé doit être supérieur à zéro.");
            }

            var statusLeaveRequestToDelete = await _statusLeaveRequestRepository.GetStatusLeaveRequestByIdAsync(statusLeaveRequestId)
                ?? throw new Exception($"Aucun statut de demande de congé trouvé avec l'identifiant {statusLeaveRequestId}");

            await _statusLeaveRequestRepository.DeleteStatusLeaveRequestByIdAsync(statusLeaveRequestId);
        }

    }
}
