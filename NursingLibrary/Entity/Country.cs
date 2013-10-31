// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="Microsoft">
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
    public class Country
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
