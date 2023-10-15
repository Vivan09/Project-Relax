using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Room
{
    public class ChangeObjects : MonoBehaviour
    {
        public SaveData saveData;
        public SpriteRenderer renderer;
        public List<SpriteRenderer> sprites;

        public int i;
        private void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeMaterial()
        {
            if (sprites.Count - 1 == i)
            {
                i = 0;
                renderer.sprite = sprites[0].sprite;
            }
            else
            {
                i++;
            }    

            if (renderer.sprite.name == sprites[i].sprite.name)
            {
                if (sprites.Count - 1 == i)
                {
                    i = 0;
                    renderer.sprite = sprites[i].sprite;
                }
                else
                {
                    renderer.sprite = sprites[i + 1].sprite;
                }
            }
            else
                renderer.sprite = sprites[i].sprite;

            renderer.sprite = sprites[i].sprite;
            saveData.Save();
        }

        public void SetIndex(int index)
        {
            if (index > sprites.Count - 1)
                index = 0;

            i = index;
            renderer.sprite = sprites[index].sprite;
        }
    }
}