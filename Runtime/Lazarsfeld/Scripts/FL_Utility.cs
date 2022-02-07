using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System.Linq;
using UnityEngine.Events;

namespace JFuzz.Lazarsfeld
{
    #region Interfaces
    public interface IFLPageUpdate
    {
        void NewFontData(string fontClass,FLFont fontData);
        void OnIconUpdate(Sprite newIcon);
        void NewContainerData(RectTransform ParentObject, bool useParentTheme, FLFont fontData);
        void InitializePage(Transform ParentObject, FLPage pageData, string pageName, FLTheme themeData, bool worldCam, Camera theCamera);
    }

    public interface IFLContainerUpdate
    {
        void OnHeaderUpdate(string newHeader);
        void OnBodyUpdate(string newBody);
        void OnContainerColorUpdate(Color baseColor);
        void OnAnchorUpdate(FLAnchors anchorData);
        void Initialize(RectTransform parentContainer, bool useParentTheme);
    }
    /// <summary>
    /// Interface to manage one way to always setup these page/data objects
    /// </summary>
    public interface IPageSetup
    {
        void SetupPanelData(Camera MainCam, Transform LocationOfPlacement, Vector3 LocationAdditionalOffset, bool WorldCanvas, bool KeepParent);
    }
    /// <summary>
    /// Interface for all font based information and exchange of information
    /// </summary>
    public interface IFLFont
    {
        void InitializeFont(FLFont fontData);
        void FontColorUpdate(Color fontColor, FontType fontType);
        void FontAssetUpdate(TMP_FontAsset fontAsset, FontType fontType);
        void FontSizeUpdate(float baseSize, FontType fontType);
        void FontDataUpdate(string fontData, FontType fontType);
        void CopyVisualData(FLFont fontData);
    }
    #endregion
    #region Enums
    public enum FontType
    {
        Header,
        SubHeader,
        Base,
        Footer,
    }
    public enum FontStyles
    {
        Header,
        SubHeader,
        Body,
        Footer, 
    }
    public enum PageEvents
    {
        LoadScene,
        MoreInformation,
    }
    #endregion
    #region Data Structs
    [Serializable]
    public struct FLEvent
    {
        public string EventName;
        public string DisplayName;
        public PageEvents EventType;
        [Tooltip("If the event is loading a scene this is the scene name to load")]
        public string EventDetails;
    }
    [Serializable]
    public struct FLAnchors
    {
        public Vector2 AnchorPosition;
        public Vector2 AnchorMin;
        public Vector2 AnchorMax;
    }
    [Serializable]
    public struct FLFont
    {
        public FontType FontType;
        public TMP_FontAsset FontAsset;
        public bool OverrideTheme;
        public Color FontColor;
        public float FontSize;
        public bool AutoScale;
        public float MinFontSize;
        public float MaxFontSize;
        public string FontData;
    }
    [Serializable]
    public struct FLImage
    {
        public Color ImageBorder;
        public bool GridBased;
        public Sprite[] Images;
    }
    [Serializable]
    public struct FLTheme
    {
        [Tooltip("Icon Will Appear next to Header Text")]
        public Sprite Icon;
        [Tooltip("Image maintains aspect ratio")]
        public Sprite BackdropBorderImage;
        [Header("Background Related Colors")]
        [Tooltip("Main Background Color to Root Canvas")]
        public Color BackGroundColor;
        [Tooltip("Color of Header Background")]
        public Color HeaderColor;
        [Tooltip("Color of Body Background")]
        public Color BodyColor;
        [Tooltip("Color of Footer Background")]
        public Color FooterColor;
        [Space]
        [Header("Font Related Colors")]
        [Tooltip("Color of Header Font")]
        public Color HeaderFontColor;
        [Tooltip("Color of Sub-Header Font")]
        public Color SubHeaderFontColor;
        [Tooltip("Color of Main Font")]
        public Color MainFontColor;
        [Tooltip("Color of Footer Font")]
        public Color FooterFontColor;
    }
    [Serializable]
    public struct FLPage
    {
        public int ZIndex;
        public Sprite Icon;
        public Sprite PageRootBackdrop;
        public Sprite PageHeaderBackdrop;
        public Sprite PageBodyBackdrop;
        public Sprite PageFooterBackdrop;

        public Color HeaderColor;
        public Color BodyColor;
        public Color RootColor;
        public Color FooterColor;

        public FLImage AddedImages;
        public FLFont HeaderFont;
        public FLFont SubHeaderFont;
        public FLFont BodyFont;
        public FLFont FooterFont;

        public FLEvent PageEvent;
        public FLAnchors RectTitle;
        public FLAnchors RectSubheader;
        public FLAnchors RectIcon;
        public FLAnchors RectBodyWithImage;
        public FLAnchors RectBodyWithNOImage;
        public FLAnchors RectImage;
        public FLAnchors RectFooter;
        public FLAnchors RectFooterNOEvent;
        public FLAnchors RectEvent;
    }
    #endregion
    public static class FL_Utility 
    {
        private readonly static char[] invalidFilenameChars;
        private readonly static char[] invalidPathChars;
        private readonly static char[] parseTextImagefileChars;
        static FL_Utility()
        {
            invalidFilenameChars = System.IO.Path.GetInvalidFileNameChars();
            invalidPathChars = System.IO.Path.GetInvalidPathChars();
            parseTextImagefileChars = new char[1] { '~' };
            Array.Sort(invalidFilenameChars);
            Array.Sort(invalidPathChars);
        }
        
        public static void UpdateRectTransformAnchors(ref RectTransform theRect, FLAnchors anchorData)
        {
            theRect.anchorMin = anchorData.AnchorMin;
            theRect.anchorMax = anchorData.AnchorMax;
            theRect.anchoredPosition = anchorData.AnchorPosition;
        }
        public static List<FLFont> ReturnFontAssetListByEnum(List<FLFont> fontSearch, FontType index)
        {
            IEnumerable<FLFont> query = fontSearch.FindAll(x => x.FontType == index);
            return query.ToList();
        }

    }
    public static class SerilizeDeserialize
    {
        // Serialize collection of any type to a byte stream
        public static byte[] Serialize<T>(T obj)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binSerializer = new BinaryFormatter();
                binSerializer.Serialize(memStream, obj);
                return memStream.ToArray();
            }
        }
        // DSerialize collection of any type to a byte stream
        public static T Deserialize<T>(byte[] serializedObj)
        {
            T obj = default(T);
            if (serializedObj == null)
            {
                return obj;
            }
            if (serializedObj.Length == 0)
            {
                return obj;
            }
            using (MemoryStream memStream = new MemoryStream(serializedObj))
            {
                BinaryFormatter binSerializer = new BinaryFormatter();
                obj = (T)binSerializer.Deserialize(memStream);
            }
            return obj;
        }
    }
}
