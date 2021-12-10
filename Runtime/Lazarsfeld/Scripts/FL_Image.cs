using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JFuzz.Lazarsfeld
{
    /// <summary>
    /// base unity class for how we want to show different images via layouts like grid / or slideshow 
    /// </summary>
    public class FL_Image : MonoBehaviour
    {
        public Color ImageBorderColor;
        public bool GridBasedSystem;
        public List<RectTransform> ImageTransforms = new List<RectTransform>();

        public GridLayoutGroup GridLayoutManager;
        public RectTransform RootImage;
        public RectTransform GridImageParentRoot;
        public RectTransform SlideImageParentRoot;
        public RectTransform SlideImageContainer;
        public Image ImageControlBoardBackground;
        public GameObject ImagePrefab;
        private int _imageIndex;
        public void SetupImageViewer(FLImage imageData)
        {
            GridBasedSystem = imageData.GridBased;
            ImageBorderColor = imageData.ImageBorder;
            ImageControlBoardBackground.color = imageData.ImageBorder;
            if (GridBasedSystem)
            {
                GridImageParentRoot.gameObject.SetActive(true);
                SlideImageParentRoot.gameObject.SetActive(false);
                var gridWidth = GridImageParentRoot.rect.width;
                var gridHeight = GridImageParentRoot.rect.height;
                var min = Mathf.Min(gridWidth, gridHeight);
                GridLayoutManager.cellSize = new Vector2(min / 2f, min / 2f);
            }
            else
            {
                SlideImageParentRoot.gameObject.SetActive(true);
                GridImageParentRoot.gameObject.SetActive(false);
            }
            for(int i = 0; i < imageData.Images.Length; i++)
            {
                var newImagePrefab = GameObject.Instantiate(ImagePrefab);
                var backDrop = newImagePrefab.transform.GetChild(0);
                var iconImage = backDrop.transform.GetChild(0);
                iconImage.GetComponent<Image>().sprite = imageData.Images[i];
                backDrop.GetComponent<Image>().color = imageData.ImageBorder;
                if (GridBasedSystem)
                {
                    newImagePrefab.transform.SetParent(GridImageParentRoot);
                    newImagePrefab.transform.localScale = new Vector3(1, 1, 1);
                    newImagePrefab.transform.localPosition = new Vector3(0, 0, 0);
                }
                else
                {
                    newImagePrefab.transform.SetParent(SlideImageContainer);
                    newImagePrefab.transform.localScale = new Vector3(1, 1, 1);
                    newImagePrefab.transform.localPosition = new Vector3(0, 0, 0);
                    newImagePrefab.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                    newImagePrefab.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                    
                    newImagePrefab.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    newImagePrefab.GetComponent<RectTransform>().offsetMax = new Vector2(1, 1);
                }
            }
            if (!GridBasedSystem)
            {
                if (SlideImageContainer.childCount > 0)
                {
                    for (int i = 0; i < SlideImageContainer.childCount; i++)
                    {
                        SlideImageContainer.GetChild(i).gameObject.SetActive(false);
                    }
                    _imageIndex = 0;
                    SlideImageContainer.GetChild(_imageIndex).gameObject.SetActive(true);
                }
                
            }

        }
        public void SlideNextImage()
        {
            SlideImageContainer.GetChild(_imageIndex).gameObject.SetActive(false);
            _imageIndex++;
            if (_imageIndex < SlideImageContainer.childCount)
            {
                
            }
            else
            {
                _imageIndex = 0;
            }
            SlideImageContainer.GetChild(_imageIndex).gameObject.SetActive(true);
        }
        public void PreviousImage()
        {
            SlideImageContainer.GetChild(_imageIndex).gameObject.SetActive(false);
            _imageIndex--;
            if (_imageIndex >= 0)
            {

            }
            else
            {
                _imageIndex = SlideImageContainer.childCount-1;
            }
            SlideImageContainer.GetChild(_imageIndex).gameObject.SetActive(true);
        }
        public void SetupNoImages()
        {
            //kill my rect transform size and make me a small potato
            RootImage.gameObject.SetActive(false);
        }

    }
}

