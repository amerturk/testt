using System;
using System.ComponentModel;
using SQLite;
using WhiteMvvm.Utilities;

namespace WhiteMvvm.Bases
{
    public class BaseModel : NotifiedObject
    {
        [PrimaryKey, AutoIncrement]
        public int? BaseId { get; set; }
        public string Id { get; set; }
    }
}
