using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCenterSystem.Models
{
    public class PatientRecordService
    {
        private List<PatientRecord> records = new List<PatientRecord>();

        // Add a medical record and link it to the patient
        public void AddRecord(Patient patient, Doctor doctor, string diagnosis, string treatment, string notes)
        {
            int newId = records.Count + 1;
            DateTime visitDate = DateTime.Now;

            PatientRecord record = new PatientRecord(newId, patient, doctor, visitDate, diagnosis, treatment, notes);

            records.Add(record);
            patient.Records.Add(record); // Link directly to the patient

            Console.WriteLine("✔ Medical record added successfully.");
        }

        // Get all records for a specific patient
        public List<PatientRecord> GetRecordsForPatient(Patient patient)
        {
            return records.Where(r => r.Patient.UserId == patient.UserId).ToList();
        }

        // Optional: Get all records (admin use case)
        public List<PatientRecord> GetAllRecords()
        {
            return records;
        }

        // Optional: Update an existing record
        public bool UpdateRecord(int recordId, string diagnosis, string treatment, string notes)
        {
            var record = records.FirstOrDefault(r => r.RecordId == recordId);
            if (record == null)
                return false;

            record.UpdateRecord(diagnosis, treatment, notes);
            return true;
        }

        // Optional: Delete a record
        public bool DeleteRecord(int recordId)
        {
            var record = records.FirstOrDefault(r => r.RecordId == recordId);
            if (record != null)
            {
                records.Remove(record);
                record.Patient.Records.Remove(record); // Remove from patient's list too
                return true;
            }
            return false;
        }
    }
}
