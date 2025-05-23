﻿namespace TweakUIX.Tweaks
{
    public abstract class TweaksBase
    {
        /// <summary>
        /// Name of Assessment
        /// </summary>
        /// <returns>The asssessment name</returns>
        public abstract string ID();

        /// <summary>
        /// Tooltip text of sssessments
        /// </summary>
        /// <returns>The asssessment tooltip</returns>
        public abstract string Info();

        /// <summary>
        /// Checks whether assessments should be applied
        /// </summary>
        /// <returns>Returns true if the asssessment should be applied, false otherwise.</returns>
        public abstract bool CheckTweak();

        /// <summary>
        /// Do the Assessment
        /// </summary>
        /// <returns>Returns true if the asssessment was successfull, false otherwise.</returns>
        public abstract bool DoTweak();

        /// <summary>
        /// Undo the Assessment
        /// </summary>
        /// <returns>Returns true if the asssessment was successfull, false otherwise.</returns>
        public abstract bool UndoTweak();
    }
}