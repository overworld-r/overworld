using System.Collections;
using NUnit.Framework;
using Overworld.Core;
using Overworld.Item.Functions;
using Overworld.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Overworld.Tests
{
    public class ItemPointerTests
    {
        private GameObject? itemPointer;
        private GameObject? gameObject;
        private ItemPointer? itemPointerComponent;
        bool sceneLoading = true;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [OneTimeSetUp]
        public void Setup()
        {
            sceneLoading = true;
            SceneManager.LoadSceneAsync("PlayerPlayfield").completed += _ =>
            {
                sceneLoading = false;
                var itemPointer = GameObject.Find("ItemManager");
                Debug.Log("Scene Load Complete");
            };
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(gameObject);
        }

        [UnityTest]
        [Order(-100)]
        public IEnumerator シーンのロード完了()
        {
            yield return new WaitWhile(() => sceneLoading);
        }

        [UnityTest]
        public IEnumerator OverworldModelにBackpackがアサインされている()
        {
            Assert.IsNotNull(overworldModel.Backpack, "OverworldModelのBackpackがnullです");
            yield return null;
        }

        [UnityTest]
        public IEnumerator ItemPointerに必要なコンポーネントがアタッチされている()
        {
            var itemPointer = GameObject.Find("ItemManager");
            Assert.IsNotNull(itemPointer, "ItemManagerがシーンに存在しません");

            Assert.IsNotNull(
                itemPointer?.GetComponent<ItemClickHandler>(),
                "ItemManagerにItemClickHandlerがアタッチされていません"
            );
            Assert.IsNotNull(
                itemPointer?.GetComponent<ItemMover>(),
                "ItemManagerにItemMoverがアタッチされていません"
            );

            Assert.IsNotNull(
                itemPointer?.GetComponent<ItemRotator>(),
                "ItemManagerにItemMoverがアタッチされていません"
            );

            Assert.IsNotNull(
                itemPointer?.GetComponent<ItemLocationManager>(),
                "ItemManagerにItemLocationManagerがアタッチされていません"
            );

            Assert.IsNotNull(
                itemPointer?.GetComponent<CursorLocationManager>(),
                "ItemManagerにCursorLocationManagerがアタッチされていません"
            );
            yield return null;
        }

        [UnityTest]
        public IEnumerator ItemManagerのResetが機能している()
        {
            gameObject = new GameObject();
            itemPointerComponent = gameObject.AddComponent<ItemPointer>();
            Assert.IsNotNull(itemPointerComponent, "ItemManagerがnullです。");

            yield return null;

            itemPointerComponent?.SendMessage("Reset");

            yield return null;

            Assert.IsNotNull(
                itemPointerComponent?.GetComponent<ItemClickHandler>(),
                "ItemClickHandler should not be null"
            );
            Assert.IsNotNull(
                itemPointerComponent?.GetComponent<ItemMover>(),
                "ItemMover should not be null"
            );
            Assert.IsNotNull(
                itemPointerComponent?.GetComponent<ItemRotator>(),
                "ItemRotator should not be null"
            );

            var itemLocationManager = itemPointerComponent?.GetComponent<ItemLocationManager>();
            itemLocationManager?.SendMessage("Reset");

            yield return null;

            Assert.IsNotNull(itemLocationManager, "ItemLocationManager should not be null");

            Assert.IsNotNull(
                itemPointerComponent?.GetComponent<CursorLocationManager>(),
                "CursorLocationManager should not be null"
            );
        }
    }
}
