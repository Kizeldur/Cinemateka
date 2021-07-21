using System;
using System.Collections.Generic;

#nullable disable

namespace Cinemateka
{
    public partial class TableCinemateka
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string MovieTitle { get; set; }
        public string LeadActor { get; set; }
        public string Director { get; set; }

    }
}
