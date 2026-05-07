using System.Collections.Generic;
using UnityEngine;

namespace MoreHead
{
    // 装饰物工具类
    public static class DecorationUtils
    {
        // 节点路径常量
        private const string HEAD_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM HEAD BOT/code_head_bot_up/code_head_bot_side/Player Spring Impulse - Head Bot/_____________________________________/ANIM HEAD TOP/code_head_top";
        private const string NECK_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM HEAD BOT/code_head_bot_up/code_head_bot_side";
        private const string BODY_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/ANIM BODY TOP SCALE";
        private const string HIP_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT";
        
        // 四肢节点路径常量
        private const string LEFT_ARM_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM ARM L/code_arm_l";
        private const string RIGHT_ARM_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM ARM R/code_arm_r_parent/code_arm_r/ANIM ARM R SCALE";
        private const string LEFT_LEG_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/code_leg_twist/_____________________________________/ANIM LEG L BOT/_____________________________________/ANIM LEG L TOP";
        private const string RIGHT_LEG_NODE_PATH = "[RIG]/code_lean/code_tilt/ANIM BOT/code_leg_twist/_____________________________________/ANIM LEG R BOT/_____________________________________/ANIM LEG R TOP";


        // 获取装饰物父级节点
        public static Dictionary<string, Transform> GetDecorationParentNodes(Transform rootTransform)
        {
            Dictionary<string, Transform> parentNodes = new Dictionary<string, Transform>();
            
            // 获取头部节点
            var headNode = rootTransform.Find(HEAD_NODE_PATH);
            if (headNode != null)
            {
                parentNodes["head"] = headNode;
            }
            
            // 获取脖子节点
            var neckNode = rootTransform.Find(NECK_NODE_PATH);
            if (neckNode != null)
            {
                parentNodes["neck"] = neckNode;
            }
            
            // 获取身体节点
            var bodyNode = rootTransform.Find(BODY_NODE_PATH);
            if (bodyNode != null)
            {
                parentNodes["body"] = bodyNode;
            }
            
            // 获取臀部节点
            var hipNode = rootTransform.Find(HIP_NODE_PATH);
            if (hipNode != null)
            {
                parentNodes["hip"] = hipNode;
            }
            
            // 获取左手臂节点
            var leftArmNode = rootTransform.Find(LEFT_ARM_NODE_PATH);
            if (leftArmNode != null)
            {
                parentNodes["leftarm"] = leftArmNode;
            }
            
            // 获取右手臂节点
            var rightArmNode = rootTransform.Find(RIGHT_ARM_NODE_PATH);
            if (rightArmNode != null)
            {
                parentNodes["rightarm"] = rightArmNode;
            }
            
            // 获取左腿节点
            var leftLegNode = rootTransform.Find(LEFT_LEG_NODE_PATH);
            if (leftLegNode != null)
            {
                parentNodes["leftleg"] = leftLegNode;
            }
            
            // 获取右腿节点
            var rightLegNode = rootTransform.Find(RIGHT_LEG_NODE_PATH);
            if (rightLegNode != null)
            {
                parentNodes["rightleg"] = rightLegNode;
            }
            
            // 获取世界空间节点 - 根目录，不受角色动画影响
            if (rootTransform != null)
            {
                // 查找是否为本地玩家
                bool isLocalPlayer = false;
                
                // 尝试获取PlayerAvatar组件
                var playerAvatar = rootTransform.parent.GetComponentInChildren<PlayerAvatar>();
                if (playerAvatar != null)
                {
                    // 判断是否是本地玩家：单人模式下或在多人模式下是自己
                    isLocalPlayer = !SemiFunc.IsMultiplayer() || (playerAvatar.photonView != null && playerAvatar.photonView.IsMine);
                }
                
                // 查找是否已经存在世界跟随节点
                Transform existingWorldNode = rootTransform.Find("WorldDecorationFollower");
                if (existingWorldNode != null)
                {
                    // 如果是本地玩家，设置为不可见
                    if (isLocalPlayer)
                    {
                        existingWorldNode.gameObject.SetActive(false);
                    }
                    else
                    {
                        existingWorldNode.gameObject.SetActive(true);
                    }
                    
                    parentNodes["world"] = existingWorldNode;
                }
                else
                {
                    // 创建一个新的跟随节点
                    GameObject worldNode = new GameObject("WorldDecorationFollower");
                    worldNode.transform.SetParent(rootTransform, false);
                    worldNode.transform.localPosition = Vector3.zero;
                    worldNode.transform.localRotation = Quaternion.identity;
                    worldNode.transform.localScale = Vector3.one;
                    
                    // 添加跟随组件
                    worldNode.AddComponent<WorldSpaceFollower>();
                    
                    // 如果是本地玩家，设置为不可见
                    if (isLocalPlayer)
                    {
                        worldNode.SetActive(false);
                    }
                    else
                    {
                        worldNode.SetActive(true);
                    }
                    
                    parentNodes["world"] = worldNode.transform;
                }
            }
            
            return parentNodes;
        }

        // 确保每个父级节点有装饰物容器
        public static void EnsureDecorationContainers(Dictionary<string, Transform> parentNodes)
        {
            foreach (var kvp in parentNodes)
            {
                Transform parentNode = kvp.Value;
                
                // 查找或创建装饰物父对象
                var decorationsParent = parentNode.Find("HeadDecorations");
                if (decorationsParent == null)
                {
                    decorationsParent = new GameObject("HeadDecorations").transform;
                    decorationsParent.SetParent(parentNode, false);
                    decorationsParent.localPosition = Vector3.zero;
                    decorationsParent.localRotation = Quaternion.identity;
                    decorationsParent.localScale = Vector3.one;
                }
            }
        }
        
        // 优化的装饰物状态更新方法
        public static void UpdateDecoration(Transform parent, string decorationName, bool showDecoration)
        {
            var decoration = parent.Find(decorationName);
            if (decoration != null && decoration.gameObject.activeSelf != showDecoration)
            {
                decoration.gameObject.SetActive(showDecoration);
            }
        }
    }
    
    // 世界空间跟随组件 - 只跟随位置，保持水平方向
    public class WorldSpaceFollower : MonoBehaviour
    {
        private Transform? _rootTransform;
        private Vector3 _initialOffset;
        
        void Start()
        {
            // 获取根变换
            _rootTransform = transform.parent;
            if (_rootTransform != null)
            {
                // 记录初始偏移量
                _initialOffset = transform.position - _rootTransform.position;
                
                // 重置旋转和缩放
                transform.rotation = Quaternion.identity;
                transform.localScale = Vector3.one;
            }
        }
        
        void LateUpdate()
        {
            if (_rootTransform != null)
            {
                // 跟随位置
                transform.position = _rootTransform.position + _initialOffset;
                
                // 保持水平方向，但跟随Y轴旋转
                transform.rotation = Quaternion.Euler(0, _rootTransform.eulerAngles.y, 0);
                
                // 保持固定缩放
                transform.localScale = Vector3.one;
            }
        }
    }
}
