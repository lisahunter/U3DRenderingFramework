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

        public PostProcessUnit()
        {
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
