using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U3Gear.Playground
{
    /// <summary>
    /// Alternate movement of a platform between two points.
    /// This script should be attached to the object that will be used as a mobile platform.
    /// </summary>
    public class SimplePlatform : MonoBehaviour
    {
        /// ==================================================
        /// Private Variables
        /// ==================================================

        // Change of direction of movement of the platform when it reaches its destination.
        private bool _switch = false;

        /// ==================================================
        /// Private Visible Variables
        /// ==================================================

        [SerializeField, Tooltip("Starting point of the platform, its origin.")]
        private Transform _origin;

        [SerializeField, Tooltip("Point of destination of the platform.")]
        private Transform _destination;

        [SerializeField, Tooltip("Platform movement speed.")]
        private float _speed = 0.03f;

        [SerializeField, Tooltip("Hold time before the platform initiates movement.")]
        private float _holdtime = 3.0f;

        /// ==================================================
        /// Unity Methods
        /// ==================================================

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// FixedUpdate should be used instead of Update when dealing with Rigidbody.
        /// For example when adding a force to a rigidbody, you have to apply the force every
        /// fixed frame inside FixedUpdate instead of every frame inside Update.
        /// </summary>
        private void FixedUpdate()
        {
            // Start the coroutine to hold time before starting the move.
            StartCoroutine(nameof(HoldTime));

            // Changing the direction of movement of the platform.
            transform.position = Vector3.MoveTowards(transform.position, _switch ? _origin.position : _destination.position, _speed);
        }

        /// <summary>
        /// Coroutine to implement hold time, pause, before starting the the movement of the platform.
        /// </summary>
        /// <returns>_holdtime</returns>
        private IEnumerator HoldTime()
        {
            // When it reaches the destination, the hold time is activated.
            if (transform.position != _destination.position) yield break;
            // Uses the _holdtime variable value.
            yield return new WaitForSeconds(_holdtime);
            // After the hold time has elapsed it activates the change of direction and starts the movement.
            _switch = true;

            // When it reaches the origin, the hold time is activated.
            if (transform.position != _origin.position) yield break;
            // Uses the _holdtime variable value.
            yield return new WaitForSeconds(_holdtime);
            // After the hold time has elapsed it starts the movement.
            _switch = false;
        }
    }
}
