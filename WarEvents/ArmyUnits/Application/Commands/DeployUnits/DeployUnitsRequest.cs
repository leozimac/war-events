using ArmyUnits.Domain.Types;

namespace ArmyUnits.Application.Commands.DeployUnits;
public class DeployUnitsRequest
{
    public int Quantity { get; set; }
    public ArmyDivision Division { get; set; }
    public string Mission { get; set; } = "";
}
