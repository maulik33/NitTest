// -----------------------------------------------------------------------
// <copyright file="Address.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NursingLibrary.Entity
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class Address
    {
        public int AddressId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public Country AddressCountry { get; set; }

        public State AddressState { get; set; }

        public int Status { get; set; }

        public string Zip { get; set; }
    }
}
