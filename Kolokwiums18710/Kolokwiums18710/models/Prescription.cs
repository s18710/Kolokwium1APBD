using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwiums18710.models
{
    public class Prescription
    {
        public int IdPrescription { get; set; }

        public String date { get; set; }
        public String dueDate { get; set; }
        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }
    }
}
