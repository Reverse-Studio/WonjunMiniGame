using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class RollAndFall : MonoBehaviour
    {
        public bool isFallTime = false;
        private float rollSpeed = 100;
        private Rigidbody2D rigid;
        private GameObject parent;

        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();

            parent = GameObject.Find("Cursor");

            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        IEnumerator late()
        {
            yield return new WaitForSeconds(0.5f);
            parent.GetComponent<Fallblock>().spawned = false;
        }

        private void Update()
        {
            if (isFallTime)
            {
                rigid.constraints = RigidbodyConstraints2D.None;
            }
            else
            {
                transform.position = parent.transform.position;
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * rollSpeed));
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && !isFallTime)
            {
                StartCoroutine(late());
                isFallTime = true;
            }

            if (transform.position.y < -10)
            {
                var fallBlock = parent.GetComponent<Fallblock>();

                fallBlock.score = 0;
                fallBlock.first = true;
                var wonjuns = GameObject.FindGameObjectsWithTag("wonjun");

                foreach (var wonjun in wonjuns)
                {
                    wonjun.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 100, ForceMode2D.Impulse);
                }
                Destroy(gameObject);
            }
        }
    }
}