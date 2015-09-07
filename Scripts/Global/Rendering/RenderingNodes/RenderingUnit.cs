using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering
{
    public class RenderingUnit : IRenderingNode
    {
        private Camera m_camTarget;
        private Camera m_camProcessor;

        public RenderingUnit(Camera targetCam)
        {
            m_camTarget = targetCam;
            m_camTarget.enabled = false;
            Reset();
        }

        /// <summary>
        /// call when added to rendering node list
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// call when rendering mgr is calling resume function
        /// </summary>
        public virtual void Reset()
        {
            m_camTarget.targetTexture = RenderingMgr.Instance.CFrameBuffer;
            m_camProcessor = RenderingMgr.Instance.ScreenInfo.ProcessCam;
        }

        /// <summary>
        /// call before execute called
        /// </summary>
        /// <param name="dt"></param>
        protected virtual void Update(float dt)
        {

        }

        /// <summary>
        /// call when remove from rendering node list
        /// </summary>
        public virtual void Clear()
        {

        }

        /// <summary>
        /// call every frame, before render stuff into real frame buffer
        /// </summary>
        public void Execute(float dt, bool renderToFrameBuffer)
        {
            Update(dt);
            m_camTarget.Render();
            if (renderToFrameBuffer)
            {
                // let processor cam copy custom frame buffer to real frame buffer
                m_camProcessor.targetTexture = null;
            }
        }
    }
}
