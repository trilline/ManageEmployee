using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public AttendanceService(IAttendanceRepository attendanceRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        public async Task<List<ReadAttendance>> GetAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAttendancesAsync();
            return _mapper.Map<List<ReadAttendance>>(attendances);
        }

        public async Task<ReadAttendance> GetAttendanceByIdAsync(int attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIDAsync(attendanceId);
            return _mapper.Map<ReadAttendance>(attendance) ?? throw new Exception($"Echec de récupération de la présence avec l'identifiant {attendanceId}.");
        }

        public async Task<List<ReadAttendance>> GetAttendanceBetweenTwoDatesAsync(DateTime startDate, DateTime endDate)
        {
            var attendances = await _attendanceRepository.GetAttendanceBetweenTwoDatesAsync(startDate, endDate);
            return _mapper.Map<List<ReadAttendance>>(attendances) ?? throw new Exception("Echec de récupération des présences pour la période spécifiée.");
        }

        public async Task<List<ReadAttendance>> GetAttendanceStartingBeforeDateAsync(DateTime startDate)
        {
            var attendances = await _attendanceRepository.GetAttendanceStartingBeforeDateAsync(startDate);
            return _mapper.Map<List<ReadAttendance>>(attendances) ?? throw new Exception("Echec de récupération des présences pour la période spécifiée.");
        }

        public async Task<List<ReadAttendance>> GetAttendanceEndingBeforeDateAsync(DateTime endDate)
        {
            var attendances = await _attendanceRepository.GetAttendanceEndingBeforeDateAsync(endDate);
            return _mapper.Map<List<ReadAttendance>>(attendances) ?? throw new Exception("Echec de récupération des présences pour la période spécifiée.");
        }

        public async Task UpdateAttendanceAsync(int attendanceId, UpdateAttendance updateAttendance)
        {
            if (updateAttendance == null)
            {
                throw new ArgumentNullException(nameof(updateAttendance));
            }

            var attendanceToUpdate = await _attendanceRepository.GetAttendanceByIDAsync(attendanceId);
            if (attendanceToUpdate == null)
            {
                throw new Exception($"Echec de mise à jour d'une assiduité : Aucune assiduité avec l'identifiant {attendanceId} trouvée.");
            }

            // Vérif si une assiduité similaire existe déjà pour cet employé et cette période
            var exists = await _attendanceRepository.ExistsAsync(
                (int)attendanceToUpdate.Employeeid,
                updateAttendance.Arrivaldate,
                updateAttendance.Departuredate
            );

            if (exists)
            {
                throw new Exception("Echec de mise à jour de l'assiduité : Une assiduité similaire existe déjà pour cet employé et cette période.");
            }

            // Maj
            attendanceToUpdate.Employeeid = updateAttendance.EmployeeId;
            attendanceToUpdate.Arrivaldate = updateAttendance.Arrivaldate;
            attendanceToUpdate.Departuredate = updateAttendance.Departuredate;

            await _attendanceRepository.UpdateAttendanceAsync(attendanceToUpdate);
        }

        public async Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance createAttendance)
        {
            if (createAttendance == null)
            {
                throw new ArgumentNullException(nameof(createAttendance));
            }

            // Vérif si Arrivaldate est nulle
            if (createAttendance.Arrivaldate == DateTime.MinValue)
            {
                throw new ArgumentException("Arrivaldate ne peut pas être nulle lors de la création d'une assiduité.");
            }
            var employeeId = createAttendance.EmployeeId; 

            // Vérif si une présence existe déjà pour cet employé et cette période
            var exists = await _attendanceRepository.ExistsAsync(
                employeeId,
                createAttendance.Arrivaldate,
                createAttendance.Departuredate
            );

            if (exists)
            {
                throw new Exception("Echec de création de la  présence : Une présence existe déjà pour cet employé et cette période.");
            }

            var attendanceToCreate = _mapper.Map<Attendance>(createAttendance);

            var createdAttendance = await _attendanceRepository.CreateAttendanceAsync(attendanceToCreate);
            return _mapper.Map<ReadAttendance>(createdAttendance) ?? throw new Exception("Echec de création de la présence.");
        }

        public async Task DeleteAttendanceByIdAsync(int attendanceId)
        {
            var attendanceToDelete = await _attendanceRepository.GetAttendanceByIDAsync(attendanceId);
            if (attendanceToDelete == null)
            {
                throw new Exception($"Echec de suppression d'une présence : Aucune assiduité avec l'identifiant {attendanceId} trouvée.");
            }

            await _attendanceRepository.DeleteAttendanceByIdAsync(attendanceId);
        }


    }
}
