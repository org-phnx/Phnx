﻿namespace Phnx.Data.Tests.LazyLoad.TestData
{
    internal class Person : IIdDataModel<int>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}