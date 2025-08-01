using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
     public class PatientRecord
    {
        public int RecordId { get; set; } // property to hold record ID
        public Patient Patient { get; set; }// property to hold patient information
        public Doctor Doctor { get; set; } // property to hold doctor information
        public DateTime VisitDate { get; set; } // property to hold visit date
        public string Diagnosis { get; set; } // property to hold diagnosis information
        public string Treatment { get; set; } // property to hold treatment information
        public string Notes { get; set; } // property to hold additional notes
        //
        public PatientRecord(int recordId, Patient patient, Doctor doctor, DateTime visitDate, string diagnosis, string treatment, string notes)
        {
            this.RecordId = recordId; // initialize record ID
            this.Patient = patient; // initialize patient information
            this.Doctor = doctor; // initialize doctor information
            this.VisitDate = visitDate; // initialize visit date
            this.Diagnosis = diagnosis; // initialize diagnosis information
            this.Treatment = treatment; // initialize treatment information
            this.Notes = notes; // initialize additional notes
        }

        public string ToString() // override ToString method to return record information
        {
            return $"Record ID: {RecordId}, Patient: {Patient.Name}, Doctor: {Doctor.Name}, Visit Date: {VisitDate.ToShortDateString()}, Diagnosis: {Diagnosis}, Treatment: {Treatment}, Notes: {Notes}";
        }

        public string ToFileString()
        {
            return $"{RecordId}|{Patient.UserId}|{Doctor.UserId}|{VisitDate:yyyy-MM-dd}|{Diagnosis}|{Treatment}|{Notes}";
        }

        public static PatientRecord FromFileString(string line, List<Patient> patients, List<User> users)
        {
            string[] parts = line.Split('|');
            if (parts.Length != 7) return null;

            int patientId = int.Parse(parts[1]);
            int doctorId = int.Parse(parts[2]);

            Patient patient = patients.FirstOrDefault(p => p.UserId == patientId);
            Doctor doctor = users.OfType<Doctor>().FirstOrDefault(d => d.UserId == doctorId);

            if (patient == null || doctor == null) return null;

            return new PatientRecord(
                int.Parse(parts[0]),
                patient,
                doctor,
                DateTime.Parse(parts[3]),
                parts[4],
                parts[5],
                parts[6]
            );
        }

        public void UpdateRecord(string diagnosis, string treatment, string notes) // method to update record information
        {
            this.Diagnosis = diagnosis; // update diagnosis information
            this.Treatment = treatment; // update treatment information
            this.Notes = notes; // update additional notes
        }
     }
}
