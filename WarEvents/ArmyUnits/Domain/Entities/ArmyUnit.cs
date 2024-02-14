using ArmyUnits.Domain.Types;

namespace ArmyUnits.Domain.Entities;
public class ArmyUnit
{
    public Guid DeploymentId { get; set; }
    public int Quantity { get; set; }
    public ArmyDivision Division { get; set; }
    public string Mission { get; set; } = "";
}