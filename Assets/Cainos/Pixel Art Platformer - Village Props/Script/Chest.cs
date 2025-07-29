using UnityEngine;

namespace Cainos.PixelArtPlatformer_VillageProps
{
    public class Chest : MonoBehaviour, IInteractable
    {
        public Animator animator;

        public void Interact()
        {
            OpenAnimaition();
        }

        private void OpenAnimaition()
        {
            animator.SetBool("IsOpened", true);
        }
    }
}
