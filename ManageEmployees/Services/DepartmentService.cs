using AutoMapper;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Repositories;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;

        }

        public async Task<List<ReadDepartment>> GetDepartments()
        {
            var departments = await _departmentRepository.GetDepartmentsAsync();
            List<ReadDepartment> readDepartments = new List<ReadDepartment>();

            foreach(var department in departments)
            {
                readDepartments.Add(new ReadDepartment()
                {
                    Id = department.Departmentid,
                    Name = department.Name,
                    Description = department.Description,
                });
            }

            return readDepartments;
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _departmentRepository.GetDepartmentByIDAsync(departmentId);

            if(department is null)
            {
                throw new Exception($"Echec de récupération des informations d'un departement car il n'existe pas : {departmentId}");
            }

            return department;
        }

        public async Task<ReadDepartment> GetDepartmentByNameAsync(string name)
        {
            var department = await _departmentRepository.GetDepartmentByNameAsync(name);

            if (department is null)
            {
                throw new Exception($"Echec de récupération des informations d'un departement car il n'existe pas de departement avec ce nom : {name}");
            }

            return new ReadDepartment()
            {
                Id = department.Departmentid,
                Name = department.Name,
                Description = department.Description,
            };
        }


        public async Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department)
        {
            var departmentGet = await _departmentRepository.GetDepartmentByIDAsync(departmentId)
                ?? throw new Exception($"Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}");

            var departmentGetByName = await _departmentRepository.GetDepartmentByNameAsync(department.Name);
            if (departmentGetByName is not null && departmentId != departmentGetByName.Departmentid)
            {
                throw new Exception($"Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}");
            }

            departmentGet.Name = department.Name;
            departmentGet.Description = department.Description;
            departmentGet.Address = department.Address;

            await _departmentRepository.UpdateDepartmentAsync(departmentGet);

        }

        public async Task DeleteDepartmentByIdAsync(int departmentId)
        {
            var departmentGet = await _departmentRepository.GetDepartmentByIdWithIncludeAsync(departmentId)
              ?? throw new Exception($"Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}");

            if (departmentGet.Employees.Any())
            {
                throw new Exception("Echec de suppression car ce departement est lié à des employés");
            }

            await _departmentRepository.DeleteDepartmentByIdAsync(departmentId);
        }

        public async Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment createDepartment)
        {

            var departmentGet = await _departmentRepository.GetDepartmentByNameAsync(createDepartment.Name);
            if (departmentGet is not null)
            {
                throw new Exception($"Echec de création d'un département : Il existe déjà un département avec ce nom {createDepartment.Name}");
            }


            var DepartementToCreate = _mapper.Map<Department>(createDepartment);

            var createdDepartment = await _departmentRepository.CreateDepartmentAsync(DepartementToCreate);
            return _mapper.Map<ReadDepartment>(createdDepartment) ?? throw new Exception("Echec de création d'un departement");
        }

        //Todo : A supprimer
        //var DepartementToCreate = new Department()

        //    {
        //        Name = createDepartment.Name,
        //        Description = createDepartment.Description,
        //        Address = createDepartment.Address,
        //    };
        //    var departmentCreated = await _departmentRepository.CreateDepartmentAsync(DepartementToCreate);

        //    return new ReadDepartment()
        //    {
        //        Id = departmentCreated.Departmentid,
        //        Name = departmentCreated.Name,
        //    };
        //}
    }
}
