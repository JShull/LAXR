
using UnityEngine;
using UnityEngine.Events;
using System;

namespace JFuzz.Lazarsfeld
{
    public class L_Survey: MonoBehaviour
    {
        /// <summary>
        /// Quick Singleton for demo purposes
        /// </summary>
        private static L_Survey _instance;

        public static L_Survey Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public GameObject PagePrefab;
       
        [Space]
        private GameObject _pageInstance;
        public Lz_Likert QuestionDetails;
        public Transform AlignUIPosition;
        public Vector3 OffsetPanelPosition;
        public Camera MainCam;
        public UnityEvent OnStartEvent;
        public UnityEvent AfterPageSetupEvent;
        private FL_Page ExampleData;
        public UnityEvent DataEvent;
        #region DEMO
        public string CurrentLabel { get { return _lastResponse; } }
        public string CurrentQuestion { get { return _lastQuestion; } }
        public string CurrentSection { get { return _lastSection; } }
        #endregion

        private string _lastSection;
        private string _lastQuestion;
        private string _lastResponse;

        public void Start()
        {
            OnStartEvent.Invoke();
        }
        /// <summary>
        /// For Demo Purposes
        /// </summary>
        /// <param name="label"></param>
        /// <param name="question"></param>
        public void LikertToggleEvent(string section, string question, string response)
        {
            _lastSection = section;
            _lastQuestion = question;
            _lastResponse = response;
            DataEvent.Invoke();
            Debug.Log($"Respone Changed: {_lastSection},{_lastQuestion},{_lastResponse}");
        }

        /// <summary>
        /// Generic Page Setup
        /// </summary>
        public void SetupPage()
        {
            _pageInstance = GameObject.Instantiate(PagePrefab);
            
            if (_pageInstance.GetComponent<FL_Page>())
            {
                
                var ExampleData = _pageInstance.GetComponent<FL_Page>();
                if (MainCam != null)
                {
                    ExampleData.PageCanvas.worldCamera = MainCam;
                }
                try
                {
                    FLPage tempPagedata = new()
                    {
                        Icon = QuestionDetails.Theme.ThemeInfo.Icon,
                        PageRootBackdrop = QuestionDetails.Theme.ThemeInfo.BackdropBorderImage,
                        BodyColor = QuestionDetails.Theme.ThemeInfo.BodyColor,
                        HeaderColor = QuestionDetails.Theme.ThemeInfo.HeaderColor,
                        FooterColor = QuestionDetails.Theme.ThemeInfo.FooterColor,
                        RootColor = QuestionDetails.Theme.ThemeInfo.RootColor,
                        BodyFont = QuestionDetails.BodyFont.Font,
                        FooterFont = QuestionDetails.FooterFont.Font,
                        HeaderFont = QuestionDetails.HeaderFont.Font,
                        SubHeaderFont = QuestionDetails.SubHeaderFont.Font,
                        RectIcon = QuestionDetails.HeaderIconAnchors,
                        RectTitle = QuestionDetails.HeaderAnchors,
                        RectSubheader = QuestionDetails.SubheaderAnchors,
                        RectBodyWithImage = QuestionDetails.BodyAnchors,
                        RectBodyWithNOImage = QuestionDetails.BodyAnchorsNoImage,
                        RectImage = QuestionDetails.ImageAnchors,
                        //PageEvent = QuestionDetails.EventData,
                        RectFooter = QuestionDetails.FooterAnchors,
                        //RectEvent = QuestionDetails.EventAnchor,
                        RectFooterNOEvent = QuestionDetails.FooterAnchors,
                        AddedImages = QuestionDetails.ImageData,
                    };
                    //manual adjustments to get the right data to the right placeholder --> need to fix this
                    tempPagedata.HeaderFont.FontData = QuestionDetails.PageName;
                    tempPagedata.SubHeaderFont.FontData = QuestionDetails.HeaderInformation;
                    tempPagedata.FooterFont.FontData = QuestionDetails.FooterInformation;

                    ExampleData.InitializePage(AlignUIPosition, tempPagedata, QuestionDetails.PageName, QuestionDetails.Theme.ThemeInfo);
                    _pageInstance.transform.position = AlignUIPosition.transform.position + OffsetPanelPosition;
                    _pageInstance.transform.SetParent(null);
                    if (QuestionDetails.QuestionSection.QType== QuestionType.Likert)
                    {
                        ExampleData.QuestionSetupLikert(QuestionDetails.QuestionSection);
                    }
                }
                catch(Exception ex)
                {
                    Debug.LogError($"More than likely missing other references to Themes/Fonts: {ex.Message}");
                }
                AfterPageSetupEvent.Invoke();
                
            }
            
        }
    }
}