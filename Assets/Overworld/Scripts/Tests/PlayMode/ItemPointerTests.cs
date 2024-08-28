using System.Collections;
using NUnit.Framework;
using Overworld.Core;
using Overworld.Features.Pointer;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Overworld.Tests
{
    public class ItemPointerTests
    {
        private GameObject? itemPointer;
        private GameObject? gameObject;
        bool sceneLoading = true;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [OneTimeSetUp]
        public void Setup()
        {
            sceneLoading = true;
            SceneManager.LoadSceneAsync("PlayerPlayfield").completed += _ =>
            {
                sceneLoading = false;
                var itemPointer = GameObject.Find("Pointer");
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
        public IEnumerator Pointerに必要なコンポーネントがアタッチされている()
        {
            GameObject _playerPointer = GameObject.Find("Pointer");
            Assert.IsNotNull(itemPointer, "Pointerがシーンに存在しません");

            Assert.IsNotNull(
                _playerPointer.GetComponent<PointerClickHandler>(),
                "ItemManagerにItemClickHandlerがアタッチされていません"
            );
            Assert.IsNotNull(
                _playerPointer.GetComponent<PointerMover>(),
                "ItemManagerにItemMoverがアタッチされていません"
            );

            Assert.IsNotNull(
                _playerPointer.GetComponent<PointerRotator>(),
                "ItemManagerにItemMoverがアタッチされていません"
            );

            Assert.IsNotNull(
                _playerPointer.GetComponent<PointerLocationManager>(),
                "ItemManagerにPointerLocationManagerがアタッチされていません"
            );
            yield return null;
        }

        [UnityTest]
        public IEnumerator ItemManagerのResetが機能している()
        {
            gameObject = new GameObject();
            PlayerPointer _playerPointerComponent = gameObject.AddComponent<PlayerPointer>();
            Assert.IsNotNull(_playerPointerComponent, "ItemManagerがnullです。");

            yield return null;

            _playerPointerComponent.SendMessage("Reset");

            yield return null;

            Assert.IsNotNull(
                _playerPointerComponent.GetComponent<PointerClickHandler>(),
                "ItemClickHandler should not be null"
            );
            Assert.IsNotNull(
                _playerPointerComponent.GetComponent<PointerMover>(),
                "ItemMover should not be null"
            );
            Assert.IsNotNull(
                _playerPointerComponent.GetComponent<PointerRotator>(),
                "ItemRotator should not be null"
            );

            yield return null;

            Assert.IsNotNull(
                _playerPointerComponent.GetComponent<PointerLocationManager>(),
                "CursorLocationManager should not be null"
            );
        }
    }
}
