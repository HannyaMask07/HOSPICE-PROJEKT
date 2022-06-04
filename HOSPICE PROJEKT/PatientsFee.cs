using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class PatientsFee
    {
        public int FeeId { get; set; }
        public int PatientId { get; set; }
        public decimal FeeAmount { get; set; }

        public virtual PatientsPersonalDatum Patient { get; set; } = null!;
    }
}
