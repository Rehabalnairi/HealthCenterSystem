using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


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
            //patient.Records.Add(record); // Link directly to the patient

            Console.WriteLine(" Medical record added successfully.");
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
                //record.Patient.Records.Remove(record); // Remove from patient's list too
                return true;
            }
            return false;
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var record in records)
                {
                    string line = $"{record.RecordId}|{record.Patient.UserId}|{record.Doctor.UserId}|{record.VisitDate:yyyy-MM-dd}|{record.Diagnosis}|{record.Treatment}|{record.Notes}";
                    writer.WriteLine(line);
                }
            }
        }

        public void LoadFromFile(string filePath, List<Patient> patients, List<User> users)
        {
            records = new List<PatientRecord>();

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length != 7)
                    continue;

                int recordId = int.Parse(parts[0]);
                int patientId = int.Parse(parts[1]);
                int doctorId = int.Parse(parts[2]);
                DateTime visitDate = DateTime.Parse(parts[3]);
                string diagnosis = parts[4];
                string treatment = parts[5];
                string notes = parts[6];

                Patient patient = patients.FirstOrDefault(p => p.UserId == patientId);
                Doctor doctor = users.OfType<Doctor>().FirstOrDefault(d => d.UserId == doctorId);

                if (patient != null && doctor != null)
                {
                    PatientRecord record = new PatientRecord(recordId, patient, doctor, visitDate, diagnosis, treatment, notes);
                    records.Add(record);
                }
            }
        }

    }
}

