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

        public void UpdateRecord(string diagnosis, string treatment, string notes) // method to update record information
        {
            this.Diagnosis = diagnosis; // update diagnosis information
            this.Treatment = treatment; // update treatment information
            this.Notes = notes; // update additional notes
        }
     }
}
