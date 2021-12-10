using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace JFuzz.Lazarsfeld
{
    //Base class for each container associated with a page
    public class FL_Container : MonoBehaviour, IFLContainerUpdate, IFLFont
    {
        public RectTransform Container;
        public Image ContainerBackdrop;
        public TextMeshProUGUI ContainerFont;
        public string FontThemeName;

        [SerializeField]
        private bool _useParentTheme;
        [SerializeField]
        private RectTransform _parentContainer;
        
        FLFont FontData;
        FLAnchors AnchorData;

        public void FontAssetUpdate(TMP_FontAsset fontAsset, FontType fontType)
        {
            if(FontData.FontType == fontType)
            {
                FontData.FontAsset = fontAsset;
                ContainerFont.font = fontAsset;
                ContainerFont.UpdateFontAsset();
            }
        }
        public void FontDataUpdate(string fontData, FontType fontType)
        {
            if (FontData.FontType == fontType)
            {
                FontData.FontData = fontData;
                ContainerFont.text = fontData;
            }
        }

        public void FontColorUpdate(Color fontColor, FontType fontType)
        {
            if(FontData.FontType == fontType)
            {
                FontData.FontColor = fontColor;
                ContainerFont.color = fontColor;
            }
        }

        public void FontSizeUpdate(float baseSize, FontType fontType)
        {
            if (FontData.FontType== fontType)
            {
                FontData.FontSize = baseSize;
                ContainerFont.fontSize = baseSize;
            }
        }

        public void Initialize(RectTransform parentContainer, bool useParentTheme)
        {
            _parentContainer = parentContainer;
            _useParentTheme = useParentTheme;
        }

        public void InitializeFont(FLFont fontData)
        {
            FontData = fontData;
            ContainerFont.font = fontData.FontAsset;
            ContainerFont.UpdateFontAsset();
            ContainerFont.color = fontData.FontColor;
            ContainerFont.fontSize = fontData.FontSize;
            ContainerFont.text = fontData.FontData;

        }
        public void CopyVisualData(FLFont fontData)
        {
            FontData.FontAsset = fontData.FontAsset;
            FontData.FontColor = fontData.FontColor;
            FontData.FontSize = fontData.FontSize;
            FontData.FontType = fontData.FontType;
        }

        public void OnAnchorUpdate(FLAnchors anchorData)
        {
            throw new System.NotImplementedException();
        }

        public void OnBodyUpdate(string newBody)
        {
            throw new System.NotImplementedException();
        }

        public void OnContainerColorUpdate(Color baseColor)
        {
            throw new System.NotImplementedException();
        }

        public void OnHeaderUpdate(string newHeader)
        {
            throw new System.NotImplementedException();
        }
    }

}
