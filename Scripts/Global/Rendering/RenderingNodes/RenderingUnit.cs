using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering
{
    public class RenderingUnit : IRenderingNode
    {
        private Camera m_targetCamera;

        public RenderingUnit(Camera targetCam)
        {
            m_targetCamera = targetCam;
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void Update()
        {

        }

        public virtual void Clear()
        {

        }

        public void Execute()
        {
            Update();
        }
    }
}
