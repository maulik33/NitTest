using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    /// <summary>
    /// Represents Cohort by Test Entity Class
    /// </summary>
    public class CohortByTest
    {
        /// <summary>
        /// Gets or sets the cohort id.
        /// </summary>
        /// <value>The cohort id.</value>
        public int CohortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the cohort.
        /// </summary>
        /// <value>The name of the cohort.</value>
        public string CohortName { get; set; }

        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        /// <value>The group id.</value>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName { get; set; }
       
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        /// <value>The product ID.</value>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the institution ID.
        /// </summary>
        /// <value>The institution ID.</value>
        public int InstitutionID { get; set; }

        /// <summary>
        /// Gets or sets the test id.
        /// </summary>
        /// <value>The test id.</value>
        public int TestId { get; set; }

        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <value>The name of the test.</value>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the normed perc correct.
        /// </summary>
        /// <value>The normed perc correct.</value>
        public float NormedPercCorrect { get; set; }

        /// <summary>
        /// Gets or sets the percantage.
        /// </summary>
        /// <value>The percantage.</value>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the No. of students.
        /// </summary>
        /// <value>No. of students.</value>
        public int NStudents { get; set; }
    }
}
