﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using MoreHead;
using System;
using TMPro;
using System.Linq;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class Morehead : BaseUnityPlugin
{
    private const string PluginGuid = "Mhz.REPOMoreHead";
    private const string PluginName = "MoreHead";
    private const string PluginVersion = "1.3.3";
    // 单例实例
    public static Morehead? Instance { get; private set; }
    
    // 日志记录器
    public new static ManualLogSource? Logger;
    
    // 获取插件版本号的公共方法
    public static string GetPluginVersion() => PluginVersion;
    
    private void Awake()
    {
        // 设置单例实例
        Instance = this;
        
        // 初始化日志记录器
        Logger = base.Logger;
        
        try
        {
            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll(typeof(PlayerAvatarVisualsPatch));
            harmony.PatchAll(typeof(PlayerUpdatePatch));
            harmony.PatchAll(typeof(PlayerAvatarAwakePatch));
            harmony.PatchAll(typeof(GameDirectorUpdatePatch));
            harmony.PatchAll(typeof(PlayerRevivePatch));
            harmony.PatchAll(typeof(MenuManagerStartPatch));
            harmony.PatchAll(typeof(MenuButtonHoveringPatch));
            harmony.PatchAll(typeof(MenuButtonHoverEndPatch));
            
            string asciiArt = @$"

 ███▄ ▄███▓ ▒█████   ██▀███  ▓█████  ██░ ██ ▓█████ ▄▄▄      ▓█████▄ 
▓██▒▀█▀ ██▒▒██▒  ██▒▓██ ▒ ██▒▓█   ▀ ▓██░ ██▒▓█   ▀▒████▄    ▒██▀ ██▌
▓██    ▓██░▒██░  ██▒▓██ ░▄█ ▒▒███   ▒██▀███░▒███  ▒██  ▀█▄  ░██   █▌
▒██    ▒██ ▒██   ██░▒███▀█▄  ▒▓█  ▄ ░▓█ ░██ ▒▓█  ▄░██▄▄▄▄██ ░▓█▄   ▌
▒██▒   ░██▒░ ████▓▒░░██▓ ▒██▒░▒████▒░▓█▒░██▓░▒████▒▓█   ▓██▒░▒████▓   v{PluginVersion}
░ ▒░   ░  ░░ ▒░▒░▒░ ░ ▒▓ ░▒▓░░░ ▒░ ░ ▒ ░░▒░▒░░ ▒░ ░▒▒   ▓▒█░ ▒▒▓  ▒ 
░  ░      ░  ░ ▒ ▒░   ░▒ ░ ▒░ ░ ░  ░ ▒ ░▒░ ░ ░ ░  ░ ▒   ▒▒ ░ ░ ▒  ▒ 
░      ░   ░ ░ ░ ▒    ░░   ░    ░    ░  ░░ ░   ░    ░   ▒    ░ ░  ░ 
       ░       ░ ░     ░        ░  ░ ░  ░  ░   ░  ░     ░  ░   ░    
";

            Logger?.LogMessage(asciiArt);
            
            // 初始化装饰物管理器
            HeadDecorationManager.Initialize();
            
            // 初始化配置管理器 - 在装饰物管理器之后初始化，以便应用已保存的状态
            ConfigManager.Initialize();
            
        }
        catch (System.Exception e)
        {
            Logger?.LogError($"Harmony补丁应用失败: {e.Message}");
        }
    }
    
    private void OnApplicationQuit()
    {
        // 在应用退出时保存配置
        ConfigManager.SaveConfig();
    }
    
    // 获取装饰物显示状态
    public static bool GetDecorationState(string? name)
    {
        return HeadDecorationManager.GetDecorationState(name);
    }
}

// 为MenuManager.Start添加补丁，确保在UI初始化后再初始化MoreHeadUI
[HarmonyPatch(typeof(MenuManager))]
[HarmonyPatch("Start")]
class MenuManagerStartPatch
{
    [HarmonyPostfix]
    static void Postfix()
    {
        try {
            // 检查MenuManager实例和菜单页面是否准备好
            if (MenuManager.instance != null && MenuManager.instance.menuPages != null && MenuManager.instance.menuPages.Count > 0)
            {
                Morehead.Logger?.LogInfo("MenuManager已初始化，正在初始化MoreHeadUI...");
                // 初始化UI
                MoreHeadUI.Initialize();
            }
            else
            {
                Morehead.Logger?.LogWarning("MenuManager未准备好，无法初始化UI");
            }
        }
        catch (Exception e) {
            Morehead.Logger?.LogError($"在MenuManager.Start补丁中初始化UI时出错: {e}");
        }
    }
}

// 为PlayerAvatar添加按键控制
[HarmonyPatch(typeof(PlayerAvatar))]
[HarmonyPatch("Update")]
class PlayerUpdatePatch
{
    static void Postfix(PlayerAvatar __instance)
    {
        // 只处理本地玩家
        if (!__instance.photonView.IsMine || !GameManager.Multiplayer() || PhotonNetwork.LocalPlayer == null) return;
        
        // 移除快捷键功能，完全依靠UI控制
    }
    
    // 更新玩家的装饰物状态
    public static void UpdatePlayerDecorations(PlayerAvatar playerAvatar)
    {
        try
        {
            if (playerAvatar?.playerAvatarVisuals == null) return;
            
            // 使用通用方法获取父级节点
            var parentNodes = DecorationUtils.GetDecorationParentNodes(playerAvatar.playerAvatarVisuals.transform);
            
            // 检查是否找到了任何父级节点
            if (parentNodes.Count == 0)
            {
                Morehead.Logger?.LogWarning("找不到任何装饰物父级节点");
                return;
            }
            
            // 确保每个父级节点有装饰物容器
            DecorationUtils.EnsureDecorationContainers(parentNodes);
            
            // 更新每个装饰物
            foreach (var decoration in HeadDecorationManager.Decorations)
            {
                // 获取对应的父级节点
                if (parentNodes.TryGetValue(decoration.ParentTag ?? string.Empty, out Transform parentNode))
                {
                    // 获取装饰物父对象
                    var decorationsParent = parentNode.Find("HeadDecorations");
                    if (decorationsParent != null)
                    {
                        // 使用优化的方法更新装饰物状态
                        DecorationUtils.UpdateDecoration(decorationsParent, decoration.Name ?? string.Empty, decoration.IsVisible);
                    }
                }
                else
                {
                    Morehead.Logger?.LogWarning($"找不到装饰物 {decoration.DisplayName} 的父级节点: {decoration.ParentTag}");
                }
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"更新玩家装饰物时出错: {e.Message}");
        }
    }
    
    // 更新菜单角色的装饰物状态
    public static void UpdateMenuPlayerDecorations()
    {
        try
        {
            // 查找菜单角色
            var menuPlayerVisuals = FindMenuPlayerVisuals();
            if (menuPlayerVisuals == null)
            {
                return;
            }
            
            // 使用通用方法获取父级节点
            var parentNodes = DecorationUtils.GetDecorationParentNodes(menuPlayerVisuals.transform);
            
            // 检查是否找到了任何父级节点
            if (parentNodes.Count == 0)
            {
                Morehead.Logger?.LogWarning("找不到任何菜单角色装饰物父级节点");
                return;
            }
            
            // 确保每个父级节点有装饰物容器
            DecorationUtils.EnsureDecorationContainers(parentNodes);
            
            // 更新每个装饰物
            foreach (var decoration in HeadDecorationManager.Decorations)
            {
                // 获取对应的父级节点
                if (parentNodes.TryGetValue(decoration.ParentTag ?? string.Empty, out Transform parentNode))
                {
                    // 获取装饰物父对象
                    var decorationsParent = parentNode.Find("HeadDecorations");
                    if (decorationsParent != null)
                    {
                        // 添加或更新装饰物
                        var existingDecoration = decorationsParent.Find(decoration.Name);
                        if (existingDecoration == null)
                        {
                            // 添加装饰物
                            AddMenuDecoration(decorationsParent, decoration.Name);
                        }
                        else
                        {
                            // 更新装饰物状态
                            DecorationUtils.UpdateDecoration(decorationsParent, decoration.Name ?? string.Empty, decoration.IsVisible);
                        }
                    }
                }
                else
                {
                    Morehead.Logger?.LogWarning($"找不到菜单角色装饰物 {decoration.DisplayName} 的父级节点: {decoration.ParentTag}");
                }
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"更新菜单角色装饰物状态失败: {e.Message}");
        }
    }
    
    // 查找菜单角色
    static PlayerAvatarVisuals? FindMenuPlayerVisuals()
    {
        // 使用PlayerAvatarMenu的静态实例，避免使用GameObject.Find
        if (PlayerAvatarMenu.instance == null) return null;
        
        // 使用GetComponentInChildren获取PlayerAvatarVisuals
        return PlayerAvatarMenu.instance.GetComponentInChildren<PlayerAvatarVisuals>();
    }
    
    // 为菜单角色添加装饰物
    public static void AddMenuDecoration(Transform parent, string? decorationName)
    {
        try
        {
            // 检查是否已经添加过
            var existingDecoration = parent.Find(decorationName);
            if (existingDecoration != null)
            {
                // 更新状态
                bool isVisible = Morehead.GetDecorationState(decorationName);
                existingDecoration.gameObject.SetActive(isVisible);
                return;
            }
            
            // 查找对应的装饰物信息
            var decoration = HeadDecorationManager.Decorations.Find(d => d.Name != null && d.Name.Equals(decorationName, System.StringComparison.OrdinalIgnoreCase));
            if (decoration != null && decoration.Prefab != null)
            {
                // 使用预制体创建装饰物
                GameObject? decorationObj = GameObject.Instantiate(decoration.Prefab, parent);
                decorationObj.name = decorationName; // 使用唯一的Name作为GameObject的名称
                
                // 设置初始状态
                decorationObj.SetActive(decoration.IsVisible);
            }
            else
            {
                Morehead.Logger?.LogWarning($"AddMenuDecoration: 找不到装饰物 {decorationName} 或其预制体为空");
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"为菜单角色添加装饰物时出错: {e.Message}");
        }
    }
}

// 为PlayerAvatar添加RPC方法
[HarmonyPatch(typeof(PlayerAvatar))]
[HarmonyPatch("Awake")]
class PlayerAvatarAwakePatch
{
    static void Postfix(PlayerAvatar __instance)
    {
        // 添加扩展方法
        __instance.gameObject.AddComponent<HeadDecorationSync>();
    }
}

// 头部装饰同步组件
public class HeadDecorationSync : MonoBehaviourPun
{
    // 同步所有装饰物状态
    public void SyncAllDecorations()
    {
        try
        {
            // 准备装饰物数据
            string?[] names = new string[HeadDecorationManager.Decorations.Count];
            bool[] states = new bool[HeadDecorationManager.Decorations.Count];
            string?[] parentTags = new string?[HeadDecorationManager.Decorations.Count];
            
            // 填充数据
            for (int i = 0; i < HeadDecorationManager.Decorations.Count; i++)
            {
                var decoration = HeadDecorationManager.Decorations[i];
                names[i] = decoration.Name;
                states[i] = decoration.IsVisible;
                parentTags[i] = decoration.ParentTag;
            }
            
            // 发送RPC
            photonView.RPC("UpdateAllDecorations", RpcTarget.Others, names, states, parentTags);
            
            // 移除普通日志，只在调试时需要
            // Morehead.Logger?.LogInfo($"已同步所有装饰物状态，共 {names.Length} 个");
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"同步所有装饰物状态失败: {e.Message}");
        }
    }
    
    // RPC方法：更新所有装饰物
    [PunRPC]
    void UpdateAllDecorations(string[] names, bool[] states, string[] parentTags)
    {
        try
        {
            // 获取PlayerAvatar组件
            var playerAvatar = GetComponent<PlayerAvatar>();
            if (playerAvatar == null || playerAvatar.playerAvatarVisuals == null)
            {
                Morehead.Logger?.LogWarning("找不到PlayerAvatar或PlayerAvatarVisuals组件");
                return;
            }
            
            // 使用通用方法获取父级节点
            var parentNodes = DecorationUtils.GetDecorationParentNodes(playerAvatar.playerAvatarVisuals.transform);
            
            // 检查是否找到了任何父级节点
            if (parentNodes.Count == 0)
            {
                Morehead.Logger?.LogWarning("找不到任何装饰物父级节点");
                return;
            }
            
            // 确保每个父级节点有装饰物容器
            DecorationUtils.EnsureDecorationContainers(parentNodes);
            
            // 更新每个装饰物
            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                bool state = states[i];
                string parentTag = parentTags[i];
                
                // 获取对应的父级节点
                if (parentNodes.TryGetValue(parentTag, out Transform parentNode))
                {
                    // 查找装饰物父对象
                    var decorationsParent = parentNode.Find("HeadDecorations");
                    if (decorationsParent != null)
                    {
                        // 更新装饰物状态
                        DecorationUtils.UpdateDecoration(decorationsParent, name, state);
                    }
                }
            }
            
            // 移除普通日志，只在调试时需要
            // Morehead.Logger?.LogInfo($"通过RPC更新玩家({photonView.Owner.NickName})的所有装饰物状态，共 {names.Length} 个");
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"RPC更新所有装饰物状态失败: {e.Message}");
        }
    }
}

// 为PlayerAvatarVisuals添加装饰物 - 统一补丁
[HarmonyPatch(typeof(PlayerAvatarVisuals))]
[HarmonyPatch("Start")]
class PlayerAvatarVisualsPatch
{
    static void Postfix(PlayerAvatarVisuals __instance)
    {
        try
        {
            // 使用通用方法获取父级节点
            var parentNodes = DecorationUtils.GetDecorationParentNodes(__instance.transform);
            
            // 检查是否找到了任何父级节点
            if (parentNodes.Count == 0)
            {
                Morehead.Logger?.LogWarning($"找不到任何装饰物父级节点 (isMenuAvatar: {__instance.isMenuAvatar})");
                return;
            }
            
            // 确保每个父级节点有装饰物容器
            DecorationUtils.EnsureDecorationContainers(parentNodes);
            
            // 根据是否为菜单角色执行不同的装饰物添加逻辑
            if (__instance.isMenuAvatar)
            {
                // 菜单角色装饰物处理逻辑
                foreach (var kvp in parentNodes)
                {
                    string tag = kvp.Key;
                    Transform parentNode = kvp.Value;
                    
                    // 获取装饰物父对象
                    var decorationsParent = parentNode.Find("HeadDecorations");
                    if (decorationsParent != null)
                    {
                        // 为该父级节点添加所有对应的装饰物
                        foreach (var decoration in HeadDecorationManager.Decorations)
                        {
                            if (decoration.ParentTag == tag)
                            {
                                PlayerUpdatePatch.AddMenuDecoration(decorationsParent, decoration.Name);
                            }
                        }
                    }
                }
            }
            else
            {
                // 普通角色装饰物处理逻辑
                foreach (var decoration in HeadDecorationManager.Decorations)
                {
                    // 获取对应的父级节点
                    if (parentNodes.TryGetValue(decoration.ParentTag ?? string.Empty, out Transform parentNode))
                    {
                        // 获取装饰物父对象
                        var decorationsParent = parentNode.Find("HeadDecorations");
                        if (decorationsParent != null)
                        {
                            // 添加装饰物
                            AddNewDecoration(decorationsParent, decoration, __instance);
                        }
                    }
                    else
                    {
                        Morehead.Logger?.LogWarning($"初始化时找不到装饰物 {decoration.DisplayName} 的父级节点: {decoration.ParentTag}");
                    }
                }
                
                // 如果是本地玩家，同步初始状态到其他玩家
                if (GameManager.Multiplayer() && __instance.playerAvatar != null && __instance.playerAvatar.photonView != null && __instance.playerAvatar.photonView.IsMine)
                {
                    try
                    {
                        // 获取同步组件
                        var syncComponent = __instance.playerAvatar.GetComponent<HeadDecorationSync>();
                        if (syncComponent != null)
                        {
                            // 使用新方法同步所有装饰物
                            syncComponent.SyncAllDecorations();
                        }
                        else
                        {
                            // 保留警告日志，因为这是一个可能导致功能不正常的情况
                            Morehead.Logger?.LogWarning("找不到HeadDecorationSync组件，无法同步初始状态");
                        }
                    }
                    catch (System.Exception e)
                    {
                        Morehead.Logger?.LogError($"同步初始装饰物状态失败: {e.Message}");
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"添加装饰物失败: {e.Message} (isMenuAvatar: {__instance.isMenuAvatar})");
        }
    }
    
    // 添加新装饰物
    static void AddNewDecoration(Transform parent, DecorationInfo decoration, PlayerAvatarVisuals __instance)
    {
        // 检查是否已经添加过
        var existingDecoration = parent.Find(decoration.Name);
        if (existingDecoration != null)
        {
            // 更新状态
            existingDecoration.gameObject.SetActive(decoration.IsVisible);
            return;
        }
        
        // 使用预制体创建装饰物
        if (decoration.Prefab != null)
        {
            try
            {
                // 实例化预制体
                GameObject? decorationObj = GameObject.Instantiate(decoration.Prefab, parent);
                decorationObj.name = decoration.Name; // 使用唯一的Name作为GameObject的名称
                
                // 设置初始状态
                decorationObj.SetActive(decoration.IsVisible);
            }
            catch (System.Exception e)
            {
                Morehead.Logger?.LogError($"实例化预制体时出错: {e.Message}");
            }
        }
        else
        {
            Morehead.Logger?.LogWarning($"AddNewDecoration: 装饰物 {decoration.DisplayName} 的预制体为空");
        }
    }
}

// 监听GameDirector状态变化的补丁
[HarmonyPatch(typeof(GameDirector))]
[HarmonyPatch("Update")]
class GameDirectorUpdatePatch
{
    // 记录上一次的游戏状态
    private static GameDirector.gameState previousState = GameDirector.gameState.Load;
    
    static void Postfix(GameDirector __instance)
    {
        try
        {
            // 检测状态变化为Main
            if (previousState != GameDirector.gameState.Main && __instance.currentState == GameDirector.gameState.Main)
            {
                // 游戏正式开始，同步所有玩家的装饰物状态
                SyncAllPlayersDecorations();
            }
            
            // 更新上一次的状态
            previousState = __instance.currentState;
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"监听游戏状态变化时出错: {e.Message}");
        }
    }
    
    // 同步所有玩家的装饰物状态
    private static void SyncAllPlayersDecorations()
    {
        try
        {
            // 查找本地玩家
            var localPlayer = FindLocalPlayer();
            if (localPlayer != null)
            {
                // 确保本地玩家的装饰物状态是最新的
                PlayerUpdatePatch.UpdatePlayerDecorations(localPlayer);
                
                // 通过RPC同步到其他玩家
                var syncComponent = localPlayer.GetComponent<HeadDecorationSync>();
                if (syncComponent != null)
                {
                    syncComponent.SyncAllDecorations();
                }
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"同步所有玩家装饰物状态时出错: {e.Message}");
        }
    }
    
    // 查找本地玩家
    private static PlayerAvatar? FindLocalPlayer()
    {
        try
        {
            // 查找所有PlayerAvatar对象
            PlayerAvatar?[]? playerAvatars = UnityEngine.Object.FindObjectsOfType<PlayerAvatar>();
            foreach (var avatar in playerAvatars)
            {
                // 检查是否是本地玩家
                if (avatar?.photonView != null && avatar.photonView.IsMine)
                {
                    return avatar;
                }
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"查找本地玩家时出错: {e.Message}");
        }
        
        return null;
    }
}

// 监听玩家复活事件的补丁
[HarmonyPatch(typeof(PlayerAvatar))]
[HarmonyPatch("ReviveRPC")]
class PlayerRevivePatch
{
    static void Postfix(PlayerAvatar __instance)
    {
        try
        {
            // 检查是否是本地玩家
            if (__instance.photonView.IsMine)
            {
                //Morehead.Logger?.LogInfo("本地玩家复活，准备同步装饰物状态");                
                // 延迟一帧后同步到其他玩家，确保装饰物已经正确加载
                __instance.StartCoroutine(DelayedSync(__instance));
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"玩家复活时同步装饰物状态失败: {e.Message}");
        }
    }
    
    // 延迟同步方法
    private static System.Collections.IEnumerator DelayedSync(PlayerAvatar playerAvatar)
    {
        // 等待一帧
        yield return null;
        
        // 再等待一小段时间，确保所有组件都已初始化
        yield return new WaitForSeconds(0.2f);
        
        // 首先确保玩家的装饰物状态是最新的
        try
        {
            PlayerUpdatePatch.UpdatePlayerDecorations(playerAvatar);
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"更新玩家装饰物状态失败: {e.Message}");
        }
        
        // 再等待一小段时间，确保装饰物已经应用
        yield return new WaitForSeconds(0.1f);
        
        // 获取同步组件并同步状态
        try
        {
            var syncComponent = playerAvatar.GetComponent<HeadDecorationSync>();
            if (syncComponent != null)
            {
                //Morehead.Logger?.LogInfo("玩家复活后同步装饰物状态");
                syncComponent.SyncAllDecorations();
            }
            else
            {
                Morehead.Logger?.LogWarning("玩家复活后找不到HeadDecorationSync组件");
            }
        }
        catch (System.Exception e)
        {
            Morehead.Logger?.LogError($"同步装饰物状态失败: {e.Message}");
        }
    }
}

// 装饰物按钮标识组件
public class DecorationButtonMarker : MonoBehaviour
{
    // 关联的装饰物信息
    public DecorationInfo? Decoration { get; set; }
    
    // 是否已经处理过悬停
    public bool HasHandledHover { get; set; }
}

// 为MenuButton的OnHovering和OnHoverEnd方法添加补丁
[HarmonyPatch(typeof(MenuButton))]
[HarmonyPatch("OnHovering")]
class MenuButtonHoveringPatch
{
    [HarmonyPostfix]
    static void Postfix(MenuButton __instance)
    {
        try
        {
            // 确保实例有效
            if (__instance == null)
                return;

            // 检查游戏对象是否有效
            if (__instance.gameObject == null || !__instance.gameObject.activeInHierarchy)
                return;

            // 安全查找按钮标记组件
            DecorationButtonMarker? marker = null;
            try
            {
                // 使用更安全的方式查找按钮标记
                foreach (var m in MoreHeadUI.buttonMarkers.Values)
                {
                    if (m != null && m.gameObject != null && m.gameObject == __instance.gameObject)
                    {
                        marker = m;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // 查找失败则直接返回
                return;
            }

            // 如果未找到匹配的标记或标记无效，则退出
            if (marker == null || marker.Decoration == null || marker.HasHandledHover)
            {
                return;
            }

            var decoration = marker.Decoration;
            if (string.IsNullOrEmpty(decoration.ModName))
            {
                return;
            }

            // 使用decorationButtons获取按钮
            if (MoreHeadUI.decorationButtons.TryGetValue(decoration.Name ?? string.Empty, out var button))
            {
                if (button == null || button.labelTMP == null)
                    return;

                // 检查当前文本是否已经包含模组名称
                if (!button.labelTMP.text.Contains(decoration.ModName))
                {
                    // 直接在当前文本后添加模组名称
                    button.labelTMP.text = $"{button.labelTMP.text} <size=12><color=#AAAAAA>- {decoration.ModName}</color></size>";
                }
            }

            // 标记已处理
            marker.HasHandledHover = true;
        }
        catch (Exception e)
        {
            Morehead.Logger?.LogError($"MenuButton.OnHovering补丁出错: {e.Message}\n{e.StackTrace}");
        }
    }
}

[HarmonyPatch(typeof(MenuButton))]
[HarmonyPatch("OnHoverEnd")]
class MenuButtonHoverEndPatch
{
    [HarmonyPostfix]
    static void Postfix(MenuButton __instance)
    {
        try
        {
            // 确保实例有效
            if (__instance == null)
                return;

            // 检查游戏对象是否有效
            if (__instance.gameObject == null || !__instance.gameObject.activeInHierarchy)
                return;
                
            // 安全查找按钮标记组件
            DecorationButtonMarker? marker = null;
            try
            {
                // 使用更安全的方式查找按钮标记
                foreach (var m in MoreHeadUI.buttonMarkers.Values)
                {
                    if (m != null && m.gameObject != null && m.gameObject == __instance.gameObject)
                    {
                        marker = m;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // 查找失败则直接返回
                return;
            }

            // 如果未找到匹配的标记或标记无效，则退出
            if (marker == null || marker.Decoration == null)
            {
                return;
            }

            var decoration = marker.Decoration;
            if (string.IsNullOrEmpty(decoration.ModName))
            {
                return;
            }

            // 使用decorationButtons获取按钮
            if (MoreHeadUI.decorationButtons.TryGetValue(decoration.Name ?? string.Empty, out var button))
            {
                if (button == null || button.labelTMP == null)
                    return;
                    
                // 移除模组名称部分
                string currentText = button.labelTMP.text;
                string modNamePart = $" <size=12><color=#AAAAAA>- {decoration.ModName}</color></size>";
                int modNameIndex = currentText.IndexOf(modNamePart);
                if (modNameIndex != -1)
                {
                    // 移除整个模组名称部分（包括前面的空格和后面的颜色标签）
                    button.labelTMP.text = currentText.Substring(0, modNameIndex).TrimEnd();
                }
            }

            // 重置处理标志
            marker.HasHandledHover = false;
        }
        catch (Exception e)
        {
            Morehead.Logger?.LogError($"MenuButton.OnHoverEnd补丁出错: {e.Message}\n{e.StackTrace}");
        }
    }
}
