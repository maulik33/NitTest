using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.DAO
{
    public class LippincottComparer : IEqualityComparer<Lippincott>
    {
        #region Methods

        public bool Equals(Lippincott x, Lippincott y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.LippincottID == y.LippincottID;
        }

        public int GetHashCode(Lippincott obj)
        {
            return obj.LippincottID.GetHashCode();
        }

        #endregion Methods
    }
}
