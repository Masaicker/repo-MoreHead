using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // 使用 Newtonsoft.Json 库
using BepInEx.Logging;
using UnityEngine; // 添加UnityEngine引用，用于访问Application.persistentDataPath

namespace MoreHead
{
    // 配置管理器
    public static class ConfigManager
    {
        // 日志记录器
        private static ManualLogSource? Logger => Morehead.Logger;
        
        // MOD数据文件夹名称
        private const string MOD_DATA_FOLDER = "REPOModData";
        
        // MOD特定文件夹名称
        private const string MOD_FOLDER = "MoreHead";
        
        // 配置文件名称
        private const string CONFIG_FILENAME = "MoreHeadConfig.json";
        
        // 多套方案配置文件名称
        private const string OUTFIT_CONFIG_FILENAME = "MoreHeadOutfits.json";
        
        // 当前选中的装备方案索引 (1-5)
        public static int CurrentOutfitIndex { get; private set; } = 1;
        
        // 新的配置文件路径（Unity通用存档位置）
        private static string NewConfigFilePath => Path.Combine(
            Application.persistentDataPath, // Unity通用存档位置
            MOD_DATA_FOLDER,               // MOD数据总文件夹
            MOD_FOLDER,                    // 本MOD特定文件夹
            CONFIG_FILENAME                // 配置文件名
        );
        
        // 多套方案配置文件路径
        private static string OutfitConfigFilePath => Path.Combine(
            Application.persistentDataPath, // Unity通用存档位置
            MOD_DATA_FOLDER,               // MOD数据总文件夹
            MOD_FOLDER,                    // 本MOD特定文件夹
            OUTFIT_CONFIG_FILENAME         // 多套方案配置文件名
        );
        
        // 旧的BepInEx配置文件路径
        private static string BepInExConfigFilePath => Path.Combine(
            BepInEx.Paths.ConfigPath,      // BepInEx配置目录
            CONFIG_FILENAME                // 配置文件名
        );
        
        // 更旧的配置文件路径（用于迁移）
        private static string OldConfigFilePath => Path.Combine(
            Path.GetDirectoryName(Morehead.Instance?.Info.Location) ?? string.Empty,
            "MoreHeadConfig.txt"
        );
        
        // 装饰物状态字典 - 用于向后兼容
        private static Dictionary<string?, bool> _decorationStates = new Dictionary<string?, bool>();
        
        // 多套装备方案字典 - 索引 1-5 对应 5 套装备方案
        private static Dictionary<int, Dictionary<string?, bool>> _outfitStates = new Dictionary<int, Dictionary<string?, bool>>();
        
        // 配置类 - 用于JSON序列化
        private class ConfigData
        {
            public Dictionary<int, Dictionary<string, bool>> OutfitStates { get; set; } = new Dictionary<int, Dictionary<string, bool>>();
            public int CurrentOutfitIndex { get; set; } = 1;
        }
        
        // 初始化配置管理器
        public static void Initialize()
        {
            try
            {
                // 确保MOD数据目录存在
                EnsureModDataDirectoryExists();
                
                // 初始化多套装备配置
                InitializeOutfitStates();
                
                // 加载配置
                LoadConfig();
                
                // 应用当前选择的装备方案
                ApplyOutfit(CurrentOutfitIndex);
            }
            catch (Exception e)
            {
                Logger?.LogError($"初始化配置管理器时出错: {e.Message}");
            }
        }
        
        // 初始化多套装备配置
        private static void InitializeOutfitStates()
        {
            // 确保有 5 套方案的空字典
            for (int i = 1; i <= 5; i++)
            {
                if (!_outfitStates.ContainsKey(i))
                {
                    _outfitStates[i] = new Dictionary<string?, bool>();
                }
            }
        }
        
        // 确保MOD数据目录存在
        private static void EnsureModDataDirectoryExists()
        {
            try
            {
                // 创建MOD数据总文件夹
                string modDataPath = Path.Combine(Application.persistentDataPath, MOD_DATA_FOLDER);
                if (!Directory.Exists(modDataPath))
                {
                    Directory.CreateDirectory(modDataPath);
                    Logger?.LogInfo($"已创建MOD数据总文件夹: {modDataPath}");
                }
                
                // 创建本MOD特定文件夹
                string modFolderPath = Path.Combine(modDataPath, MOD_FOLDER);
                if (!Directory.Exists(modFolderPath))
                {
                    Directory.CreateDirectory(modFolderPath);
                    Logger?.LogInfo($"已创建MOD特定文件夹: {modFolderPath}");
                }
            }
            catch (Exception e)
            {
                Logger?.LogError($"创建MOD数据目录时出错: {e.Message}");
            }
        }
        
        // 加载配置
        private static void LoadConfig()
        {
            try
            {
                // 清空状态字典
                _decorationStates.Clear();
                
                // 先加载多套装备方案配置
                LoadOutfitConfig();
                
                // 首先尝试从新的Unity存档位置加载单套配置(向后兼容)
                if (File.Exists(NewConfigFilePath))
                {
                    if (LoadJsonConfig(NewConfigFilePath))
                    {
                        // Logger?.LogInfo($"已从Unity存档位置加载配置: {NewConfigFilePath}");
                        
                        // 如果多套装备方案 1 为空，则用旧配置填充
                        if (_outfitStates[1].Count == 0)
                        {
                            _outfitStates[1] = new Dictionary<string?, bool>(_decorationStates);
                            SaveOutfitConfig(); // 保存多套方案配置
                            Logger?.LogInfo("已将单套配置迁移到第1套装备方案");
                        }
                        
                        return; // 成功加载，直接返回
                    }
                }
                
                // 如果从新位置加载失败，尝试从BepInEx配置目录加载并迁移
                if (File.Exists(BepInExConfigFilePath))
                {
                    if (LoadJsonConfig(BepInExConfigFilePath))
                    {
                        Logger?.LogInfo($"已从BepInEx配置目录加载配置: {BepInExConfigFilePath}");
                        
                        // 立即保存到新位置
                        SaveConfigWithoutUpdate();
                        Logger?.LogInfo($"已将配置从BepInEx目录迁移到Unity存档位置: {NewConfigFilePath}");
                        
                        // 如果多套装备方案 1 为空，则用旧配置填充
                        if (_outfitStates[1].Count == 0)
                        {
                            _outfitStates[1] = new Dictionary<string?, bool>(_decorationStates);
                            SaveOutfitConfig(); // 保存多套方案配置
                            Logger?.LogInfo("已将单套配置迁移到第1套装备方案");
                        }
                        
                        // 尝试删除旧配置文件（可选）
                        try
                        {
                            File.Delete(BepInExConfigFilePath);
                            Logger?.LogInfo($"已删除BepInEx配置文件: {BepInExConfigFilePath}");
                        }
                        catch (Exception ex)
                        {
                            // 删除失败不影响主程序，只记录日志
                            Logger?.LogWarning($"删除BepInEx配置文件失败: {ex.Message}");
                        }
                        
                        return; // 成功加载并迁移，直接返回
                    }
                }
                
                // 如果前两种方式都失败，尝试从最旧的位置加载文本配置并迁移
                if (File.Exists(OldConfigFilePath))
                {
                    try
                    {
                        // 读取所有行
                        string[] lines = File.ReadAllLines(OldConfigFilePath);
                        
                        // 解析每一行
                        foreach (string line in lines)
                        {
                            // 跳过空行
                            if (string.IsNullOrWhiteSpace(line))
                                continue;
                            
                            // 分割行内容
                            string[] parts = line.Split('=');
                            if (parts.Length == 2)
                            {
                                string? name = parts[0].Trim();
                                bool isVisible = parts[1].Trim().Equals("1", StringComparison.OrdinalIgnoreCase);
                                
                                // 添加到状态字典
                                _decorationStates[name] = isVisible;
                            }
                        }
                        
                        if (_decorationStates.Count > 0)
                        {
                            Logger?.LogInfo($"已从旧文本格式加载配置，包含 {_decorationStates.Count} 个装饰物状态");
                            
                            // 立即保存为新格式到新位置
                            SaveConfigWithoutUpdate();
                            Logger?.LogInfo($"已将旧文本格式配置迁移到新的JSON格式: {NewConfigFilePath}");
                            
                            // 如果多套装备方案 1 为空，则用旧配置填充
                            if (_outfitStates[1].Count == 0)
                            {
                                _outfitStates[1] = new Dictionary<string?, bool>(_decorationStates);
                                SaveOutfitConfig(); // 保存多套方案配置
                                Logger?.LogInfo("已将单套配置迁移到第1套装备方案");
                            }
                            
                            // 尝试删除旧配置文件
                            try
                            {
                                File.Delete(OldConfigFilePath);
                                Logger?.LogInfo($"已删除旧文本配置文件: {OldConfigFilePath}");
                            }
                            catch (Exception ex)
                            {
                                // 删除失败不影响主程序，只记录日志
                                Logger?.LogWarning($"删除旧文本配置文件失败: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"从旧文本格式加载配置时出错: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Logger?.LogError($"加载配置时出错: {e.Message}");
                
                // 清空状态字典
                _decorationStates.Clear();
            }
        }
        
        // 加载多套装备方案配置
        private static void LoadOutfitConfig()
        {
            try
            {
                // 确保初始化
                InitializeOutfitStates();
                
                // 如果多套方案配置文件存在，则加载
                if (File.Exists(OutfitConfigFilePath))
                {
                    // 读取JSON文件内容
                    string jsonContent = File.ReadAllText(OutfitConfigFilePath);
                    
                    // 反序列化JSON到配置类
                    var configData = JsonConvert.DeserializeObject<ConfigData>(jsonContent);
                    
                    // 将加载的状态添加到字典
                    if (configData != null)
                    {
                        // 清空现有配置
                        _outfitStates.Clear();
                        
                        // 添加加载的配置
                        foreach (var kvp in configData.OutfitStates)
                        {
                            _outfitStates[kvp.Key] = new Dictionary<string?, bool>();
                            
                            foreach (var stateKvp in kvp.Value)
                            {
                                _outfitStates[kvp.Key][stateKvp.Key] = stateKvp.Value;
                            }
                        }
                        
                        // 设置当前装备方案索引
                        CurrentOutfitIndex = configData.CurrentOutfitIndex;
                        
                        // 确保索引有效
                        if (CurrentOutfitIndex < 1 || CurrentOutfitIndex > 5)
                        {
                            CurrentOutfitIndex = 1;
                        }
                        
                        return;
                    }
                }
                
                // 确保至少有 5 套空方案
                InitializeOutfitStates();
            }
            catch (Exception e)
            {
                Logger?.LogError($"加载多套装备方案配置时出错: {e.Message}");
                
                // 确保至少有 5 套空方案
                InitializeOutfitStates();
            }
        }
        
        // 从JSON文件加载配置
        private static bool LoadJsonConfig(string filePath)
        {
            try
            {
                // 读取JSON文件内容
                string jsonContent = File.ReadAllText(filePath);
                
                // 反序列化JSON到字典
                var loadedStates = JsonConvert.DeserializeObject<Dictionary<string, bool>>(jsonContent);
                
                // 将加载的状态添加到字典
                if (loadedStates != null)
                {
                    foreach (var kvp in loadedStates)
                    {
                        _decorationStates[kvp.Key] = kvp.Value;
                    }
                    
                    Logger?.LogInfo($"已从JSON加载配置，包含 {_decorationStates.Count} 个装饰物状态");
                    return true; // 成功加载
                }
            }
            catch (JsonException je)
            {
                Logger?.LogError($"解析JSON配置文件时出错: {je.Message}");
            }
            catch (Exception e)
            {
                Logger?.LogError($"加载JSON配置文件时出错: {e.Message}");
            }
            
            return false; // 加载失败
        }
        
        // 保存配置 (单套系统-向后兼容)
        public static void SaveConfig()
        {
            try
            {
                // 更新配置数据
                UpdateConfigData();
                
                // 保存到文件
                SaveToFile();
                
                // 同时保存当前装备状态到当前所选的装备方案
                SaveCurrentOutfit();
            }
            catch (Exception e)
            {
                Logger?.LogError($"保存配置时出错: {e.Message}");
            }
        }
        
        // 保存配置但不更新数据（用于迁移）
        private static void SaveConfigWithoutUpdate()
        {
            try
            {
                // 直接保存当前状态字典，不更新数据
                SaveToFile();
            }
            catch (Exception e)
            {
                Logger?.LogError($"保存配置时出错: {e.Message}");
            }
        }
        
        // 保存到文件
        private static void SaveToFile()
        {
            try
            {
                // 确保配置目录存在
                EnsureModDataDirectoryExists();
                
                // 序列化为JSON，使用格式化输出提高可读性
                string jsonContent = JsonConvert.SerializeObject(_decorationStates, Formatting.Indented);
                
                // 写入配置文件
                File.WriteAllText(NewConfigFilePath, jsonContent);
            }
            catch (Exception e)
            {
                Logger?.LogError($"写入配置文件时出错: {e.Message}");
            }
        }
        
        // 保存多套装备方案配置
        private static void SaveOutfitConfig()
        {
            try
            {
                // 确保配置目录存在
                EnsureModDataDirectoryExists();
                
                // 创建配置数据对象
                var configData = new ConfigData
                {
                    OutfitStates = new Dictionary<int, Dictionary<string, bool>>(),
                    CurrentOutfitIndex = CurrentOutfitIndex
                };
                
                // 复制装备方案数据
                foreach (var kvp in _outfitStates)
                {
                    configData.OutfitStates[kvp.Key] = new Dictionary<string, bool>();
                    
                    foreach (var stateKvp in kvp.Value)
                    {
                        if (stateKvp.Key != null)
                        {
                            configData.OutfitStates[kvp.Key][stateKvp.Key] = stateKvp.Value;
                        }
                    }
                }
                
                // 序列化为JSON，使用格式化输出提高可读性
                string jsonContent = JsonConvert.SerializeObject(configData, Formatting.Indented);
                
                // 写入配置文件
                File.WriteAllText(OutfitConfigFilePath, jsonContent);
            }
            catch (Exception e)
            {
                Logger?.LogError($"写入多套装备方案配置文件时出错: {e.Message}");
            }
        }
        
        // 更新配置数据 (单套系统-向后兼容)
        private static void UpdateConfigData()
        {
            // 清空装饰物状态
            _decorationStates.Clear();
            
            // 添加当前装饰物状态
            foreach (var decoration in HeadDecorationManager.Decorations)
            {
                _decorationStates[decoration.Name] = decoration.IsVisible;
            }
        }
        
        // 应用已保存的装饰物状态 (单套系统-向后兼容)
        public static void ApplySavedStates()
        {
            try
            {
                int appliedCount = 0;
                
                // 遍历所有装饰物
                foreach (var decoration in HeadDecorationManager.Decorations)
                {
                    // 检查是否有保存的状态
                    if (_decorationStates.TryGetValue(decoration.Name, out bool isVisible))
                    {
                        // 应用保存的状态
                        decoration.IsVisible = isVisible;
                        appliedCount++;
                    }
                }
                
                if (appliedCount > 0)
                {
                    Logger?.LogInfo($"已应用 {appliedCount} 个已保存的装饰物状态");
                }
            }
            catch (Exception e)
            {
                Logger?.LogError($"应用已保存的装饰物状态时出错: {e.Message}");
            }
        }
        
        // 切换到指定的装备方案
        public static void SwitchOutfit(int outfitIndex)
        {
            if (outfitIndex < 1 || outfitIndex > 5)
            {
                Logger?.LogWarning($"无效的装备方案索引: {outfitIndex}，索引应在1-5之间");
                return;
            }
            
            try
            {
                // 保存当前方案状态到当前索引（不触发文件保存）
                SaveCurrentOutfitState();
                
                // 更新当前方案索引
                CurrentOutfitIndex = outfitIndex;
                
                // 应用新方案
                ApplyOutfit(outfitIndex);
                
                // 统一执行保存操作
                SaveAllConfigs();
            }
            catch (Exception e)
            {
                Logger?.LogError($"切换装备方案时出错: {e.Message}");
            }
        }
        
        // 保存当前装饰物状态到当前所选的装备方案（只更新内存，不保存文件）
        private static void SaveCurrentOutfitState()
        {
            try
            {
                // 确保索引有效
                if (CurrentOutfitIndex < 1 || CurrentOutfitIndex > 5)
                {
                    Logger?.LogWarning($"无效的当前装备方案索引: {CurrentOutfitIndex}，重置为1");
                    CurrentOutfitIndex = 1;
                }
                
                // 获取当前装备方案
                var currentOutfit = new Dictionary<string?, bool>();
                
                // 记录当前的装饰物状态
                foreach (var decoration in HeadDecorationManager.Decorations)
                {
                    currentOutfit[decoration.Name] = decoration.IsVisible;
                }
                
                // 更新到多套方案中
                _outfitStates[CurrentOutfitIndex] = currentOutfit;
            }
            catch (Exception e)
            {
                Logger?.LogError($"保存当前装备状态到方案时出错: {e.Message}");
            }
        }
        
        // 统一保存所有配置
        private static void SaveAllConfigs()
        {
            try
            {
                // 更新单套配置数据(向后兼容)
                UpdateConfigData();
                
                // 保存单套配置到文件
                SaveToFile();
                
                // 保存多套方案配置到文件
                SaveOutfitConfig();
            }
            catch (Exception e)
            {
                Logger?.LogError($"保存所有配置时出错: {e.Message}");
            }
        }
        
        // 应用装备方案
        private static void ApplyOutfit(int outfitIndex)
        {
            try
            {
                // 检查有效范围
                if (outfitIndex < 1 || outfitIndex > 5)
                {
                    Logger?.LogWarning($"无效的装备方案索引: {outfitIndex}，使用默认方案1");
                    outfitIndex = 1;
                }
                
                // 确保字典中有该方案
                if (!_outfitStates.TryGetValue(outfitIndex, out var outfitStates))
                {
                    Logger?.LogWarning($"找不到装备方案 {outfitIndex}，使用空方案");
                    outfitStates = new Dictionary<string?, bool>();
                }
                
                // 应用方案中的所有装饰物状态
                foreach (var decoration in HeadDecorationManager.Decorations)
                {
                    bool newState = false;
                    if (outfitStates.TryGetValue(decoration.Name ?? string.Empty, out bool state))
                    {
                        newState = state;
                    }
                    
                    // 只有状态不同时才设置，避免不必要的更新
                    if (decoration.IsVisible != newState)
                    {
                        HeadDecorationManager.SetDecorationState(decoration.Name ?? string.Empty, newState);
                    }
                }
                
                // 更新当前方案索引
                CurrentOutfitIndex = outfitIndex;
                
                // 记录日志
                // Logger?.LogInfo($"已应用装备方案 {outfitIndex}");
            }
            catch (Exception e)
            {
                Logger?.LogError($"应用装备方案 {outfitIndex} 时出错: {e.Message}");
            }
        }
        
        // 保存当前装备状态到当前所选的装备方案（包括保存到文件）
        public static void SaveCurrentOutfit()
        {
            try
            {
                // 保存当前状态到内存
                SaveCurrentOutfitState();
                
                // 保存到文件
                SaveOutfitConfig();
            }
            catch (Exception e)
            {
                Logger?.LogError($"保存当前装备状态到方案时出错: {e.Message}");
            }
        }
        
        // 获取当前方案索引
        public static int GetCurrentOutfitIndex()
        {
            return CurrentOutfitIndex;
        }
    }
}
