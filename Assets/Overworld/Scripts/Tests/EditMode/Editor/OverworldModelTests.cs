using NUnit.Framework;
using Overworld.Core;
using Overworld.Model;

public class OverworldModelTests
{
    OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

    [Test]
    public void OverworldModel_RequiredComponentsAreNotNull()
    {
        Assert.NotNull(overworldModel.Backpack, "Backpack is null");
        Assert.IsTrue(
            overworldModel.Backpack?.gameObject.scene.IsValid(),
            "Backpack should not be set as prefab, please set instance in the scene"
        );

        Assert.NotNull(overworldModel.UICamera, "UICamera is null");
        Assert.IsTrue(
            overworldModel.Backpack?.gameObject.scene.IsValid(),
            "UICamera should not be set as prefab, please set instance in the scene"
        );
        Assert.AreEqual(overworldModel.cursorLocationStatus, OverworldModel.LocationStatus.World);
    }
}
