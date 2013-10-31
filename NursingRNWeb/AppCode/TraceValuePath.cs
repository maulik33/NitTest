using System.Web.UI.WebControls;
using NursingLibrary.Entity;

public class TraceValuePath
{
    public TraceValuePath(TreeNode node)
    {
        string[] parts = node.ValuePath.Split('/');

        if (parts.Length > 1)
        {
            DateString = string.Format("{0}/{1}/{2}", parts[1], parts[2], parts[3]);
        }

        if (parts.Length > 4)
        {
            UserId = GetUserId(parts[4]);
        }

        if (parts.Length > 5)
        {
            SessionId = parts[5];
        }
    }

    public string UserId { get; set; }

    public string DateString { get; set; }

    public string SessionId { get; set; }

    public static string GetUserId(string userKey)
    {
        return userKey.Substring(userKey.IndexOf("(") + 1, userKey.IndexOf(")") - userKey.IndexOf("(") - 1);
    }

    public static string GetUserKey(TraceInfo entity)
    {
        return string.Format("{0}({1})", entity.UserName, entity.UserId);
    }
}