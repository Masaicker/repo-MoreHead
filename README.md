<small>

# License & Resource Notice / 许可与资源说明

## Code / 代码
The source code of this mod is released under the MIT License.  
本模组源代码采用 MIT 许可开源。

## Assets / 资源
All 3D models, textures, materials, Unity resources, and embedded AssetBundles  
are **not included in MIT** and remain **All Rights Reserved**.  
They may be **used locally** within the game but **may not be redistributed**.  

所有 3D 模型、贴图、材质、Unity 资源文件及内嵌 AssetBundle  
不属于 MIT 许可范围，均为版权所有（All Rights Reserved）。  
可在本地游戏中使用，但禁止在本项目之外复制或再分发。

<br>

**All models were created by YurisCat Calypso, specifically for this mod :3**
<br>
**所有的模型由[尤里的猫·卡里普索](https://space.bilibili.com/1704421)为了这个模组而制作:3**

</small>

![](https://i.imgur.com/i2NHQrX.gif)

# Introduction 简介
A simple and fun decoration mod that provides a **unitypackage** for players to import custom models.
<br>Currently, only a few decorations are available, but more updates are planned.
<br>[nickklmao-MenuLib](https://thunderstore.io/c/repo/p/nickklmao/MenuLib/) mod is required as a prerequisite.
<br>if you encounter strength loss problems, please download the [fixstrength](https://thunderstore.io/c/repo/p/YMC_MHZ/fixstrength/) mod.
<br>

简单好玩的装饰模组，提供 **unitypackage**，供玩家导入自定义模型。
<br>目前仅有少量装饰，未来会持续更新。
<br>需安装 [nickklmao-MenuLib](https://thunderstore.io/c/repo/p/nickklmao/MenuLib/) 前置模组。
<br>如果遇到力量丢失问题，请下载 [fixstrength](https://thunderstore.io/c/repo/p/YMC_MHZ/fixstrength/) 模组。

![](https://i.imgur.com/7wmnl1P.png)
<br>
![](https://i.imgur.com/jtu0xsM.png)
<small>*The chicken beak in the screenshot comes from [EvenMoreHead](https://thunderstore.io/c/repo/p/DEMMERS/EvenMoreHead/).</small>


# How to Use 如何使用

1. **Install this mod** and enter the game.
2. You can find the **"MoreHead" button in the bottom left corner** either **in the lobby** or **after entering a level** (press `ESC`).
3. Click the button to switch cosmetics.

<br>

1. **安装本模组**，进入游戏。
2. **在大厅准备阶段** 或 **正式进入关卡后按 `ESC`**，都能在左下角找到 **"MoreHead" 按钮**。
3. 点击按钮切换化妆品。

<br>![](https://i.imgur.com/Wp7ZedO.jpeg)  
<br>![](https://i.imgur.com/u4v7esZ.jpeg)  
<br>![](https://i.imgur.com/bKOSgHq.jpeg)

<br><br>

# ⚠️ Compatibility Notice 兼容性说明 ⚠️

<details>
  <summary>Click to see 点击展开</summary>

<div style="border: 2px solid red; padding: 10px; background-color: #fff0f0;">

With **`MenuLib`** updated to **`2.1.3`**, **`MoreHead`** must be updated to **`1.3.0`** or later to function properly.

> <mark>⚠️ If you are still using **`MoreHead 1.2.8`** or earlier, please keep **`MenuLib`** at **`2.1.1`** or earlier to avoid compatibility issues.</mark>

For the best experience and stability, we recommend always using the latest version.

<br>

**`MenuLib`** 已更新至 **`2.1.3`**，因此 **`MoreHead`** 需升级至 **`1.3.0`** 或更高版本才能正常运行。

> <mark>⚠️ 如果您仍在使用 **`MoreHead 1.2.8`** 或更早版本，请确保 **`MenuLib`** 仍为 **`2.1.1`** 或更早版本，否则可能会出现兼容性问题。</mark>

为确保最佳体验和稳定性，建议始终使用最新版本。

</div>

---

### Previously: 之前的版本
<div style="border: 1px solid gray; padding: 10px; background-color: #f8f8f8;">

**MenuLib** **2.0.0** → **MoreHead** **1.2.8+** required.  
**MenuLib** **1.0.3** → **MoreHead** **1.1.8+** required.  

**MenuLib** **2.0.0** → 需要 **MoreHead** **1.2.8+** 版本。  
**MenuLib** **1.0.3** → 需要 **MoreHead** **1.1.8+** 版本。

</div>


</details>

<br><br>



# Configuration Options 配置选项

<details>
  <summary>Click to see 点击展开</summary>

## English

MoreHead provides several configuration options that can be modified in the BepInEx configuration file (`BepInEx/config/Mhz.REPOMoreHead.cfg`):

### UI Settings

| Option | Description | Default |
| --- | --- | --- |
| **EscButtonPosX** | X position of MoreHead button in ESC menu | 0 |
| **EscButtonPosY** | Y position of MoreHead button in ESC menu | 0 |
| **LobbyButtonPosX** | X position of MoreHead button in lobby | 0 |
| **LobbyButtonPosY** | Y position of MoreHead button in lobby | 0 |

Adjusting these values allows you to reposition the MoreHead buttons if they conflict with other mod UI elements.

### Blacklist Mode

| Option | Description | Default |
| --- | --- | --- |
| **BlacklistMode** | Set to "ENABLE_BLACKLIST" to enable blacklist functionality | "" (disabled) |

When blacklist mode is enabled:
- Use `Shift + click` on decoration buttons to add/remove items from the blacklist
- Use `Shift + CLEAR ALL` to clear the entire blacklist at once
- ⚠️ All blacklist changes take effect after restarting the game

Blacklisted decorations won't appear in your decoration menu, which is useful for hiding items you don't want to use.

> <mark>💡It's recommended that one player manages the blacklist and shares it via mod code or the `BepInEx/config/MoreHeadBlacklist.json` file to ensure appearance sync in multiplayer.<br>💡Before sharing, make sure to disable Blacklist Mode (clear the config value) to prevent unintended changes from other players.</mark>

### Logging

| Option | Description | Default |
| --- | --- | --- |
| **EnableVerboseLogging** | Enable detailed logs for model loading | false |

Enable this option to see more detailed logs about model loading process, helpful for troubleshooting.

### File Locations

| File | Path | Description |
| --- | --- | --- |
| **MoreHeadConfig.json** | `[Unity.persistentDataPath]/REPOModData/MoreHead/` | Main configuration file storing decoration states |
| **MoreHeadOutfits.json** | `[Unity.persistentDataPath]/REPOModData/MoreHead/` | Stores up to 5 sets of outfit configurations |
| **Mhz.REPOMoreHead.cfg** | `BepInEx/config/` | BepInEx configuration file with UI positioning and other settings |
| **MoreHeadBlacklist.json** | `BepInEx/config/` | Blacklist file for hiding unwanted decorations |
| **Decorations (.hhh files)** | `BepInEx/plugins/` or<br>`BepInEx/plugins/MoreHead/Decorations/` | Custom decoration models |

**Note**: `[Unity.persistentDataPath]` typically resolves to:
- Windows: `C:/Users/[YourName]/AppData/LocalLow/semiwork/Repo/`
- Linux: `~/.config/unity3d/semiwork/Repo/`
- Mac: `~/Library/Application Support/semiwork/Repo/`

### File Descriptions

- **MoreHeadConfig.json**: Stores the enabled/disabled state of each decoration (single outfit mode - for backward compatibility)
- **MoreHeadOutfits.json**: Stores up to 5 different decoration configurations that you can switch between using the UI or function keys F1-F5
- **Mhz.REPOMoreHead.cfg**: Contains settings for UI button positions, blacklist mode toggle, and logging options
- **MoreHeadBlacklist.json**: Contains a list of decoration names that should be hidden from the UI menu (requires blacklist mode to be enabled)
- **Decoration .hhh files**: Asset bundles containing 3D models for decorations, can be placed in either location

## 中文

MoreHead 提供了几个配置选项，可以在BepInEx配置文件中修改（`BepInEx/config/Mhz.REPOMoreHead.cfg`）：

### UI设置

| 选项 | 描述 | 默认值 |
| --- | --- | --- |
| **EscButtonPosX** | ESC菜单中MoreHead按钮的X坐标 | 0 |
| **EscButtonPosY** | ESC菜单中MoreHead按钮的Y坐标 | 0 |
| **LobbyButtonPosX** | 大厅中MoreHead按钮的X坐标 | 0 |
| **LobbyButtonPosY** | 大厅中MoreHead按钮的Y坐标 | 0 |

调整这些值可以重新定位MoreHead按钮，避免与其他模组的UI元素冲突。

### 黑名单模式

| 选项 | 描述 | 默认值 |
| --- | --- | --- |
| **BlacklistMode** | 设置为"ENABLE_BLACKLIST"以启用黑名单功能 | ""（禁用） |

启用黑名单模式后：
- 使用 `Shift + 点击` 化妆品按钮来添加/移除黑名单项目
- 使用 `Shift + CLEAR ALL` 可一次性清空所有黑名单条目
- ⚠️ 所有黑名单相关操作需重启游戏后才会生效

被列入黑名单的化妆品不会出现在您的装饰菜单中，这对于隐藏您不想使用的物品很有用。

> <mark>💡推荐由一名玩家维护黑名单，通过分享模组代码或直接发送 `BepInEx/config/MoreHeadBlacklist.json` 文件，以保证多人游戏时外观同步。<br>💡分享前请务必关闭黑名单模式（清空配置项字符串），以避免其他玩家误触导致数据不同步。</mark>

### 日志记录

| 选项 | 描述 | 默认值 |
| --- | --- | --- |
| **EnableVerboseLogging** | 启用模型加载的详细日志 | false |

启用此选项可以查看更详细的模型加载过程日志，有助于故障排除。

### 文件位置

| 文件 | 路径 | 描述 |
| --- | --- | --- |
| **MoreHeadConfig.json** | `[Unity.persistentDataPath]/REPOModData/MoreHead/` | 主配置文件，存储装饰状态 |
| **MoreHeadOutfits.json** | `[Unity.persistentDataPath]/REPOModData/MoreHead/` | 存储最多5套装备配置 |
| **Mhz.REPOMoreHead.cfg** | `BepInEx/config/` | BepInEx配置文件，包含UI定位和其他设置 |
| **MoreHeadBlacklist.json** | `BepInEx/config/` | 黑名单文件，用于隐藏不需要的装饰 |
| **装饰文件 (.hhh 文件)** | `BepInEx/plugins/` 或<br>`BepInEx/plugins/MoreHead/Decorations/` | 自定义装饰模型 |

**注意**: `[Unity.persistentDataPath]` 通常对应于:
- Windows: `C:/Users/[您的用户名]/AppData/LocalLow/semiwork/Repo/`
- Linux: `~/.config/unity3d/semiwork/Repo/`
- Mac: `~/Library/Application Support/semiwork/Repo/`

### 文件说明

- **MoreHeadConfig.json**: 存储每个装饰的启用/禁用状态（单套装备模式 - 用于向后兼容）
- **MoreHeadOutfits.json**: 存储最多5种不同的装饰配置，可以通过UI或功能键F1-F5切换
- **Mhz.REPOMoreHead.cfg**: 包含UI按钮位置、黑名单模式开关和日志选项的设置
- **MoreHeadBlacklist.json**: 包含应在UI菜单中隐藏的装饰名称列表（需要启用黑名单模式）
- **装饰 .hhh 文件**: 包含装饰3D模型的Asset Bundle，可以放置在任一位置

</details>

<br><br>

# How to Add Models 如何添加模型

<details>
  <summary>Click to see 点击展开</summary>

## English Tutorial

1. **Set up Unity** (2022.3\* recommended, the game uses built-in render pipeline, please pay attention to the corresponding model materials), import `MoreHead-Asset-Pack_v1.3.unitypackage`.  
   > **📥 [Download unitypackage](https://github.com/Masaicker/repo-MoreHead/releases/download/v1.3/MoreHead-Asset-Pack_v1.3.unitypackage)**

2. **Import your model** and **drag `PlayerAvatar` prefab into the scene**.  
   > <mark>Make sure your model is compatible with Unity and properly rigged if animated.</mark>

3. **Find the following parts** in `PlayerAvatar`, where models will be placed:  

    | Name | Description | Tag |
    | --- | --- | --- |
    | **Head decoration (Do Not Move)** | Head part that raises when speaking | `head` |
    | **Neck decoration (Do Not Move)** | Chin/chest area | `neck` |
    | **Body decoration (Do Not Move)** | Body | `body` |
    | **Hip decoration (Do Not Move)** | Hip/lower body area | `hip` |
    | **L-ARM decoration (Do Not Move)** | Left arm | `leftarm` |
    | **R-ARM decoration (Do Not Move)** | Right arm | `rightarm` |
    | **L-LEG decoration (Do Not Move)** | Left leg | `leftleg` |
    | **R-LEG decoration (Do Not Move)** | Right leg | `rightleg` |
    | **World decoration (Do Not Move)** | Follows character position but maintains horizontal orientation | `world` |

    > <mark>⚠️ These positions are anchor references. DO NOT modify coordinates, or your model will be misaligned in-game.</mark><br>
    > <mark>💡 The `world` tag is useful for decorations that should maintain their horizontal orientation regardless of character movement.</mark><br>
    > <mark>💡 Limb-related tags (leftarm, rightarm, leftleg, rightleg) can be viewed together in-game under the "LIMBS" main tag.</mark>

4. **Create an empty object under the target part (referred to as A) and name it**.  
   > <mark>This name will be displayed in-game as your decoration's name.</mark>

5. **Place your model inside A and adjust to a suitable position**.  

6. **Drag A to the `Project` window to create a prefab**.
   > <mark>⚠️ Note: Please avoid adding collider components to decoration models as they may affect character physics, causing rotation issues or unstable movements. For collision interactions, consider alternative techniques.</mark>

7. **Run `Tools → Head Decorations Builder`**, drag your A prefab (or more, batch export is supported) into the script panel, and select the corresponding part from step 3, then **click `Add Model to List`**.  
   > <mark>Make sure to select the correct body part (head/neck/body/hip, etc.) that matches where you placed your decoration.</mark>

8. **Click `Build All AssetBundle`**, then locate the `[your decoration name].hhh` file in the `AssetBundles` folder.  
   > <mark>Only the `.hhh` file is needed for installation. Other generated files can be ignored.</mark>

9. **Move the `.hhh` file to `BepInEx\plugins`** (the game will load all .hhh files in the plugins folder. If you're not sure where to put it, you can place it in `\MoreHead\Decorations` for better organization).  
   > <mark>⚠️ For multiplayer visibility: You and your friends must have the same `.hhh` file installed to see the correct decorations.</mark><br>
   > <mark>⚠️ Do not package the `MoreHead.dll` file into your Mod, only the `.hhh` file and this mod are needed to enjoy the decorations.</mark>

10. **Start the game and enjoy!**  
    > <mark>Press ESC in-game and find "MoreHead" in the bottom left to access your new decoration.</mark>

## 中文教程

1. **部署 Unity**（推荐 2022.3\* 版本，游戏使用的是内置渲染管线，请注意对应的模型材质），导入 `MoreHead-Asset-Pack_v1.3.unitypackage`。  
   > **📥 [下载 unitypackage](https://github.com/Masaicker/repo-MoreHead/releases/download/v1.3/MoreHead-Asset-Pack_v1.3.unitypackage)**

2. **导入模型，并将 `PlayerAvatar` 预制体拖入场景**。  
   > <mark>确保您的模型与Unity兼容，如果有动画需要正确绑定骨骼。</mark>

3. **找到 `PlayerAvatar` 中的以下部位，对应模型将会摆放的位置**：  

    | 名称 | 描述 | 标签 |
    | --- | --- | --- |
    | **Head decoration (Do Not Move)** | 说话时会上抬的头部 | `head` |
    | **Neck decoration (Do Not Move)** | 下巴/胸口 | `neck` |
    | **Body decoration (Do Not Move)** | 身体 | `body` |
    | **Hip decoration (Do Not Move)** | 臀部/下半身 | `hip` |
    | **L-ARM decoration (Do Not Move)** | 左手臂 | `leftarm` |
    | **R-ARM decoration (Do Not Move)** | 右手臂 | `rightarm` |
    | **L-LEG decoration (Do Not Move)** | 左腿 | `leftleg` |
    | **R-LEG decoration (Do Not Move)** | 右腿 | `rightleg` |
    | **World decoration (Do Not Move)** | 跟随角色位置移动但保持水平方向 | `world` |

    > <mark>⚠️ 这些位置是坐标锚点参考。请勿修改坐标，否则您的模型在游戏中会错位。</mark><br>
    > <mark>💡 `world` 标签适用于需要保持水平方向的装饰品，无论角色如何移动。</mark><br>
    > <mark>💡 四肢相关标签（leftarm、rightarm、leftleg、rightleg）可在游戏中通过"LIMBS"主标签统一查看。</mark>

4. **在目标部位下创建一个空对象（称为A），并命名**。  
   > <mark>此名称将在游戏中显示为您的装饰品名称。</mark>

5. **将模型放入A，并调整到合适位置**。  

6. **拖动A到`Project`窗口，创建预制体**。
   > <mark>⚠️ 提示：装饰模型上请勿添加碰撞体组件。碰撞体可能影响角色物理行为，导致旋转异常或动作不稳定。如需碰撞交互，请考虑使用其他技术实现。</mark>

7. **运行`Tools → Head Decorations Builder`**，拖入A预制体（或更多，支持批量导出），并选择第3步中对应的部位，随后**点击`Add Model to List`**。  
   > <mark>确保选择正确的身体部位（头部/颈部/身体/臀部等），与您放置装饰的位置相匹配。</mark>

8. **点击`Build All AssetBundle`**，然后在`AssetBundles`文件夹中找到`[你的装饰品名].hhh`文件。  
   > <mark>安装时只需要`.hhh`文件，其他生成的文件可以忽略。</mark>

9. **将`.hhh`文件移动至`BepInEx\plugins`**（启动游戏会加载plugins下所有的.hhh文件。如果不知道往哪放，可以放入`\MoreHead\Decorations`便于管理）。  
   > <mark>⚠️ 多人游戏可见性：您和您的朋友必须安装相同的`.hhh`文件才能互相看到正确的装饰。</mark><br>
   > <mark>⚠️ 不要打包`MoreHead.dll`文件到您的Mod中，只需`.hhh`文件和该mod就能畅玩饰品。</mark>

10. **启动游戏，享受装饰！**  
    > <mark>在游戏中按ESC，在左下角找到"MoreHead"即可访问您的新装饰。</mark>

</details>

<br><br>

# Developer API & Extension 开发者API与扩展

<details>
  <summary>Click to see 点击展开</summary>

## English Documentation

---

Starting from version 1.2.4, MoreHead provides public APIs for extension. These interfaces allow developers to programmatically load AssetBundle resources, inject custom scripts, and enhance decoration functionality.

### Public APIs

The following public interfaces are available in the `HeadDecorationManager` class:

| Method | Description |
| --- | --- |
| `LoadExternalAssetBundlesFromAssembly(Assembly assembly)` | Loads all `.hhh` resources embedded in the specified assembly |
| `RecreateUI()` | Refreshes the UI to display newly loaded decorations |
| `GetDecorationsFromAssembly(Assembly assembly)` | Returns a list of all decorations loaded from the specified assembly |
| `GetDecorationGameObjectsFromAssembly(Assembly assembly)` | Returns a list of GameObject prefabs loaded from the specified assembly |
| `GetDecorationByName(Assembly assembly, string decorationName)` | Finds a specific decoration by name from the specified assembly |
| `FindDecorationsByPartialName(Assembly assembly, string partialName)` | Searches for decorations that contain the specified partial name |

---

### Integration Methods

There are two primary ways to leverage MoreHead's API for extended functionality:

#### 1. Embedding AssetBundles as Resources

You can embed your AssetBundle (`.hhh`) files directly into your DLL as embedded resources:

1. Create AssetBundles following standard procedures outlined in the "How to Add Models" section
2. In Visual Studio (or your preferred IDE):
   - Add the `.hhh` files to your project
   - Set their "Build Action" to "Embedded Resource"
3. In your plugin's initialization code:
   ```csharp
   // Load all embedded .hhh resources from your assembly
   Assembly assembly = Assembly.GetExecutingAssembly();
   MoreHead.HeadDecorationManager.LoadExternalAssetBundlesFromAssembly(assembly);
   MoreHead.HeadDecorationManager.RecreateUI();
   ```

> **Note**: Embedding resources into your DLL is not mandatory. You can also use MoreHead's standard methods to load local `.hhh` files and simply use your DLL for script injection and functionality implementation.

---

#### 2. Programmatic Script Injection

You can write scripts that access and modify decorations, regardless of whether they were loaded from your DLL or other sources:

1. Use the provided API to access decoration prefabs
2. Add custom components/scripts to enhance functionality
3. Implement your own logic for decoration behavior

---

### Implementation Example

```csharp
using BepInEx;
using BepInEx.Logging;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace MyCustomDecorations
{
    [BepInPlugin("com.yourname.customdecorations", "Custom Decorations Mod", "1.0.0")]
    [BepInDependency("Mhz.REPOMoreHead", BepInDependency.DependencyFlags.HardDependency)]
    public class CustomDecorationsPlugin : BaseUnityPlugin
    {
        public static CustomDecorationsPlugin Instance { get; private set; }
        private ManualLogSource _logger;
        private const string TARGET_MODEL_NAME = "Cigar"; // Target model name

        private void Awake()
        {
            Instance = this;
            _logger = Logger;
            _logger.LogInfo("Custom Decorations Mod loaded");
            
            // Load decorations and set up handlers
            LoadDecorations();
        }

        private void LoadDecorations()
        {
            try
            {
                // Get current assembly
                Assembly assembly = Assembly.GetExecutingAssembly();
                
                // Load all .hhh resources from current DLL
                MoreHead.HeadDecorationManager.LoadExternalAssetBundlesFromAssembly(assembly);
                
                // Recreate UI to display newly loaded models
                MoreHead.HeadDecorationManager.RecreateUI();
                
                // Find specific models matching the target name
                List<MoreHead.DecorationInfo> targetDecorations = 
                    MoreHead.HeadDecorationManager.FindDecorationsByPartialName(assembly, TARGET_MODEL_NAME);
                
                if (targetDecorations.Count > 0)
                {
                    _logger.LogInfo($"Found {targetDecorations.Count} models matching '{TARGET_MODEL_NAME}'");
                    
                    // Add component to each found target model
                    foreach (var decoration in targetDecorations)
                    {
                        if (decoration.Prefab != null && decoration.Prefab.GetComponent<SpaceKeyHandler>() == null)
                        {
                            _logger.LogInfo($"Adding SpaceKeyHandler to {decoration.DisplayName}");
                            decoration.Prefab.AddComponent<SpaceKeyHandler>();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error processing decorations: {ex.Message}");
            }
        }
        
        // Class for handling decoration behavior
        public class SpaceKeyHandler : MonoBehaviour
        {
            private void Awake()
            {
                // Log when the component is initialized
                Instance._logger.LogInfo($"SpaceKeyHandler initialized on {gameObject.name}");
            }
            
            private void Update()
            {
                // Check for space key press directly in the component
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Simply log the message when triggered
                    Instance._logger.LogInfo($"Space key pressed on decoration: {gameObject.name}");
                }
            }
        }
    }
}
```

> ### ⚠️ Important Notes
> 
> <mark>Scripts don't necessarily need to be attached to the corresponding model - implement them as needed for proper functionality. For example, the sample code above only responds to space key presses when the character is displayed in the menu.</mark>
> 
> #### Why is this happening?
> 
> Because in first-person view, the game sets the local character's `[RIG]` GameObject to **inactive** (`SetActive(false)`).
> 
> * `[RIG]` is the parent object containing the character model, and **MoreHead decorations are also mounted under this hierarchy**.
> * When `[RIG]` is disabled, according to Unity's mechanics, **all components of child objects** will not have their `Update`, `FixedUpdate` and other lifecycle methods **called**.
> * Custom decoration models are also placed under the `[RIG]` hierarchy, so when you directly attach scripts to the model, once you enter first-person view, these scripts **cannot execute normally** because the parent object is disabled.
> * However, in the game **menu interface**, the character model always remains **active**, so scripts attached to decorations can work normally, as Unity only executes component methods on active GameObjects.
> 
> This **difference in activation states** is the key point that explains why the same script behaves differently in different scenarios.
> 
> ### Important Notes About Multiplayer Synchronization
> 
> <mark>Although in first-person view **you cannot see your own decoration model** (because the local [RIG] is disabled), the game still **synchronizes your state over the network**. This means that as long as you **properly handle multiplayer synchronization logic**, other players can still see the changes and actions of your decoration model.</mark>
> 
> <mark>Therefore, even if local scripts don't execute due to [RIG] being disabled, you can still implement interactive effects through **global managers or network synchronization mechanisms**, allowing **all players to see** changes to your decoration model. This creates possibilities for **interactive decorations**.</mark>
> 
> ### Points to Consider When Implementing Model Behavior Logic:
> 
> 1. **Script Mounting Strategy** - Consider using global managers or event systems as alternatives
> 2. **Differences Between Scene Character and Menu Character** - Some scripts may only work in specific contexts
> 3. **Multiplayer Synchronization Issues** - Ensure consistent behavior across network clients
> 4. **Component Activation State** - Components on disabled GameObjects won't execute Update/FixedUpdate methods
> 5. **Resource Management** - Properly initialize and clean up resources to avoid memory leaks
> 6. **Network Authority** - Consider which operations should be executed locally and which need network synchronization
<br><br>

## 中文文档

---

从1.2.4版本开始，MoreHead提供了公开API接口，用于扩展功能。这些接口允许开发者以编程方式加载AssetBundle资源、注入自定义脚本，并增强装饰物功能。

### 公开API接口

以下是`HeadDecorationManager`类中可用的公共接口：

| 方法 | 说明 |
| --- | --- |
| `LoadExternalAssetBundlesFromAssembly(Assembly assembly)` | 加载指定程序集中嵌入的所有`.hhh`资源 |
| `RecreateUI()` | 刷新UI以显示新加载的装饰物 |
| `GetDecorationsFromAssembly(Assembly assembly)` | 返回从指定程序集加载的所有装饰物列表 |
| `GetDecorationGameObjectsFromAssembly(Assembly assembly)` | 返回从指定程序集加载的所有GameObject预制体列表 |
| `GetDecorationByName(Assembly assembly, string decorationName)` | 根据名称查找指定程序集中的特定装饰物 |
| `FindDecorationsByPartialName(Assembly assembly, string partialName)` | 搜索包含指定部分名称的装饰物 |

---

### 集成方法

有两种主要方式利用MoreHead的API进行功能扩展：

#### 1. 将AssetBundle作为资源嵌入

您可以将AssetBundle（`.hhh`）文件直接嵌入到DLL中作为嵌入式资源：

1. 按照"如何添加模型"部分的标准流程创建AssetBundle
2. 在Visual Studio（或您选择的IDE）中：
   - 将`.hhh`文件添加到您的项目
   - 将它们的"生成操作"设置为"嵌入式资源"
3. 在您的插件初始化代码中：
   ```csharp
   // 从您的程序集加载所有嵌入的.hhh资源
   Assembly assembly = Assembly.GetExecutingAssembly();
   MoreHead.HeadDecorationManager.LoadExternalAssetBundlesFromAssembly(assembly);
   MoreHead.HeadDecorationManager.RecreateUI();
   ```

> **注意**：将资源嵌入到DLL中不是必须操作。您也可以使用MoreHead的标准方法加载本地`.hhh`文件，只用您的DLL注入脚本和实现功能。

---

#### 2. 程序化脚本注入

您可以编写脚本访问和修改装饰物，无论它们是从您的DLL还是其他来源加载的：

1. 使用提供的API访问装饰物预制体
2. 添加自定义组件/脚本以增强功能
3. 实现您自己的装饰物行为逻辑

---

### 实现示例

```csharp
using BepInEx;
using BepInEx.Logging;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace MyCustomDecorations
{
    [BepInPlugin("com.yourname.customdecorations", "自定义装饰MOD", "1.0.0")]
    [BepInDependency("Mhz.REPOMoreHead", BepInDependency.DependencyFlags.HardDependency)]
    public class CustomDecorationsPlugin : BaseUnityPlugin
    {
        public static CustomDecorationsPlugin Instance { get; private set; }
        private ManualLogSource _logger;
        private const string TARGET_MODEL_NAME = "Cigar雪茄"; // 目标模型名称

        private void Awake()
        {
            Instance = this;
            _logger = Logger;
            _logger.LogInfo("自定义装饰MOD已加载");
            
            // 加载装饰物并设置处理脚本
            LoadDecorations();
        }

        private void LoadDecorations()
        {
            try
            {
                // 获取当前程序集
                Assembly assembly = Assembly.GetExecutingAssembly();
                
                // 加载当前DLL中的所有.hhh资源
                MoreHead.HeadDecorationManager.LoadExternalAssetBundlesFromAssembly(assembly);
                
                // 重建UI以显示新加载的模型
                MoreHead.HeadDecorationManager.RecreateUI();
                
                // 查找匹配目标名称的特定模型
                List<MoreHead.DecorationInfo> targetDecorations = 
                    MoreHead.HeadDecorationManager.FindDecorationsByPartialName(assembly, TARGET_MODEL_NAME);
                
                if (targetDecorations.Count > 0)
                {
                    _logger.LogInfo($"找到 {targetDecorations.Count} 个匹配 '{TARGET_MODEL_NAME}' 的模型");
                    
                    // 为每个找到的目标模型添加组件
                    foreach (var decoration in targetDecorations)
                    {
                        if (decoration.Prefab != null && decoration.Prefab.GetComponent<SpaceKeyHandler>() == null)
                        {
                            _logger.LogInfo($"为 {decoration.DisplayName} 添加 SpaceKeyHandler");
                            decoration.Prefab.AddComponent<SpaceKeyHandler>();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"处理装饰物时出错: {ex.Message}");
            }
        }
        
        // 用于处理装饰物行为的类
        public class SpaceKeyHandler : MonoBehaviour
        {
            private void Awake()
            {
                // 组件初始化时记录日志
                Instance._logger.LogInfo($"在 {gameObject.name} 上初始化了 SpaceKeyHandler");
            }
            
            private void Update()
            {
                // 直接在组件中检测空格键按下
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // 触发时简单记录消息
                    Instance._logger.LogInfo($"空格键在装饰物上按下: {gameObject.name}");
                }
            }
        }
    }
}
```

> ### ⚠️ 重要提示
> 
> <mark>脚本不一定非要绑定在对应模型上，按需处理才能完善功能。例如上面的示例代码，只能在**菜单里显示角色时**响应空格输出日志。</mark>
> 
> #### 为什么会这样？
> 
> 因为游戏在**第一人称视角**下会将本地角色的`[RIG]`游戏对象设为**非激活状态**（`SetActive(false)`）。
> 
> * `[RIG]`是包含角色模型的父级对象，**MoreHead的装饰模型也被挂载在这个层级下**。
> * 当`[RIG]`被禁用时，根据Unity的工作机制，其下**所有子对象的组件**的`Update`、`FixedUpdate`等生命周期方法**都不会被调用**。
> * 自定义装饰模型同样放置在`[RIG]`层级下，因此当您在模型上直接挂载脚本时，一旦进入第一人称视角，这些脚本就会因为父对象被禁用而**无法正常执行**。
> * 而在游戏**菜单界面**中，角色模型始终保持**激活状态**，此时挂载在装饰上的脚本可以正常工作，这是因为Unity只会执行激活状态游戏对象上的组件方法。
> 
> 这种**激活状态的差异**是关键点，它解释了为什么同样的脚本在不同场景下表现不一致。
> 
> ### 关于多人同步的重要说明
> 
> <mark>虽然在第一人称视角下**您看不到自己的装饰模型**（因为本地[RIG]被禁用），但游戏依然会在**网络中同步您的状态**。这意味着只要您**正确处理多人同步逻辑**，其他玩家仍然可以看到您装饰模型的变化和动作。</mark>
> 
> <mark>因此，即使本地脚本因[RIG]禁用而不执行，您仍然可以通过**全局管理器或网络同步机制**来实现交互效果，让**所有玩家都能看到**您的装饰模型发生的变化。这为创造**交互式装饰**提供了可能性。</mark>
> 
> ### 实现模型行为逻辑时需要注意以下几点：
> 
> 1. **脚本挂载策略** - 考虑使用全局管理器或事件系统等替代方法
> 2. **场景角色和菜单角色的区别** - 某些脚本可能只在特定上下文中工作
> 3. **多人同步问题** - 确保在网络客户端间行为一致
> 4. **组件激活状态** - 禁用的游戏对象上的组件不会执行Update/FixedUpdate方法
> 5. **资源管理** - 正确初始化和清理资源，避免内存泄漏
> 6. **网络权限** - 考虑哪些操作应该在本地执行，哪些需要网络同步

</details>

<br><br>

# Future Update Plans 未来更新计划

- **More hats**  
- **Screenshots for decorations to assist selection** *(not guaranteed)*  

- **更多的帽子**  
- **为装饰物提供截图，方便选择** *(不保证)*  

<br><br>

# Author Information 作者信息

**Masaicker** 马赛克了：[BILIBILI](https://space.bilibili.com/1542613)  
**YurisCat Calypso** 尤里的猫·卡里普索：[BILIBILI](https://space.bilibili.com/1704421)  

For questions and suggestions, submit issues:  
如有问题和建议，请提交 Issues：[GitHub Issues](https://github.com/Masaicker/repo-MoreHead/issues)  

Want to support us with coffee and yellow energy crystals?  
想请我们喝咖啡和购买黄色能量水晶？  
[Donate on Ko-Fi](https://ko-fi.com/masaicker)  

<kbd>Whispering, we are making an indie game! 
<br>悄悄说一句，我们正在制作独立游戏！</kbd>
