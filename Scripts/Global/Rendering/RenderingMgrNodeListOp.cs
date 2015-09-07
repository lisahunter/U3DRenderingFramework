using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rendering
{
    public partial class RenderingMgr
    {
        public void AddUnitAtFirst(IRenderingNode unit)
        {
            if (unit == null)
                return;
            m_llRenderingNodeList.AddFirst(unit);
            unit.Initialize();
        }

        public void AddUnitAtLast(IRenderingNode unit)
        {
            if (unit == null)
                return;
            m_llRenderingNodeList.AddLast(unit);
            unit.Initialize();
        }

        public void AddUnitAfterNode(IRenderingNode unit, LinkedListNode<IRenderingNode> node)
        {
            if (node == null || unit == null)
                return;
            m_llRenderingNodeList.AddAfter(node, unit);
            unit.Initialize();
        }

        public void AddUnitBeforeNode(IRenderingNode unit, LinkedListNode<IRenderingNode> node)
        {
            if (node == null || unit == null)
                return;
            m_llRenderingNodeList.AddBefore(node, unit);
            unit.Initialize();
        }

        public void AddCrucialUnitAtFirst(string unitName, IRenderingNode unit)
        {
            if (unit == null)
                return;
            LinkedListNode<IRenderingNode> newUnit = new LinkedListNode<IRenderingNode>(unit);
            m_dicCrucialNodes.Add(unitName, newUnit);
            m_llRenderingNodeList.AddFirst(newUnit);
            unit.Initialize();
        }

        public void AddCrucialUnitAtLast(string unitName, IRenderingNode unit)
        {
            if (unit == null)
                return;
            LinkedListNode<IRenderingNode> newUnit = new LinkedListNode<IRenderingNode>(unit);
            m_dicCrucialNodes.Add(unitName, newUnit);
            m_llRenderingNodeList.AddLast(newUnit);
            unit.Initialize();
        }

        public void AddCrucialUnitAfterNode(string unitName, IRenderingNode unit, LinkedListNode<IRenderingNode> node)
        {
            if (node == null || unit == null)
                return;
            LinkedListNode<IRenderingNode> newUnit = new LinkedListNode<IRenderingNode>(unit);
            m_dicCrucialNodes.Add(unitName, newUnit);
            m_llRenderingNodeList.AddAfter(node, newUnit);
            unit.Initialize();
        }

        public void AddCrucialUnitBeforeNode(string unitName, IRenderingNode unit, LinkedListNode<IRenderingNode> node)
        {
            if (node == null || unit == null)
                return;
            LinkedListNode<IRenderingNode> newUnit = new LinkedListNode<IRenderingNode>(unit);
            m_dicCrucialNodes.Add(unitName, newUnit);
            m_llRenderingNodeList.AddBefore(node, newUnit);
            unit.Initialize();
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
