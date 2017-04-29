using System.ComponentModel;

namespace Repo.UI.OperationType
{
    public enum OperationType
    {
        [Description("Role")]
        Role = 1,
        [Description("User")]
        User = 2,
        [Description("Message")]
        Message = 3
    }
}