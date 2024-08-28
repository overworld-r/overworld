using Overworld.Features.Item.Models;
using Overworld.Mechanics.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    public class Droppable : MonoBehaviour, Models.IBreakable
    {
        void Update() { }

        void Models.IBreakable.OnBreak()
        {
            GenerateResource(this.GetComponent<IResourceMetadata>().metadata);
        }

        void GenerateResource(ResourceMetadata meta)
        {
            for (int i = 0; i < meta.stone; i++)
            {
                GameObject stone = Instantiate(
                    Resources.Load<GameObject>("GameResourcePrefabs/stone")
                );
                Vector3 random = new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f),
                    0.0f
                );
                stone.transform.position = this.transform.position + random;
                stone.GetComponent<Rigidbody2D>().Push(random);
            }

            for (int i = 0; i < meta.wood; i++)
            {
                GameObject wood = Instantiate(
                    Resources.Load<GameObject>("GameResourcePrefabs/wood")
                );
                Vector3 random = new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f),
                    0.0f
                );
                wood.transform.position = this.transform.position + random;
                wood.GetComponent<Rigidbody2D>().Push(random);
            }

            for (int i = 0; i < meta.iron; i++)
            {
                GameObject iron = Instantiate(
                    Resources.Load<GameObject>("GameResourcePrefabs/iron")
                );
                Vector3 random = new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f),
                    0.0f
                );
                iron.transform.position = this.transform.position + random;
                iron.GetComponent<Rigidbody2D>().Push(random);
            }
        }
    }
}
