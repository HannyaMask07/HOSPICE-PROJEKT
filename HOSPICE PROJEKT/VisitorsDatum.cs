using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class VisitorsDatum
    {
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string DegOfKinship { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public virtual PatientsPersonalDatum Patient { get; set; } = null!;
    }
}
