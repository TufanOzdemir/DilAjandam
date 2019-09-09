using Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Word : IEntity
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public string PrefixKey { get; set; }
        public string Id { get; set; }
    }
}
