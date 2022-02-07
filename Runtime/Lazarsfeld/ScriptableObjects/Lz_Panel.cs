using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace JFuzz.Lazarsfeld
{
    [CreateAssetMenu(fileName = "Simple Information Panel", menuName = "ScriptableObjects/JFuzz/Lazarsfeld/Info_Panel", order = 5)]
    public class Lz_Panel : Lz_Info
    {
        [Space]
        [Header("Textual Information in the Body")]
        [TextArea(5, 10)]
        public string BodyInformation;
        [Space]
        [Header("Event Info")]
        public FLEvent EventData;
        public FLAnchors EventAnchor;
        public FLAnchors FooterAnchorNoEvent;
        /*
        [Header("Theme the Page")]
        public FL_Theme Theme;
        public string PageName;

        [Space]
        [Header("Header of Page")]
        public FL_Font HeaderFont;
        public string HeaderInformation;
        public FLAnchors HeaderIconAnchors;
        public FLAnchors HeaderAnchors;

        [Space]
        [Header("SubHeader of Page")]
        public FL_Font SubHeaderFont;
        public string SubheaderInformation;
        public FLAnchors SubheaderAnchors;

        [Space]
        [Header("Main of Page")]
        public FL_Font BodyFont;
        public FLAnchors BodyAnchors;
        public FLAnchors BodyAnchorsNoImage;
        
        [Space]
        [Header("Image info")]
        public FLImage ImageData;
        public FLAnchors ImageAnchors;
        [Space]
        [Header("Footer of Page")]
        public FL_Font FooterFont;
        public string FooterInformation;
        public FLAnchors FooterAnchors;
        */
    }
}
