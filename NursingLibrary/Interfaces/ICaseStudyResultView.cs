using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NursingLibrary.Interfaces
{
    public interface ICaseStudyResultView
    {
        void WriteResponse(XmlDocument responseDoc);
    }
}
