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
        public Camera ProcessCam;
        public Material ScreenMat;
    };

    public class RenderingMgr : Singleton<RenderingMgr>
    {
        private RenderTexture m_customFramBuffer;
        private LinkedList<IRenderingNode> m_renderingNodeList;
        private Camera m_processCam;


        public RenderingMgr()
        {
            m_renderingNodeList = new LinkedList<IRenderingNode>();
        }

        private void Initialize()
        {

        }

        private void ExecuteNodeList()
        {
            m_processCam.targetTexture = m_customFramBuffer;
            LinkedListNode<IRenderingNode> iter = m_renderingNodeList.First;
            for (; iter != null; iter = iter.Next)
            {
                iter.Value.Execute();
                m_processCam.Render();
            }
            m_processCam.targetTexture = null;
            m_processCam.Render();
        }

        public RenderTexture CFrameBuffer
        {
            get
            {
                return m_customFramBuffer;
            }
        }

        public void AddNode(IRenderingNode unit)
        {

        }

        public void RemoveNode(IRenderingNode unit)
        {
            LinkedListNode<IRenderingNode> node = m_renderingNodeList.Find(unit);
            if (node == null)
                return;

            node.Value.Clear();
            m_renderingNodeList.Remove(node);
        }
    }
}
