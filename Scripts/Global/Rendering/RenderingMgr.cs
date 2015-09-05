using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Rendering
{
    public struct ScreenStruct
    {
        public GameObject CanvasObj;
        public GameObject CameraObj;
        public GameObject RawImgObj;
        public Canvas CanvasComp;
        public Camera ProcessCam;
        public RawImage RawImgComp;
        public Material ScreenMat;
    };

    public enum RESOLUTION
    {
        R1280x720 = 0,
        R1920x1080 = 1
    }

    public class RenderingMgr : Singleton<RenderingMgr>
    {
        private RenderTexture m_rtCustomFramBuffer;
        private LinkedList<IRenderingNode> m_llRenderingNodeList;
        private Camera m_camProcessor;
        private RenderingDriver m_driver;
        private ScreenStruct m_csScreen;
        private Ticker m_ticker;


        public RenderingMgr()
        {
            m_llRenderingNodeList = new LinkedList<IRenderingNode>();
            Initialize();
        }

        private void Initialize()
        {
            CreateRenderScreen();

            /**************** Initiating Custom Frame Buffer ****************/
            //tmp resoultion r1280x720
            m_rtCustomFramBuffer = new RenderTexture(1280, 720, 24);
            m_csScreen.RawImgComp.texture = m_rtCustomFramBuffer;
            m_csScreen.ScreenMat = new Material(Shader.Find("UI/Default"));
            m_csScreen.RawImgComp.material = m_csScreen.ScreenMat;
            /**************** Initiating Custom Frame Buffer ****************/


            ResumeRendering();
        }

        private void CreateRenderScreen()
        {
            /**************** Initiating GameObjects ****************/
            m_csScreen.CanvasObj = new GameObject();
            m_csScreen.CanvasObj.name = "Screen";
            m_ticker = m_csScreen.CanvasObj.AddComponent<Ticker>();
            m_csScreen.CanvasComp = m_csScreen.CanvasObj.AddComponent<Canvas>();
            m_csScreen.CanvasComp.renderMode = RenderMode.ScreenSpaceCamera;
            m_csScreen.CanvasObj.AddComponent<CanvasScaler>();
            m_csScreen.CameraObj = new GameObject();
            m_csScreen.CameraObj.name = "ProcessCam";
            m_csScreen.ProcessCam = m_csScreen.CameraObj.AddComponent<Camera>();
            m_driver = m_csScreen.CameraObj.AddComponent<RenderingDriver>();
            m_camProcessor = m_csScreen.ProcessCam;
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
            /**************** Initiating GameObjects ****************/
        }

        private void ExecuteNodeList(float dt)
        {
            m_camProcessor.targetTexture = m_rtCustomFramBuffer;
            LinkedListNode<IRenderingNode> iter = m_llRenderingNodeList.First;
            for (; iter != null; iter = iter.Next)
            {
                iter.Value.Execute(dt);
            }

            //finish processing, copy the CFrameBuffer to the real fram buffer
            m_camProcessor.targetTexture = null;
        }

        private void ForceTerminateRendering()
        {
            m_camProcessor.targetTexture = null;
            m_csScreen.RawImgComp.texture = null;
            if (m_rtCustomFramBuffer != null)
            {
                m_rtCustomFramBuffer.Release();
                m_rtCustomFramBuffer = null;
            }

            //remove ExecuteNodeList from ticker
            m_driver.onPreRender = null;
        }

        private void ResumeRendering()
        {
            //Add ExecuteNodeList to ticker
            m_driver.onPreRender = ExecuteNodeList;
        }

        public RenderTexture CFrameBuffer
        {
            get
            {
                return m_rtCustomFramBuffer;
            }
        }

        public ScreenStruct ScreenInfo
        {
            get
            {
                return m_csScreen;
            }
        }

        public void SetResolution(RESOLUTION r)
        {
            ForceTerminateRendering();
            switch(r)
            {
                case RESOLUTION.R1280x720:
                    break;
                case RESOLUTION.R1920x1080:
                    break;
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
