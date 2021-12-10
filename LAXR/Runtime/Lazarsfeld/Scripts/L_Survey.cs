
using UnityEngine;
using UnityEngine.Events;
using System;

namespace JFuzz.Lazarsfeld
{
    public class L_Survey: MonoBehaviour
    {
        public GameObject PagePrefab;
       
        [Space]
        private GameObject _pageInstance;
        public Lz_Likert QuestionDetails;
        public Transform AlignUIPosition;
        public Vector3 OffsetPanelPosition;
        public Camera MainCam;
        public UnityEvent OnStartEvent;
        public UnityEvent AfterPageSetupEvent;

        public void Start()
        {
            OnStartEvent.Invoke();
        }
        /// <summary>
        /// Generic Page Setup
        /// </summary>
        public void SetupPage()
        {
            _pageInstance = GameObject.Instantiate(PagePrefab);
            
            if (_pageInstance.GetComponent<FL_Page>())
            {
                
                var pageInfo = _pageInstance.GetComponent<FL_Page>();
                if (MainCam != null)
                {
                    pageInfo.PageCanvas.worldCamera = MainCam;
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

                    pageInfo.InitializePage(AlignUIPosition, tempPagedata, QuestionDetails.PageName, QuestionDetails.Theme.ThemeInfo);
                    _pageInstance.transform.position = AlignUIPosition.transform.position + OffsetPanelPosition;
                    _pageInstance.transform.SetParent(null);
                    if (QuestionDetails.QuestionSection.QType== QuestionType.Likert)
                    {
                        pageInfo.QuestionSetupLikert(QuestionDetails.QuestionSection);
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