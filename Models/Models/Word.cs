using Models.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.Enums;

namespace Models
{
    public class Word : IEntity
    {
        [NotNull]
        public string Key { get; set; }

        [NotNull]
        public string Description { get; set; }

        [NotNull]
        public string PrefixKey { get; set; }

        [PrimaryKey]
        public string Id { get; set; }

        [NotNull]
        public WordType Type { get; set; }
    }
}
