using NUnit.Framework;
using Overworld.Core;
using Overworld.Features.Pointer;
using Overworld.Models;

public class OverworldModelTests
{
    OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

    [Test]
    public void OverworldModel_RequiredComponentsAreNotNull()
    {
        var backpack = overworldModel.Backpack;

        Assert.NotNull(overworldModel.Backpack, "Backpack is null");
        Assert.IsTrue(
            backpack.gameObject.scene.IsValid(),
            "Backpack should not be set as prefab, please set instance in the scene"
        );

        Assert.NotNull(overworldModel.UICamera, "UICamera is null");
        Assert.IsTrue(
            backpack.gameObject.scene.IsValid(),
            "UICamera should not be set as prefab, please set instance in the scene"
        );

        var pointer = backpack.GetComponent<PlayerPointer>();
        Assert.NotNull(pointer, "pointer is null");

        Assert.AreEqual(pointer.locationStatus, OverworldModel.LocationStatus.World);
    }
}
