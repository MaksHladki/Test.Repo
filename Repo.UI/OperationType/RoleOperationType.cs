using System.ComponentModel;

namespace Repo.UI.OperationType
{
    public enum RoleOperationType
    {
        [Description("Get all")]
        GetAll = 1,
        [Description("Get by name")]
        GetByName = 2,
        [Description("Create")]
        Create = 3,
        [Description("Update")]
        Update = 4,
        [Description("Delete")]
        Delete = 5,
        [Description("Associate with user")]
        AssociateWithUser = 6,
        [Description("UnAssociate with user")]
        UnAssociateWithUser = 7
    }
}