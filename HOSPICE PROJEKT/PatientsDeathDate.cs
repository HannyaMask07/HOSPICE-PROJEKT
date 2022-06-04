using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class PatientsDeathDate
    {
        public int PatientId { get; set; }
        public DateTime DeathDate { get; set; }
        public string CauseOfDeath { get; set; } = null!;
    }
}
