using System;
using System.Collections.Generic;

namespace HOSPICE_PROJEKT
{
    public partial class PatientsAdressDatum
    {
        public int PatientId { get; set; }
        public string Street { get; set; } = null!;
        public short StreetNr { get; set; }
        public string ZipCode { get; set; } = null!;
        public string Locality { get; set; } = null!;
        public short FlatNr { get; set; }
    }
}
