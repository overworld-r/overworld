using NUnit.Framework;
using Overworld.Core;
using Overworld.Features.Pointer;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

public class OverworldModelTests
{
    OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

    [Test]
    public void OverworldModel_RequiredComponentsAreNotNull()
    {
        GameObject _backpack = overworldModel.Backpack.Unwrap();

        Assert.NotNull(overworldModel.Backpack, "Backpack is null");
        Assert.IsTrue(
            _backpack.gameObject.scene.IsValid(),
            "Backpack should not be set as prefab, please set instance in the scene"
        );

        Assert.NotNull(overworldModel.UICamera, "UICamera is null");
        Assert.IsTrue(
            _backpack.gameObject.scene.IsValid(),
            "UICamera should not be set as prefab, please set instance in the scene"
        );

        PlayerPointer _pointer = _backpack.GetComponent<PlayerPointer>().Unwrap();
        Assert.NotNull(_pointer, "pointer is null");

        Assert.AreEqual(_pointer?.locationStatus, OverworldModel.LocationStatus.World);
    }
}
