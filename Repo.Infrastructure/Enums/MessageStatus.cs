using System.ComponentModel;

namespace Repo.Infrastructure.Enums
{
    public enum MessageStatus
    {
        [Description("New")]
        New = 1,
        [Description("Sent")]
        Sent = 2,
        [Description("Received")]
        Received = 3
    }
}