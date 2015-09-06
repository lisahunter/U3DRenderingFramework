using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Rendering
{
    public class PostProcessUnit : IRenderingNode
    {
        private Shader m_postProcessShader;
        private Material m_matScreenMat;
        private Camera m_camProcessor;

        public PostProcessUnit()
        {
            m_camProcessor = RenderingMgr.Instance.ScreenInfo.ProcessCam;
        }

        /// <summary>
        /// call when added to rendering node list
        /// </summary>
        public virtual void Initialize()
        {
            
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
            if (renderToFrameBuffer)
            {
                // it's the last rendering node, render to frame buffer
                m_camProcessor.targetTexture = null;
            }
            else
            {
                // it's not the last rendering node, render to custom frame buffer
                m_camProcessor.Render();
            }
        }
    }
}
