using System.Collections.Generic;

namespace ElectronicJournal_Desktop.Model.Data
{
    public partial class Buildings
    {
        public Buildings()
        {
            Classrooms = new HashSet<Classrooms>();
        }

        public int BuildingId { get; set; }
        public string BuildingName { get; set; }

        public virtual ICollection<Classrooms> Classrooms { get; set; }
    }
}
