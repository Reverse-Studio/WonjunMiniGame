using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMove : MonoBehaviour
    {
        private Vector3 toMove;
        private Vector3 velocity;
        void Update()
        {
            var wonjuns = GameObject.FindGameObjectsWithTag("wonjun");

            float max = -10.0f;

            foreach (var wonjun in wonjuns)
            {
                if (wonjun.GetComponent<RollAndFall>().isFallTime)
                {
                    if (wonjun.GetComponent<Rigidbody2D>().velocity.y >= -0.1)
                    {
                        var y = wonjun.transform.position.y;
                        max = max < y ? y : max;
                    }
                }
            }

            toMove.Set(0, max + 5f, -10);
        }

        private void FixedUpdate()
        {
            velocity = (toMove - transform.position) * Time.deltaTime;

            transform.position += velocity;
        }

        
    }
}