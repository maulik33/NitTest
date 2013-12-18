using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ObjectFactory
/// </summary>
public class ObjectFactory
{
    public static IKTPWebList GetKTPWebListObject(ListControl control)
    {
        return new KTPWebList(control);
    }
}