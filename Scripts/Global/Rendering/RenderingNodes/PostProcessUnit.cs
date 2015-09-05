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

        public PostProcessUnit(Shader shader)
        {
            m_matScreenMat = RenderingMgr.Instance.ScreenInfo.ScreenMat;
            m_camProcessor = RenderingMgr.Instance.ScreenInfo.ProcessCam;
            m_postProcessShader = shader;
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void Update(float dt)
        {

        }

        public virtual void Clear()
        {

        }

        public void Execute(float dt)
        {
            Update(dt);
        }
    }
}
