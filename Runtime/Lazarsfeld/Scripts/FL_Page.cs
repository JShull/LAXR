using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

namespace JFuzz.Lazarsfeld
{
    //base class for all UI items associated with a 'page' and the UI requirements for that page
    
    public class FL_Page : MonoBehaviour, IFLPageUpdate
    {
        [Header("Data Related")]
        [Space]
        public UnityEvent DataChanged;
        [Header("Page Index Information")]
        public Transform ParentObject;
       
        public int ZIndex;
        public string PageName;
        [Space]
        [Header("Question Related")]
        public GameObject LikertQuestionPrefab;
        [Header("References")]
        public Canvas PageCanvas;
        public RectTransform PageRoot;
        public RectTransform PageHeaderRoot;
        public RectTransform PageBodyRoot;
        public RectTransform PageFooterRoot;
        public RectTransform ImageRoot;
        public RectTransform EventRoot;
        public RectTransform TitleRoot;
        public RectTransform SubheaderRoot;
        public RectTransform IconRoot;
        [Header("Image based")]
        public Image PageRootBackdrop;
        public Image PageHeaderBackdrop;
        public Image PageBodyBackdrop;
        public Image PageFooterBackdrop;
        public Image PageHeaderIcon;
        public FL_Image PageImageRoot;
        //public List<Sprite> PageImages = new List<Sprite>();

        [Header("Text Based")]
        public TextMeshProUGUI PageHeaderTitle;
        public TextMeshProUGUI PageSubheader;
        public TextMeshProUGUI AdditionalText;
        public TextMeshProUGUI PageFooterText;

        public FLPage PageData;
        public FLTheme PageTheme;
        [Header("Event Buttons")]
        public Button PageEventButton;
        [Header("Prefabs")]
        public GameObject ContainerPrefab;

        [Header("Data")]
        public List<FL_Container> ContainerObjects = new List<FL_Container>();
        [SerializeField]
        public Dictionary<string, FLFont> FontObjects = new Dictionary<string, FLFont>();
        
        #region interfaces   
        public void NewFontData(string fontThemeName,FLFont fontData)
        {
            if (FontObjects.ContainsKey(fontThemeName))
            {
                Debug.LogWarning($"Already have font class in my memory");
            }
            else
            {
                FLFont newObject = new FLFont
                {
                    FontAsset = fontData.FontAsset,
                    FontColor = fontData.FontColor,
                    FontData = fontData.FontData,
                    FontSize = fontData.FontSize,
                    FontType = fontData.FontType
                };

                FontObjects.Add(fontThemeName,newObject);
                CycleThroughFontChanges();
            }

            //update all nested objects
            
        }
        public void NewContainerData(RectTransform ParentObject, bool useParentTheme, FLFont fontData)
        {
            var flContainer = GameObject.Instantiate(ContainerPrefab, ParentObject);
            if (flContainer.GetComponent<FL_Container>())
            {
                var containerdata = flContainer.GetComponent<FL_Container>();
                containerdata.Initialize(ParentObject, useParentTheme);
                containerdata.InitializeFont(fontData);
                ContainerObjects.Add(containerdata);
            }
            CycleThroughFontChanges();

        }
        public void QuestionSetupLikert(LSection questionData)
        {
            GameObject LikertSectionPrefab = GameObject.Instantiate(LikertQuestionPrefab, PageBodyRoot);
            LikertSectionPrefab.GetComponent<L_Likert>().InitializePage(this,PageBodyRoot, questionData, PageData.SubHeaderFont, PageData.BodyFont, PageData.FooterFont);
        }
        
        public void InitializePage(Transform parentObject, FLPage pageData, string pageName, FLTheme themeData, bool WorldCanvas, Camera theCam)
        {
            if (WorldCanvas)
            {
                PageCanvas.renderMode = RenderMode.WorldSpace;
                PageCanvas.worldCamera = theCam;
            }
            
            PageName = pageName;
            ParentObject = parentObject;
            this.transform.SetParent(ParentObject);
            PageData = pageData;
            PageTheme = themeData;
            this.gameObject.name = "Page_" + PageName;
            ZIndex = PageData.ZIndex;
            
            if (PageData.Icon != null)
            {
                PageHeaderIcon.sprite = PageData.Icon;
                IconRoot.anchorMin = PageData.RectIcon.AnchorMin;
                IconRoot.anchorMax = PageData.RectIcon.AnchorMax;
                TitleRoot.anchorMin = PageData.RectTitle.AnchorMin;
                TitleRoot.anchorMax = PageData.RectTitle.AnchorMax;
                SubheaderRoot.anchorMin = PageData.RectSubheader.AnchorMin;
                SubheaderRoot.anchorMax = PageData.RectSubheader.AnchorMax;
            }
            else
            {
                //don't use ICON adjust anchors
                IconRoot.anchorMin = new Vector2(0, 0);
                IconRoot.anchorMax = new Vector2(0,0);
                //stretch title
                TitleRoot.anchorMin = new Vector2(PageData.RectIcon.AnchorMin.x,PageData.RectTitle.AnchorMin.y);
                TitleRoot.anchorMax = PageData.RectTitle.AnchorMax;
                SubheaderRoot.anchorMin = new Vector2(PageData.RectIcon.AnchorMin.x,PageData.RectSubheader.AnchorMin.y);
                SubheaderRoot.anchorMax = PageData.RectSubheader.AnchorMax;
                //ImageRoot.anchorMax = PageData.RectImage.AnchorMax;
                //PageBodyRoot.anchorMin = PageData.RectBodyWithImage.AnchorMin;
                //PageBodyRoot.anchorMax = PageData.RectBodyWithImage.AnchorMax;
            }

            if (PageData.PageRootBackdrop != null)
            {
                PageRootBackdrop.sprite = PageData.PageRootBackdrop;
                PageRootBackdrop.type = Image.Type.Simple;
                PageRootBackdrop.preserveAspect = true;
            }
            PageRootBackdrop.color = PageData.RootColor;

            if (PageData.PageHeaderBackdrop != null)
            {
                PageHeaderBackdrop.sprite = PageData.PageHeaderBackdrop;
            }
            PageHeaderBackdrop.color = PageData.HeaderColor;

            if (PageData.PageBodyBackdrop != null)
            {
                PageBodyBackdrop.sprite = PageData.PageBodyBackdrop;
            }
            PageBodyBackdrop.color = PageData.BodyColor;

            if (PageData.PageFooterBackdrop != null)
            {
                PageFooterBackdrop.sprite = PageData.PageFooterBackdrop;
            }
            if (PageData.AddedImages.Images.Length > 0)
            {
                //process images here
                //PageImages.Clear();
                ImageRoot.anchorMin = PageData.RectImage.AnchorMin;
                ImageRoot.anchorMax = PageData.RectImage.AnchorMax;
                PageBodyRoot.anchorMin = PageData.RectBodyWithImage.AnchorMin;
                PageBodyRoot.anchorMax = PageData.RectBodyWithImage.AnchorMax;
                PageImageRoot.SetupImageViewer(PageData.AddedImages);

                //build out the images now using two container prefabs
            }
            else
            {
                PageImageRoot.SetupNoImages();
                //default
                PageBodyRoot.anchorMax = PageData.RectBodyWithNOImage.AnchorMax;
                PageBodyRoot.anchorMin = PageData.RectBodyWithNOImage.AnchorMin;
                ImageRoot.anchorMin = Vector2.zero;
                ImageRoot.anchorMax = Vector2.zero;
            }
            PageFooterBackdrop.color = PageData.FooterColor;
            //rebuild font objects
            FontObjects.Clear();
            if (PageData.HeaderFont.FontAsset!=null)
            {
                if (!pageData.HeaderFont.OverrideTheme)
                {
                    //use theme color
                    PageData.HeaderFont.FontColor = PageTheme.HeaderFontColor;
                }
                
                NewFontData(FontStyles.Header.ToString(), PageData.HeaderFont);
            }
            if (PageData.SubHeaderFont.FontAsset != null)
            {
                if (!PageData.SubHeaderFont.OverrideTheme)
                {
                    //use theme color
                    PageData.SubHeaderFont.FontColor = PageTheme.SubHeaderFontColor;
                }
                NewFontData(FontStyles.SubHeader.ToString(), PageData.SubHeaderFont);
            }
            if (PageData.BodyFont.FontAsset != null)
            {
                if (!PageData.BodyFont.OverrideTheme)
                {
                    //use theme color
                    PageData.BodyFont.FontColor = PageTheme.MainFontColor;
                }
                NewFontData(FontStyles.Body.ToString(), PageData.BodyFont);
            }
            if (PageData.FooterFont.FontAsset != null)
            {
                if (!PageData.FooterFont.OverrideTheme)
                {
                    //use theme color
                    PageData.FooterFont.FontColor = PageTheme.FooterFontColor;
                }
                NewFontData(FontStyles.Footer.ToString(), PageData.FooterFont);
            }
            if (pageData.PageEvent.DisplayName!="Blank")
            {
                //we have a display name
                PageEventButton.onClick.AddListener(InvokePageEvent);
                PageEventButton.GetComponentInChildren<TextMeshProUGUI>().text = pageData.PageEvent.DisplayName;
                EventRoot.anchorMin = PageData.RectEvent.AnchorMin;
                EventRoot.anchorMax = PageData.RectEvent.AnchorMax;
                PageFooterRoot.anchorMax = PageData.RectFooter.AnchorMax;
                PageFooterRoot.anchorMin = PageData.RectFooter.AnchorMin;
            }
            else
            {
                EventRoot.gameObject.SetActive(false);
                PageFooterRoot.anchorMax = PageData.RectFooterNOEvent.AnchorMax;
                PageFooterRoot.anchorMin = PageData.RectFooterNOEvent.AnchorMin;
            }
            PassFontData(PageData.HeaderFont, PageHeaderTitle);
            PassFontData(PageData.FooterFont, PageFooterText);
            PassFontData(PageData.SubHeaderFont, PageSubheader);
            PassFontData(PageData.BodyFont, AdditionalText);

        }
        public void PassFontData(FLFont fontData,TextMeshProUGUI VisualFont)
        {
            VisualFont.font = fontData.FontAsset;
            VisualFont.color = fontData.FontColor;
            VisualFont.text = fontData.FontData;
            VisualFont.fontSize = fontData.FontSize;
            VisualFont.enableAutoSizing = fontData.AutoScale;
            VisualFont.fontSizeMin = fontData.MinFontSize;
            VisualFont.fontSizeMax = fontData.MaxFontSize;
            VisualFont.UpdateFontAsset();
        }
        public void InvokePageEvent()
        {
            if (PageData.PageEvent.DisplayName != "")
            {
                switch (PageData.PageEvent.EventType)
                {
                    case PageEvents.LoadScene:
                        if (SceneManager.GetSceneByName(PageData.PageEvent.EventDetails) != null)
                        {
                            try
                            {
                                SceneManager.LoadScene(PageData.PageEvent.EventDetails);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError($"Didn't find the scene{ex.Message}");
                            }
                        }
                        
                        break;
                    case PageEvents.MoreInformation:
                        break;
                }
            }
        }
        public void OnDisable()
        {
            if (PageData.PageEvent.DisplayName != "")
            {
                PageEventButton.onClick.RemoveListener(InvokePageEvent);
            }   
        }
        public void OnEnable()
        {
            if (PageData.PageEvent.DisplayName != "")
            {
                PageEventButton.onClick.AddListener(InvokePageEvent);
            }
        }
        public void OnIconUpdate(Sprite newIcon)
        {
            PageData.Icon = newIcon;
            PageHeaderIcon.sprite = newIcon;
        }
        #endregion
        public void GrabStarted()
        {
            Debug.LogWarning($"Grab started");

        }
        public void DataFieldChanged(string questionfield,string answerfield)
        {

        }
        public void GrabHeld()
        {
            Debug.LogWarning($"Grab held");
        }
        public void EndGrab()
        {
            Debug.LogWarning($"Grab ended");
        }
        private void CycleThroughFontChanges()
        {
            foreach (var cObject in ContainerObjects)
            {
                if (cObject is IFLFont)
                {
                    if (FontObjects.ContainsKey(cObject.FontThemeName))
                    {
                        var themeData = FontObjects[cObject.FontThemeName];
                        cObject.CopyVisualData(themeData);
                    }
                }
            }
        }
    }
}

