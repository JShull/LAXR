using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace JFuzz.Lazarsfeld
{
    //MonoBehaviour class to setup a simple Panel Information Page using existing data structures for in world canvas

    public class FL_InfoPanel : MonoBehaviour, IPageSetup
    {
        [Header("Data for the Information Page")]
        public Lz_Panel PageDetails;
       
        public bool InWorldCanvasObject;
        public bool KeepParentObject;
        public Camera TheCamera;
        public Transform PlacementOfPageLocation;
        public Vector3 AdditionalOffsetInformation;
        [Space]
        [Tooltip("The prefab that contains all of the components in their aligned structures")]
        public GameObject PagePrefab;
        private GameObject _pageInstance;
        [Space]
        public UnityEvent OnStartEvent;
        public UnityEvent AfterPageSetupEvent;
        public void Start()
        {
            OnStartEvent.Invoke();
            SetupPanelData(TheCamera, PlacementOfPageLocation, AdditionalOffsetInformation, InWorldCanvasObject, KeepParentObject);
        }

        #region IPageSetup Interface Methods
        /// <summary>
        /// Whatever sort of code we need to do for setup
        /// </summary>
        public void SetupPanelData(Camera MainCam,Transform AlignUIPosition, Vector3 OffsetPanelPosition, bool CanvasInWorld, bool KeepParent)
        {
            _pageInstance = GameObject.Instantiate(PagePrefab);

            if (_pageInstance.GetComponent<FL_Page>())
            {
                var ExampleData = _pageInstance.GetComponent<FL_Page>();
                try
                {
                    
                    FLPage tempPagedata = new FLPage
                    {
                        Icon = PageDetails.Theme.ThemeInfo.Icon,
                        PageRootBackdrop = PageDetails.Theme.ThemeInfo.BackdropBorderImage,
                        BodyColor = PageDetails.Theme.ThemeInfo.BodyColor,
                        HeaderColor = PageDetails.Theme.ThemeInfo.HeaderColor,
                        FooterColor = PageDetails.Theme.ThemeInfo.FooterColor,
                        RootColor = PageDetails.Theme.ThemeInfo.BackGroundColor,
                        BodyFont = PageDetails.BodyFont.Font,
                        FooterFont = PageDetails.FooterFont.Font,
                        HeaderFont = PageDetails.HeaderFont.Font,
                        SubHeaderFont = PageDetails.SubHeaderFont.Font,
                        RectIcon = PageDetails.HeaderIconAnchors,
                        RectTitle = PageDetails.HeaderAnchors,
                        RectSubheader = PageDetails.SubheaderAnchors,
                        RectBodyWithImage = PageDetails.BodyAnchors,
                        RectBodyWithNOImage = PageDetails.BodyAnchorsNoImage,
                        RectImage = PageDetails.ImageAnchors,
                        PageEvent = PageDetails.EventData,
                        RectFooter = PageDetails.FooterAnchors,
                        RectEvent = PageDetails.EventAnchor,
                        RectFooterNOEvent = PageDetails.FooterAnchorNoEvent,
                        AddedImages = PageDetails.ImageData,
                        
                    };
                    //manual adjustments to get the right data to the right placeholder --> need to fix this
                    tempPagedata.HeaderFont.FontData = PageDetails.PageName;
                    tempPagedata.SubHeaderFont.FontData = PageDetails.HeaderInformation;
                    tempPagedata.FooterFont.FontData = PageDetails.FooterInformation;
                    tempPagedata.BodyFont.FontData = PageDetails.BodyInformation;
                    ExampleData.InitializePage(AlignUIPosition, tempPagedata, PageDetails.PageName, PageDetails.Theme.ThemeInfo, CanvasInWorld, MainCam);
                    _pageInstance.transform.position = AlignUIPosition.transform.position + OffsetPanelPosition;
                    if (!KeepParent)
                    {
                        _pageInstance.transform.SetParent(null);
                    }
                   
                    
                }
                catch (Exception ex)
                {
                    Debug.LogError($"More than likely missing other references to Themes/Fonts: {ex.Message}");
                }
                AfterPageSetupEvent.Invoke();
            }
        }

        #endregion
    }
}
