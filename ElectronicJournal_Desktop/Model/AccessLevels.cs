﻿using System;
using System.Collections.Generic;

namespace ElectronicJournal_Desktop.Model
{
    public partial class AccessLevels
    {
        public AccessLevels()
        {
            Users = new HashSet<Users>();
        }

        public int AccessLevelId { get; set; }
        public string AccessLevelName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
