﻿namespace KantorClient.BLL.Models
{
    public class KantorModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
