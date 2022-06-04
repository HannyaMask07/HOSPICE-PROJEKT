using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class PatientsMedication
    {
        public int TreatmentId { get; set; }
        public int PatientId { get; set; }
        public string MedicationId { get; set; } = null!;
        public string DosageOfMed { get; set; } = null!;

        public virtual PatientsPersonalDatum Patient { get; set; } = null!;
    }
}
