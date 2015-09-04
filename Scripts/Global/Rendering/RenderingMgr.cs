using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Rendering
{
    struct ScreenStruct
    {
        public GameObject CanvasObj;
        public GameObject CameraObj;
        public GameObject RawImgObj;
        public Canvas CanvasComp;
        public Camera ProcessCam;
        public RawImage RawImgComp;
        public Material ScreenMat;
    };

    public class RenderingMgr : Singleton<RenderingMgr>
    {
        private RenderTexture m_rtCustomFramBuffer;
        private LinkedList<IRenderingNode> m_llRenderingNodeList;
        private Camera m_camProcessor;
        private ScreenStruct m_csScreen;


        public RenderingMgr()
        {
            m_llRenderingNodeList = new LinkedList<IRenderingNode>();
            Initialize();
        }

        private void Initialize()
        {
            m_csScreen.CanvasObj = new GameObject();
            m_csScreen.CanvasObj.name = "Screen";
            m_csScreen.CanvasComp = m_csScreen.CanvasObj.AddComponent<Canvas>();
            m_csScreen.CanvasComp.renderMode = RenderMode.ScreenSpaceCamera;
            m_csScreen.CanvasObj.AddComponent<CanvasScaler>();
            m_csScreen.CameraObj = new GameObject();
            m_csScreen.CameraObj.name = "ProcessCam";
            m_csScreen.ProcessCam = m_csScreen.CameraObj.AddComponent<Camera>();
            m_csScreen.CameraObj.transform.SetParent(m_csScreen.CanvasObj.transform, false);
            m_csScreen.CameraObj.transform.localPosition = new Vector3(0.0f, 0.0f, -100.0f);
            m_csScreen.ProcessCam.orthographic = true;
            m_csScreen.ProcessCam.farClipPlane = 10.0f;
            m_csScreen.CanvasComp.worldCamera = m_csScreen.ProcessCam;
            m_csScreen.RawImgObj = new GameObject();
            m_csScreen.RawImgObj.name = "RawImage";
            m_csScreen.RawImgObj.transform.SetParent(m_csScreen.CanvasObj.transform, false);
            RectTransform rawImgRect = m_csScreen.RawImgObj.AddComponent<RectTransform>();
            rawImgRect.anchorMin = Vector2.zero;
            rawImgRect.anchorMax = Vector2.one;
            rawImgRect.offsetMin = Vector2.zero;
            rawImgRect.offsetMax = Vector2.zero;
            m_csScreen.RawImgComp = m_csScreen.RawImgObj.AddComponent<RawImage>();
            m_csScreen.ScreenMat = m_csScreen.RawImgComp.material;
        }

        private void ExecuteNodeList()
        {
            m_camProcessor.targetTexture = m_rtCustomFramBuffer;
            LinkedListNode<IRenderingNode> iter = m_llRenderingNodeList.First;
            for (; iter != null; iter = iter.Next)
            {
                iter.Value.Execute();
                m_camProcessor.Render();
            }
            m_camProcessor.targetTexture = null;
            m_camProcessor.Render();
        }

        public RenderTexture CFrameBuffer
        {
            get
            {
                return m_rtCustomFramBuffer;
            }
        }

        public void AddNodeAtFirst(IRenderingNode unit)
        {
            m_llRenderingNodeList.AddFirst(unit);
        }

        public void AddNodeAtLast(IRenderingNode unit)
        {
            m_llRenderingNodeList.AddLast(unit);
        }

        public void RemoveNode(IRenderingNode unit)
        {
            LinkedListNode<IRenderingNode> node = m_llRenderingNodeList.Find(unit);
            if (node == null)
                return;

            node.Value.Clear();
            m_llRenderingNodeList.Remove(node);
        }
    }
}
