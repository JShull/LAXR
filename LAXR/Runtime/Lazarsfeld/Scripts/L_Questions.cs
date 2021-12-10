using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace JFuzz.Lazarsfeld
{
    #region Interfaces
    public interface ILSection
    {
        void InitializePage(FL_Page PageRoot,Transform ParentObject, LSection data, FLFont Header, FLFont Subheader, FLFont Body);
        void NewFontData(string fontClass, FLFont fontData);
    }
    #endregion
    #region Enums
    public enum QuestionType
    {
        TrueFalse,
        MultipleChoice,
        Likert,
        ShortAnswer,
        Ranking,
    }
    #endregion
    #region Structs
    [Serializable]
    public struct LQuestion
    {
        [Tooltip("Label for Question")]
        public string QNumber;
        //public LSection QSection;
        [Tooltip("The actual question")]
        [TextArea(2, 4)]
        public string TheQuestion;
    }
    [Serializable]
    public struct LSection
    {
        [Tooltip("If you need to name the section")]
        public string SectionName;
        [Tooltip("If additional context is needed")]
        [TextArea(2, 6)]
        public string QuestionNarrative;
        [Tooltip("If sub narrative is needed or general question for something like a Likert")]
        [TextArea(2, 4)]
        public string QuestionSubNarrative;
        [Tooltip("This section will then be of this type")]
        public QuestionType QType;
        [Tooltip("Label Font Choice")]
        public FL_Font QuestionLabelFont;
        [Tooltip("True, False | Likert: agree, disagree, | A. B. C. D. etc.")]
        public List<string> QResponseFieldLabels;
        [Tooltip("Questions")]
        public List<LQuestion> Questions;
    }
    #endregion
    /// <summary>
    /// Root class for all Question types for Unity to placehold at runtime
    /// </summary>
    public class L_Questions : MonoBehaviour
    {
        public int QNumber;
        public int QSubNumber;
        public string QSection;
        public string TheQuestion;
        public QuestionType QType;
    }
}

