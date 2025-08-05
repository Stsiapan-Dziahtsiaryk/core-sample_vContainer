using Shared.Presentation;
using UnityEngine;

namespace Gameplay.Presentation.Views
{
    public class PlayerView : ViewBase
    {
        [field: SerializeField] public Rigidbody2D PhysicsBody { get; private set; }
        [field: SerializeField] public Animator Animation { get; private set; }

        public void Jump()
        {
            PhysicsBody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }
}