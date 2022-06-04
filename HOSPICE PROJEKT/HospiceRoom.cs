using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class HospiceRoom
    {
        public short BedId { get; set; }
        public int PatientId { get; set; }
        public short RoomNr { get; set; }

        public virtual PatientsPersonalDatum Patient { get; set; } = null!;
    }
}
