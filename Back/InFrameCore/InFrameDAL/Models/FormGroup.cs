﻿using System;
using System.Collections.Generic;

namespace InFrameDAL.Models
{
    public partial class FormGroup
    {
        public FormGroup()
        {
            FormField = new HashSet<FormField>();
        }

        public long Id { get; set; }
        public long FormConfigId { get; set; }
        public string Title { get; set; }
        public int ColumnIndex { get; set; }
        public int GroupOrder { get; set; }
        public bool Active { get; set; }
        public string CssClass { get; set; }
        public int Behavior { get; set; }

        public virtual FormConfig FormConfig { get; set; }
        public virtual ICollection<FormField> FormField { get; set; }
    }
}
