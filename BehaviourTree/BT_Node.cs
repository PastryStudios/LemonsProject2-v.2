using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class BT_Node
    {
        protected NodeState nodeState;

        public BT_Node nodeParent;
        protected List<BT_Node> nodeChildren;

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        public BT_Node()
        {
            nodeParent = null;
        }
        public BT_Node(List<BT_Node> nodeChildren)
        {

        }

        private void _Attach(BT_Node node)
        {
            node.nodeParent = this;
            nodeChildren.Add(node);
        }
        public virtual NodeState Evaluate() => NodeState.FAILURE;
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            BT_Node bT_Node = nodeParent;
            while (bT_Node != null)
            {
                value = bT_Node.GetData(key);
                if (value != null)
                    return value;
                bT_Node = bT_Node.nodeParent;
            }
            return null;
        }
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }
            BT_Node bT_Node = nodeParent;
            while(bT_Node != null)
            {
                bool cleared = bT_Node.ClearData(key);
                if (cleared)
                    return true;
                bT_Node = bT_Node.nodeParent;
            }
            return false;
        }
    }

}
