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
        void InitializePage(Transform ParentObject, FLPage pageData, string pageName, FLTheme themeData);
    }

    public interface IFLContainerUpdate
    {
        void OnHeaderUpdate(string newHeader);
        void OnBodyUpdate(string newBody);
        void OnContainerColorUpdate(Color baseColor);
        void OnAnchorUpdate(FLAnchors anchorData);
        void Initialize(RectTransform parentContainer, bool useParentTheme);
       
    }
    
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
        public Sprite Icon;
        public Sprite BackdropBorderImage;
        public Color HeaderColor;
        public Color RootColor;
        public Color BodyColor;
        public Color FooterColor;

        public Color MainFontColor;
        public Color HeaderFontColor;
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
