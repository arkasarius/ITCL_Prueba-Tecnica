using System;
using TMPro;
using UnityEngine;

namespace Candidato.Scripts.Utils
{
    public class PointsFeedback : MonoBehaviour
    {
        public TMP_Text text;
        
        /// <summary>
        ///   <para>Modify the Prefab text including a + sign before for feedback.</para>
        /// </summary>
        /// <param name="points">Points to display on the TMP_Text for feedback</param>
        public void SetPoints(int points)
        {
            text.text = "+" + points.ToString();
        }

        private void Awake()
        {
            Destroy(this.gameObject,.8f);
        }
        
        /// <summary>
        ///   <para>Updates feedback GameObject location over time for a nicer effect.</para>
        /// </summary>
        private void Update()
        {
            var position = transform.position;
            position = new Vector3(position.x, position.y + 0.4f * Time.deltaTime, position.z);
            transform.position = position;
        }
    }
}
