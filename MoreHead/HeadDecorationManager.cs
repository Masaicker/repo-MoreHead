using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Logging;
using UnityEngine;
using System.Reflection;
using BepInEx.Configuration;

namespace MoreHead
{
    // 装饰物信息类
    public class DecorationInfo
    {
        // 装饰物名称
        public string? Name { get; set; }
        
        // 装饰物显示名称
        public string? DisplayName { get; set; }
        
        // 装饰物显示状态
        public bool IsVisible { get; set; }
        
        // 装饰物预制体
        public GameObject? Prefab { get; set; }
        
        // 装饰物父级位置标识
        public string? ParentTag { get; set; }
        
        // 装饰物AB包路径
        public string? BundlePath { get; set; }
        
        // 所属模组名称
        public string? ModName { get; set; }
    }
    
    // 头部装饰物管理器
    public static class HeadDecorationManager
    {
        // 日志记录器
        private static ManualLogSource? Logger => Morehead.Logger;
        
        // 日志控制配置选项
        private static ConfigEntry<bool>? _enableVerboseLogging;
        
        // 装饰物列表
        public static List<DecorationInfo> Decorations { get; private set; } = new List<DecorationInfo>();
        
        // 记录状态发生变化的装饰物
        private static HashSet<string> changedDecorations = new HashSet<string>();
        
        // 装饰物父级路径映射
        private static Dictionary<string?, string> parentPathMap = new Dictionary<string?, string>
        {
            { "head", "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM HEAD BOT/code_head_bot_up/code_head_bot_side/_____________________________________/ANIM HEAD TOP/code_head_top" },
            { "neck", "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM HEAD BOT/code_head_bot_up/code_head_bot_side" },
            { "body", "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/ANIM BODY TOP SCALE" },
            { "hip", "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT" },
            { "leftarm", "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM ARM L/code_arm_l" },
            { "rightarm", "[RIG]/code_lean/code_tilt/ANIM BOT/_____________________________________/ANIM BODY BOT/_____________________________________/ANIM BODY TOP/code_body_top_up/code_body_top_side/_____________________________________/ANIM ARM R/code_arm_r_parent/code_arm_r/ANIM ARM R SCALE" },
            { "leftleg", "[RIG]/code_lean/code_tilt/ANIM BOT/code_leg_twist/_____________________________________/ANIM LEG L BOT/_____________________________________/ANIM LEG L TOP" },
            { "rightleg", "[RIG]/code_lean/code_tilt/ANIM BOT/code_leg_twist/_____________________________________/ANIM LEG R BOT/_____________________________________/ANIM LEG R TOP" },
            { "world", "" } // 世界空间节点路径（根目录，避免角色翻滚动画影响）
        };
        
        // 外部DLL装饰物字典，键为DLL Assembly名称，值为该DLL加载的装饰物列表
        private static Dictionary<string, List<DecorationInfo>> externalDecorations = new Dictionary<string, List<DecorationInfo>>();
        
        // 初始化装饰物管理器
        public static void Initialize()
        {
            try
            {
                // 初始化配置选项
                _enableVerboseLogging = Morehead.Instance?.Config.Bind(
                    "Logging",
                    "EnableVerboseLogging",
                    true,
                    "启用模型加载日志（默认开启） Enable model loading logs (default: on)"
                );
                
                Logger?.LogInfo("正在初始化装饰物管理器...");
                
                // 清空装饰物列表
                Decorations.Clear();
                
                // 加载所有.hhh后缀的AB包
                LoadAllDecorations();
                
                Logger?.LogInfo($"装饰物管理器初始化完成，共加载了 {Decorations.Count} 个装饰物");
            }
            catch (Exception e)
            {
                Logger?.LogError($"初始化装饰物管理器时出错: {e.Message}");
            }
        }
        
        // 加载所有装饰物
        private static void LoadAllDecorations()
        {
            try
            {
                // 获取MOD所在目录
                string? modDirectory = Path.GetDirectoryName(Morehead.Instance?.Info.Location);
                if (string.IsNullOrEmpty(modDirectory))
                {
                    Logger?.LogError("无法获取MOD所在目录");
                    return;
                }
                
                // 创建装饰物目录（如果不存在）
                string decorationsDirectory = Path.Combine(modDirectory, "Decorations");
                if (!Directory.Exists(decorationsDirectory))
                {
                    Directory.CreateDirectory(decorationsDirectory);
                    Logger?.LogInfo($"已创建装饰物目录: {decorationsDirectory}");
                }
                
                // 存储所有找到的.hhh文件
                List<string> allBundleFiles = new List<string>();
                
                // 1. 从Decorations目录加载（保留这一步，因为这是用户最直接放置自定义装饰物的地方）
                string[] decorationsBundleFiles = Directory.GetFiles(decorationsDirectory, "*.hhh");
                allBundleFiles.AddRange(decorationsBundleFiles);
                
                // 2. 查找并加载BepInEx/plugins目录下的所有.hhh文件
                try
                {
                    // 使用 BepInEx.Paths.PluginPath 获取插件目录
                    string pluginsDirectory = BepInEx.Paths.PluginPath;
                    
                    if (!string.IsNullOrEmpty(pluginsDirectory) && Directory.Exists(pluginsDirectory))
                    {
                        //Logger?.LogInfo($"找到BepInEx/plugins目录: {pluginsDirectory}");
                        
                        // 递归搜索plugins目录下的所有.hhh文件
                        string[] pluginsBundleFiles = Directory.GetFiles(pluginsDirectory, "*.hhh", SearchOption.AllDirectories);
                        allBundleFiles.AddRange(pluginsBundleFiles);
                        
                        // Logger?.LogInfo($"在plugins目录中找到 {pluginsBundleFiles.Length} 个.hhh文件");
                    }
                    else
                    {
                        Logger?.LogWarning("无法找到BepInEx/plugins目录，将只加载本地装饰物");
                    }
                }
                catch (Exception e)
                {
                    Logger?.LogError($"搜索plugins目录时出错: {e.Message}");
                }
                
                // 去重（可能有重复的文件路径）
                allBundleFiles = allBundleFiles.Distinct().ToList();
                
                if (allBundleFiles.Count == 0)
                {
                    Logger?.LogWarning("未找到任何装饰物包文件，请确保.hhh文件已放置");
                }
                //else
                //{
                    // Logger?.LogInfo($"找到 {allBundleFiles.Count} 个装饰物包文件");
                    
                    // 记录文件位置的详细信息
                    // if (decorationsBundleFiles.Length > 0)
                    //    Logger?.LogInfo($"- Decorations目录: {decorationsBundleFiles.Length} 个文件");
                //}
                
                // 加载每个装饰物包
                foreach (string bundlePath in allBundleFiles)
                {
                    LoadDecorationBundle(bundlePath);
                }
            }
            catch (Exception e)
            {
                Logger?.LogError($"加载装饰物时出错: {e.Message}");
            }
        }
        
        // 加载单个装饰物包
        private static void LoadDecorationBundle(string bundlePath)
        {
            AssetBundle? assetBundle = null;
            
            try
            {
                // 基本文件验证
                if (!File.Exists(bundlePath))
                {
                    Logger?.LogWarning($"文件不存在: {bundlePath}");
                    return;
                }
                
                // 检查文件大小，过小的文件可能不是有效的AssetBundle
                FileInfo fileInfo = new FileInfo(bundlePath);
                if (fileInfo.Length < 1024) // 小于1KB的文件可能不是有效的AssetBundle
                {
                    Logger?.LogWarning($"文件过小，可能不是有效的AssetBundle: {bundlePath}, 大小: {fileInfo.Length} 字节");
                    return;
                }
                
                // 从文件名获取装饰物名称和父级标签
                string fileName = Path.GetFileNameWithoutExtension(bundlePath);
                
                // 尝试从文件名中提取父级标签
                string? parentTag = "head"; // 默认为head
                string bundleBaseName = fileName;
                
                // 检查文件名是否包含下划线，格式应为 name_tag
                if (fileName.Contains("_"))
                {
                    string[] parts = fileName.Split('_');
                    if (parts.Length >= 2)
                    {
                        // 最后一部分作为标签
                        string? possibleTag = parts[parts.Length - 1].ToLower();
                        
                        // 检查是否是有效的父级标签
                        if (parentPathMap.ContainsKey(possibleTag))
                        {
                            parentTag = possibleTag;
                            // 重建装饰物名称（不包括标签部分）
                            bundleBaseName = string.Join("_", parts, 0, parts.Length - 1);
                        }
                    }
                }
                
                // 确保Name是唯一的
                string uniqueName = EnsureUniqueName(bundleBaseName);
                if (uniqueName != bundleBaseName)
                {
                    Logger?.LogWarning($"检测到重名，将基础名称从 {bundleBaseName} 修改为 {uniqueName}");
                    bundleBaseName = uniqueName;
                }
                
                // 尝试加载AssetBundle
                try
                {
                    assetBundle = UnityEngine.AssetBundle.LoadFromFile(bundlePath);
                    if (assetBundle == null)
                    {
                        Logger?.LogError($"无法加载AssetBundle，文件可能已损坏或不是有效的AssetBundle: {bundlePath}");
                        return;
                    }
                }
                catch (Exception e)
                {
                    Logger?.LogError($"加载AssetBundle时出错，文件可能不是有效的AssetBundle: {bundlePath}, 错误: {e.Message}");
                    return;
                }
                
                try
                {
                    // 获取所有资源名称
                    string[] assetNames;
                    try
                    {
                        assetNames = assetBundle.GetAllAssetNames();
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"获取AssetBundle资源名称时出错: {bundlePath}, 错误: {e.Message}");
                        assetBundle.Unload(true);
                        return;
                    }
                    
                    if (assetNames.Length == 0)
                    {
                        Logger?.LogWarning($"AssetBundle不包含任何资源: {bundlePath}");
                        assetBundle.Unload(true);
                        return;
                    }
                    
                    // 验证资源类型
                    bool foundValidAsset = false;
                    GameObject? prefab = null;
                    
                    foreach (string assetName in assetNames)
                    {
                        try
                        {
                            // 尝试加载为GameObject
                            prefab = assetBundle.LoadAsset<GameObject>(assetName);
                            if (prefab != null)
                            {
                                foundValidAsset = true;
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger?.LogWarning($"加载资源 {assetName} 时出错: {e.Message}");
                            // 继续尝试下一个资源
                        }
                    }
                    
                    if (!foundValidAsset || prefab == null)
                    {
                        Logger?.LogWarning($"AssetBundle不包含有效的GameObject资源: {bundlePath}");
                        assetBundle.Unload(true);
                        return;
                    }
                    
                    // 使用预制体的名称作为装饰物的显示名称
                    string displayName = prefab.name;
                    
                    // 确保DisplayName在UI中是唯一的
                    string uniqueDisplayName = EnsureUniqueDisplayName(displayName);
                    if (uniqueDisplayName != displayName)
                    {
                        Logger?.LogWarning($"检测到显示名称重复，将显示名称从 {displayName} 修改为 {uniqueDisplayName}");
                        displayName = uniqueDisplayName;
                    }
                    
                    // 创建装饰物信息
                    DecorationInfo decoration = new DecorationInfo
                    {
                        Name = bundleBaseName, // 用于查找和标识的名称（从文件名中提取）
                        DisplayName = displayName, // 用于UI显示的名称（从预制体中获取）
                        IsVisible = false, // 默认不显示
                        Prefab = prefab,
                        ParentTag = parentTag,
                        BundlePath = bundlePath,
                        ModName = GetModNameFromPath(bundlePath)
                    };
                    
                    // 添加到装饰物列表
                    Decorations.Add(decoration);
                    if (_enableVerboseLogging?.Value ?? true)
                    {
                        Logger?.LogInfo($"成功加载装饰物: {decoration.DisplayName}, 标签: {decoration.ParentTag}");
                    }
                    
                    // 卸载AssetBundle但保留已加载的资源
                    assetBundle.Unload(false);
                }
                catch (Exception e)
                {
                    Logger?.LogError($"处理AssetBundle时出错: {e.Message}");
                    if (assetBundle != null)
                    {
                        assetBundle.Unload(true);
                    }
                }
            }
            catch (Exception e)
            {
                Logger?.LogError($"加载装饰物包时出错: {e.Message}, 路径: {bundlePath}");
                if (assetBundle != null)
                {
                    assetBundle.Unload(true);
                }
            }
        }
        
        // 确保Name是唯一的
        private static string EnsureUniqueName(string baseName)
        {
            string name = baseName;
            int counter = 1;
            
            // 检查是否已存在同名装饰物
            while (Decorations.Any(d => d.Name != null && d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                name = $"{baseName}({counter})";
                counter++;
            }
            
            return name;
        }
        
        // 确保DisplayName在UI中是唯一的
        private static string EnsureUniqueDisplayName(string baseDisplayName)
        {
            string displayName = baseDisplayName;
            int counter = 1;
            
            // 检查是否已存在同名显示名称
            while (Decorations.Any(d => d.DisplayName != null && d.DisplayName.Equals(displayName, StringComparison.OrdinalIgnoreCase)))
            {
                displayName = $"{baseDisplayName}({counter})";
                counter++;
            }
            
            return displayName;
        }
        
        // 从预制体获取父级标签
        private static string? GetParentTagFromPrefab(GameObject prefab)
        {
            // 尝试从预制体名称中获取标签
            string name = prefab.name.ToLower();
            
            // 检查名称是否包含已知的父级标签
            foreach (var tag in parentPathMap.Keys)
            {
                if (tag != null && name.Contains(tag))
                {
                    return tag;
                }
            }
            
            // 尝试获取自定义组件或属性
            // 这里可以扩展，例如检查自定义组件或标签
            
            return null; // 返回null表示使用默认值
        }
        
        // 获取装饰物显示状态
        public static bool GetDecorationState(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var decoration = Decorations.FirstOrDefault(d => d.Name == name);
            return decoration?.IsVisible ?? false;
        }
        
        // 设置装饰物显示状态
        public static void SetDecorationState(string name, bool isVisible)
        {
            if (string.IsNullOrEmpty(name)) return;
            var decoration = Decorations.FirstOrDefault(d => d.Name == name);
            if (decoration != null && decoration.IsVisible != isVisible)
            {
                decoration.IsVisible = isVisible;
                changedDecorations.Add(name); // 记录状态发生变化的装饰物
            }
        }
        
        // 切换装饰物显示状态
        public static bool ToggleDecorationState(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var decoration = Decorations.FirstOrDefault(d => d.Name == name);
            if (decoration != null)
            {
                // 切换状态
                decoration.IsVisible = !decoration.IsVisible;
                changedDecorations.Add(name); // 记录状态发生变化的装饰物
                return decoration.IsVisible;
            }
            return false;
        }
        
        // 获取父级路径
        public static string GetParentPath(string parentTag)
        {
            if (parentPathMap.TryGetValue(parentTag.ToLower(), out string path))
            {
                return path;
            }
            
            // 默认返回头部路径
            return parentPathMap["head"];
        }
        
        // 获取发生状态变化的装饰物集合
        public static List<string> GetChangedDecorations()
        {
            return changedDecorations.ToList();
        }
        
        // 清除变化记录
        public static void ClearChangedDecorations()
        {
            changedDecorations.Clear();
        }
        
        // 禁用所有装饰物
        public static void DisableAllDecorations()
        {
            try
            {
                // 关闭所有装饰物
                foreach (var decoration in Decorations)
                {
                    if (decoration.IsVisible)
                    {
                        decoration.IsVisible = false;
                        changedDecorations.Add(decoration.Name ?? string.Empty); // 记录变化
                    }
                }
                
                // Logger?.LogInfo("已关闭所有装饰物");
            }
            catch (Exception e)
            {
                Logger?.LogError($"禁用所有装饰物时出错: {e.Message}");
            }
        }

        public static bool LoadExternalAssetBundle(byte[] bundleData, string resourceName)
        {
            try
            {
                // 创建临时文件，使用原始资源名
                string tempPath = Path.Combine(Path.GetTempPath(), resourceName);
                File.WriteAllBytes(tempPath, bundleData);

                // 使用现有的加载逻辑，将进行文件名解析
                LoadDecorationBundle(tempPath);

                // 清理临时文件
                try
                {
                    File.Delete(tempPath);
                }
                catch (Exception e)
                {
                    Logger?.LogWarning($"清理临时文件失败: {e.Message}");
                }

                return true;
            }
            catch (Exception e)
            {
                Logger?.LogError($"加载外部AB包失败: {e.Message}");
                return false;
            }
        }

        // 加载外部DLL中的所有.hhh资源
        public static void LoadExternalAssetBundlesFromAssembly(Assembly assembly)
        {
            try
            {
                int loadedCount = 0;
                string assemblyName = assembly.GetName().Name;
                
                // 获取DLL文件名（包含.dll后缀）作为模组名
                string dllPath = assembly.Location;
                string modName = Path.GetFileName(dllPath);
                
                // 初始化该Assembly的装饰物列表
                if (!externalDecorations.ContainsKey(assemblyName))
                {
                    externalDecorations[assemblyName] = new List<DecorationInfo>();
                }
                else
                {
                    // 如果已存在，先清空之前的记录
                    externalDecorations[assemblyName].Clear();
                }
                
                // 记录当前Decorations的数量，用于之后识别新增的装饰物
                int startIndex = Decorations.Count;
                
                // 获取所有嵌入资源名称
                string[] resourceNames = assembly.GetManifestResourceNames();
                
                // 筛选出.hhh后缀的资源
                var hhhResources = resourceNames.Where(name => name.EndsWith(".hhh", StringComparison.OrdinalIgnoreCase));
                
                if (!hhhResources.Any())
                {
                    Logger?.LogWarning($"在DLL {assemblyName} 中未找到.hhh资源");
                    return;
                }
                
                Logger?.LogInfo($"在DLL {assemblyName} 中找到 {hhhResources.Count()} 个.hhh资源");
                
                // 加载每个.hhh资源
                foreach (string resourceName in hhhResources)
                {
                    try
                    {
                        // 从资源名称中提取文件名
                        string fileName;
                        // 资源名称格式通常是：MyNamespace.MyHat_head.hhh
                        // 需要提取的是MyHat_head，而不是hhh
                        string withoutExtension = resourceName.Substring(0, resourceName.Length - 4); // 去掉.hhh
                        if (withoutExtension.Contains("."))
                        {
                            // 如果有命名空间，获取最后一个.后面的部分
                            fileName = withoutExtension.Split('.').Last();
                        }
                        else
                        {
                            fileName = withoutExtension;
                        }

                        // 添加调试日志
                        Logger?.LogInfo($"处理资源: {resourceName}, 提取文件名: {fileName}");
                        
                        // 读取资源数据
                        byte[] bundleData;
                        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                        {
                            if (stream == null) continue;
                            
                            bundleData = new byte[stream.Length];
                            stream.Read(bundleData, 0, bundleData.Length);
                        }
                        
                        // 加载AB包
                        if (LoadExternalAssetBundle(bundleData, fileName + ".hhh"))
                        {
                            loadedCount++;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"加载资源 {resourceName} 失败: {e.Message}");
                    }
                }
                
                // 将新增的装饰物添加到该Assembly的记录中
                if (loadedCount > 0)
                {
                    // 将新增的装饰物添加到externalDecorations中，并设置模组名
                    for (int i = startIndex; i < Decorations.Count; i++)
                    {
                        var decoration = Decorations[i];
                        decoration.ModName = modName; // 设置DLL文件名作为模组名
                        externalDecorations[assemblyName].Add(decoration);
                    }
                    
                    Logger?.LogInfo($"成功从DLL {assemblyName} 加载了 {loadedCount} 个资源，已保存到外部装饰物记录中");
                    // 应用已保存的装饰物状态
                    ConfigManager.ApplySavedStates();
                }
            }
            catch (Exception e)
            {
                Logger?.LogError($"从DLL加载资源失败: {e.Message}");
            }
        }

        // 获取指定DLL加载的所有装饰物
        public static List<DecorationInfo> GetDecorationsFromAssembly(Assembly assembly)
        {
            string assemblyName = assembly.GetName().Name;
            if (externalDecorations.TryGetValue(assemblyName, out List<DecorationInfo> decorations))
            {
                return decorations;
            }
            return new List<DecorationInfo>(); // 返回空列表而不是null
        }
        
        // 获取指定DLL加载的所有装饰物的GameObject列表
        public static List<GameObject> GetDecorationGameObjectsFromAssembly(Assembly assembly)
        {
            List<GameObject> result = new List<GameObject>();
            
            foreach (DecorationInfo decoration in GetDecorationsFromAssembly(assembly))
            {
                if (decoration.Prefab != null)
                {
                    result.Add(decoration.Prefab);
                }
            }
            
            return result;
        }
        
        // 根据名称查找指定DLL加载的装饰物
        public static DecorationInfo GetDecorationByName(Assembly assembly, string decorationName)
        {
            List<DecorationInfo> assemblyDecorations = GetDecorationsFromAssembly(assembly);
            return assemblyDecorations.FirstOrDefault(d => 
                d.Name != null && d.Name.Equals(decorationName, StringComparison.OrdinalIgnoreCase) ||
                d.DisplayName != null && d.DisplayName.Equals(decorationName, StringComparison.OrdinalIgnoreCase)
            );
        }
        
        // 根据部分名称搜索指定DLL加载的装饰物（模糊匹配）
        public static List<DecorationInfo> FindDecorationsByPartialName(Assembly assembly, string partialName)
        {
            if (string.IsNullOrEmpty(partialName))
            {
                return new List<DecorationInfo>();
            }
            
            List<DecorationInfo> assemblyDecorations = GetDecorationsFromAssembly(assembly);
            return assemblyDecorations.Where(d => 
                (d.Name != null && d.Name.IndexOf(partialName, StringComparison.OrdinalIgnoreCase) >= 0) || 
                (d.DisplayName != null && d.DisplayName.IndexOf(partialName, StringComparison.OrdinalIgnoreCase) >= 0)
            ).ToList();
        }

        // 重新创建UI（供第三方MOD使用）
        public static void RecreateUI()
        {
            try
            {
                // 调用MoreHeadUI的RecreateUI方法
                MoreHeadUI.RecreateUI();
                
                // 不再记录日志，避免重复
            }
            catch (Exception e)
            {
                Logger?.LogError($"重新创建UI时出错: {e.Message}");
            }
        }

        // 获取模组名称
        private static string? GetModNameFromPath(string? bundlePath)
        {
            try
            {
                if (string.IsNullOrEmpty(bundlePath))
                    return null;
                
                // 获取官方插件目录
                string pluginsDirectory = BepInEx.Paths.PluginPath;
                
                // 验证插件目录是否有效
                if (string.IsNullOrEmpty(pluginsDirectory) || !Directory.Exists(pluginsDirectory))
                {
                    return null;
                }
                
                // 检查是否在BepInEx/plugins目录下
                if (!bundlePath.StartsWith(pluginsDirectory, StringComparison.OrdinalIgnoreCase))
                    return null;
                
                // 获取文件所在的文件夹
                string? parentDirectory = Path.GetDirectoryName(bundlePath);
                if (string.IsNullOrEmpty(parentDirectory))
                    return null;
                
                // 处理直到找到plugins目录下的文件夹
                string directoryName = parentDirectory;
                while (!string.IsNullOrEmpty(directoryName) && 
                       !Directory.GetParent(directoryName)?.FullName.Equals(pluginsDirectory, StringComparison.OrdinalIgnoreCase) == true)
                {
                    directoryName = Directory.GetParent(directoryName)?.FullName ?? string.Empty;
                }
                
                if (string.IsNullOrEmpty(directoryName))
                    return null;
                
                // 获取文件夹名称
                string folderName = Path.GetFileName(directoryName);
                
                // 检查文件夹名称中的破折号
                string[] parts = folderName.Split('-');
                if (parts.Length <= 1)
                {
                    // 没有破折号，返回整个文件夹名
                    return folderName;
                }
                
                // 检查最后一部分是否可能是版本号
                string lastPart = parts[parts.Length - 1];
                bool isLastPartVersion = IsLikelyVersionNumber(lastPart);
                
                if (isLastPartVersion && parts.Length > 2)
                {
                    // 如果最后一部分像是版本号，且有多个破折号，使用倒数第二个破折号后的部分作为模组名
                    return parts[parts.Length - 2];
                }
                else if (isLastPartVersion)
                {
                    // 如果只有一个破折号且最后一部分是版本号，使用第一部分
                    return parts[0];
                }
                else
                {
                    // 如果最后一部分不像是版本号，按原逻辑使用它作为模组名
                    return lastPart;
                }
            }
            catch (Exception e)
            {
                Morehead.Logger?.LogError($"获取模组名称时出错: {e.Message}");
                return null;
            }
        }
        
        // 检查字符串是否可能是版本号
        private static bool IsLikelyVersionNumber(string text)
        {
            // 版本号通常包含数字，且可能有点或其他分隔符
            // 检查是否以数字开头
            if (text.Length == 0 || !char.IsDigit(text[0]))
                return false;
                
            // 检查是否包含常见版本号模式，如 1.0, v1.0, 1.0.0, 1.0.0.1 等
            bool hasNumericPart = false;
            bool hasSeparator = false;
            
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    hasNumericPart = true;
                }
                else if (c == '.' || c == '_' || c == '-')
                {
                    hasSeparator = true;
                }
            }
            
            // 版本号通常有数字和分隔符
            return hasNumericPart && hasSeparator;
        }
    }
}
