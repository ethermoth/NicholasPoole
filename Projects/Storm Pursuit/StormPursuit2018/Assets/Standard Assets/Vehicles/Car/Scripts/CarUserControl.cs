using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        [SerializeField] private int dejaVu;
        [SerializeField] private GameObject dejaVuObj;

        private CarController m_Car; // the car controller we want to use

           

            
        
        
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {

            if (m_Car.Skidding )
            {

                if (!dejaVuObj.GetComponent<AudioSource>().isPlaying)
                {
                    //dejaVuObj.GetComponent<AudioSource>().Play();

                }
            }
            else
            {
                dejaVuObj.GetComponent<AudioSource>().Stop();

            }
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
