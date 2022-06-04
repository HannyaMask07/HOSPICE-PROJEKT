using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class PatientsPersonalDatum
    {
        public PatientsPersonalDatum()
        {
            PatientsConditions = new HashSet<PatientsCondition>();
            PatientsFees = new HashSet<PatientsFee>();
            PatientsMedications = new HashSet<PatientsMedication>();
            VisitorsData = new HashSet<VisitorsDatum>();
        }

        public int PatientId { get; set; }
        public string? Pesel { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public virtual HospiceRoom HospiceRoom { get; set; } = null!;
        public virtual ICollection<PatientsCondition> PatientsConditions { get; set; }
        public virtual ICollection<PatientsFee> PatientsFees { get; set; }
        public virtual ICollection<PatientsMedication> PatientsMedications { get; set; }
        public virtual ICollection<VisitorsDatum> VisitorsData { get; set; }
    }
}
