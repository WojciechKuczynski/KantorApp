﻿namespace KantorClient.BLL.Models
{
    public class UserModelLight
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
