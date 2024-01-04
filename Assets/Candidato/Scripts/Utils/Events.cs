using UnityEngine.Events;

namespace Candidato.Scripts.Utils
{
    public static class Events
    {
        #region eventosUI
        
        /// <summary>
        ///   <para>This event is called when there is an update on the points.</para>
        /// </summary>
        public static event UnityAction<int> ScoreIncrease;
        
        /// <summary>
        ///   <para>Invoke method of event ScoreIncrease</para>
        /// </summary>
        /// <param name="value">Value of the increment of score.</param>
        public static void OnScoreIncrease(int value) => ScoreIncrease?.Invoke(value);
        #endregion
    }
}


