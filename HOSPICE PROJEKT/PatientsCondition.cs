using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class PatientsCondition
    {
        public int TestId { get; set; }
        public int PatientId { get; set; }
        public string Condition { get; set; } = null!;

        public virtual PatientsPersonalDatum Patient { get; set; } = null!;
    }
}
