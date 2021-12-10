using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
namespace JFuzz.Lazarsfeld
{
    /// <summary>
    /// Used to hold Unity UI information for Likert style questions
    /// </summary>
    public class L_Likert : MonoBehaviour, ILSection
    {
        /// <summary>
        /// Hold data 
        /// </summary>
        private LSection _qInfo;
        [Header("Textual Likert Related")]
        public TextMeshProUGUI SectionTitle;
        public TextMeshProUGUI Narrative;
        public TextMeshProUGUI SubNarrative;

        public GameObject QPrefab;
        public GameObject LabelPrefab;
        public GameObject AnswerPrefab;
        public GameObject RadioButtonPrefab;
        [Space]
        [Header("Likert Panel Related")]
        public RectTransform LikertLabelPanel;
        public GridLayoutGroup LabelGrid;
        [Space]
        [Header("Question Related")]
        public RectTransform LikertQPanel;
        public GridLayoutGroup QGrid;
        [Space]
        [Header("Answer Related")]
        public RectTransform LikertAPanel;
        public GridLayoutGroup AGrid;
        private FL_Page pageRef;
        private List<Toggle> _allRadioButtons = new List<Toggle>();
        

        public void InitializePage(FL_Page pageHome, Transform ParentObject, LSection data, FLFont headerFont, FLFont narrativeFont, FLFont subNarrativeFont)
        {
            pageRef = pageHome;
            GetComponent<RectTransform>().anchorMin = Vector2.zero;
            GetComponent<RectTransform>().anchorMax = Vector2.one;
            _qInfo = data;
            headerFont.FontData = _qInfo.SectionName;
            if (SectionTitle != null)
            {

                pageHome.PassFontData(headerFont, SectionTitle);
            }
            narrativeFont.FontData = _qInfo.QuestionNarrative;
            if (Narrative != null)
            {
                pageHome.PassFontData(narrativeFont, Narrative);
            }
            subNarrativeFont.FontData = _qInfo.QuestionSubNarrative;
            if (SubNarrative != null)
            {
                pageHome.PassFontData(subNarrativeFont, SubNarrative);
            }
            
            //setup question section
            int numLabels = _qInfo.QResponseFieldLabels.Count;
            LabelGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            LabelGrid.constraintCount = numLabels;
            var cellWidth = LikertLabelPanel.rect.width / numLabels;
            var cellHeight = LikertLabelPanel.rect.height;
            LabelGrid.cellSize = new Vector2(cellWidth, cellHeight);
            for (int i = 0; i < numLabels; i++)
            {
                var aLabel = GameObject.Instantiate(LabelPrefab, LikertLabelPanel);
                var textC = aLabel.GetComponent<L_Label>();
                //Debug.LogWarning($"Generating a Label");
                textC.LabelBackdrop.color = pageHome.PageTheme.FooterColor;
                //update font with style
                pageHome.PassFontData(_qInfo.QuestionLabelFont.Font, textC.Label);
                //update text with label information
                
                textC.Label.text = _qInfo.QResponseFieldLabels[i];
                //Debug.LogWarning($"Finished Label");
            }
            AGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            AGrid.constraintCount = data.Questions.Count;
            QGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            QGrid.constraintCount = data.Questions.Count;
            QGrid.cellSize = new Vector2(LikertQPanel.rect.width, LikertQPanel.rect.height / data.Questions.Count);
            AGrid.cellSize = new Vector2(LikertLabelPanel.rect.width, LikertAPanel.rect.height / data.Questions.Count);
            for (int i = 0; i < data.Questions.Count;i++)
            {
                //question part
                var aQuestion = GameObject.Instantiate(QPrefab, LikertQPanel);
                var qText = aQuestion.GetComponent<L_Label>();
                pageHome.PassFontData(_qInfo.QuestionLabelFont.Font, qText.Label);
                var question = data.Questions[i].TheQuestion;
                qText.Label.text = data.Questions[i].TheQuestion;
                //answer part = radio buttons / need radio group
                var aAnswer = GameObject.Instantiate(AnswerPrefab, LikertAPanel);
                aAnswer.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                aAnswer.GetComponent<GridLayoutGroup>().constraintCount = numLabels;
                aAnswer.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellWidth, cellHeight);
                
                for (int x = 0; x < numLabels; x++)
                {
                    var _response = _qInfo.QResponseFieldLabels[x];
                    var aRadioB = GameObject.Instantiate(RadioButtonPrefab, aAnswer.GetComponent<RectTransform>());
                    aRadioB.GetComponent<Toggle>().group = aAnswer.GetComponent<ToggleGroup>();
                    var toggleR = aRadioB.GetComponent<Toggle>();
                    toggleR.onValueChanged.AddListener((value)  => RadioButtonChanged(value,headerFont.FontData, question, _response));
                    if (x == 0)
                    {
                        aRadioB.GetComponent<Toggle>().isOn = true;
                    }
                    _allRadioButtons.Add(aRadioB.GetComponent<Toggle>());
                }
               

            }
            //running top
        }
        public void RadioButtonChanged(bool toggleValue,string section, string question, string response)
        {
            if (toggleValue)
            {
                L_Survey.Instance.LikertToggleEvent(section, question, response);
            }
           
        }
        void OnDestroy()
        {
            foreach(var rButton in _allRadioButtons)
            {
                rButton.onValueChanged.RemoveAllListeners();
            }
        }
        
        public void NewFontData(string fontClass, FLFont fontData)
        {
            throw new System.NotImplementedException();
        }

    }
}

