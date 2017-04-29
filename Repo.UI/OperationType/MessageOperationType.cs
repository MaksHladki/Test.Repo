using System.ComponentModel;

namespace Repo.UI.OperationType
{
    public enum MessageOperationType
    {
        [Description("Send")]
        Send = 1,
        [Description("Receive")]
        Receive = 2,
        [Description("Receive all")]
        ReceiveAll = 3,
        [Description("Get all")]
        GetAll = 4
    }
}
