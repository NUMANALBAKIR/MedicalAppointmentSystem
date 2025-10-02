using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IDataService
    {
        Task<List<DoctorDTO>> GetDoctors();
        Task<List<MedicineDTO>> GetMedicines();
        Task<List<PatientDTO>> GetPatients();
    }
}