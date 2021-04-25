using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U3Gear.Playground
{
/// <summary>
    /// Mechanize the rotation between two points causing pendulum effect.
    /// This script should be placed at the pivot of the object.
    /// </summary>
    public class Pendulum : MonoBehaviour
    {
        /// ==================================================
        /// Private Variables
        /// ==================================================

        // Assigns to the pendulum rotation a start limit and an end limit to create the pendulum effect.
        private Quaternion _start, _end;

        /// ==================================================
        /// Private Visible Variables
        /// ==================================================

        [SerializeField, Range(0.0f, 5.0f), Tooltip("The speed of the pendulum.")]
        private float _speed = 2.0f;

        [SerializeField, Range(0.0f, 10.0f), Tooltip("The moment of departure of the pendulum, creating a different moment in relation to the other pendulums.")]
        private float _startTime = 0.0f;

        [SerializeField, Range(0.0f, 360.0f), Tooltip("The direction of the pendulum in degrees.")]
        private float _pendulumDirection = 0.0f;

        [SerializeField, Range(0.0f, 90.0f), Tooltip("The amplitude of rotation of the pendulum in degrees. Influence speed at low amplitudes.")]
        private float _pendulumAmplitude = 90.0f;

        /// ==================================================
        /// Unity Methods
        /// ==================================================

        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// Like the Awake function, Start is called exactly once in the lifetime of the script
        /// </summary>
        private void Start()
        {
            Init();
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// FixedUpdate should be used instead of Update when dealing with Rigidbody.
        /// For example when adding a force to a rigidbody, you have to apply the force every
        /// fixed frame inside FixedUpdate instead of every frame inside Update.
        /// </summary>
        private void FixedUpdate()
        {
            // Assigns the time in seconds it took to complete the last frame (Time.deltaTime) to the varivale _startTime.
            _startTime += Time.deltaTime;

            // Assigns the rotation interpolated between _start and _end by sine of _startTime * _speed
            // to the transform.rotation and normalizes the result afterwards.
            transform.rotation = Quaternion.Lerp(_start, _end, (Mathf.Sin(_startTime * _speed + Mathf.PI / 2) + 1.0f) / 2.0f);
        }

        /// ==================================================
        /// Methods
        /// ==================================================

        /// <summary>
        /// Initializes the Unity components.
        /// </summary>
        private void Init()
        {
            // Normalizes the direction of rotation of the pendulum.
            this.transform.rotation = Quaternion.Euler(0, _pendulumDirection, 0);

            // Normalizes the amplitude of the start point of the rotation of the pendulum.
            _start = PendulumRotation(_pendulumAmplitude);

            // Normalizes the amplitude of the end point of the rotation of the pendulum.
            _end = PendulumRotation(-_pendulumAmplitude);
        }

        /// <summary>
        /// Pendulum rotation mechanics. Unity internally uses Quaternions to represent all rotations.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>pendulumRotation</returns>
        private Quaternion PendulumRotation(float angle)
        {
            var pendulumRotation = transform.rotation;
            var angleZ = pendulumRotation.z + angle;

            if (angleZ > 180)
                angleZ -= 360;
            else if (angleZ < -180)
                angleZ += 360;

            pendulumRotation.eulerAngles = new Vector3(pendulumRotation.eulerAngles.x, pendulumRotation.eulerAngles.y, angleZ);
            return pendulumRotation;
        }
    }
}
