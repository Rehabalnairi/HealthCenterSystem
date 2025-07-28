using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IPatientRecordService
    {
        void AddPatientRecord(PatientRecord record); // method to add a patient record
        void UpdatePatientRecord(int recordId, Patient patient, Doctor doctor, DateTime visitDate, string diagnosis, string treatment, string notes); // method to update a patient record
        PatientRecord GetPatientRecordById(int recordId); // method to get a patient record by ID
        List<PatientRecord> GetAllPatientRecords(); // method to get all patient records

        List<PatientRecord> GetRecordsByPatientId(int patientId); // method to get records by patient ID
        List<PatientRecord> GetRecordsByDoctorId(int doctorId); // method to get records by doctor ID
        void DeletePatientRecord(int recordId); // method to delete a patient record
    }
}
