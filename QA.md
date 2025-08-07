# MoreHead Frequently Asked Questions

## 1. Why is there no MoreHead button after entering the game?
The button appears both **in the lobby** and **after fully entering the game**. You can find the MoreHead button in the lower left corner either while in the lobby preparing for a game, or after entering a level (by pressing ESC).

You can also customize the button position in both locations through the config (LobbyButtonPosX/Y and EscButtonPosX/Y options) if it overlaps with other mods.

## 2. Why are other players wearing my cosmetics?
Make sure all players have the same version of MoreHead installed. If someone hasn't installed the mod, they will see the default appearance, while you might see them wearing your cosmetics.

## 3. What should I do if MoreHead shows an error at startup?
MoreHead relies on the MenuLib library. If MenuLib is updated, it might cause errors. Please roll back the MenuLib version, and MoreHead will be updated later to support the latest version of MenuLib.

## 4.Why does the game throw errors or behave strangely when using certain mods alongside MoreHead?
Some extension mods may accidentally include an older version of `MoreHead.dll` in their package. This can lead to conflicts during loading and result in bugs or crashes. Try checking the mod contents—if you find a bundled `MoreHead.dll`, try removing it or contacting the mod creator.

## 5. Why are some cosmetics, especially base cosmetics, not syncing properly even though we have the same mod version?
This may happen if different mod managers are used. We recommend using the **same mod manager** for everyone.  
Currently, we know that **r2modman** can sometimes cause Chinese characters in the base cosmetic list to be garbled during loading. Although our tests showed that garbled base cosmetics still sync correctly, a small number of players have reported desync issues — you can check this discussion for more info:   https://github.com/Masaicker/repo-MoreHead/issues/14   https://github.com/Masaicker/repo-MoreHead/issues/27  .

At the moment, there's no specific fix for this. If other add-on cosmetic mods display normally but base cosmetics do not, the problem is likely caused by the **font encoding issue during loading**.

## 6. What should I do if I experience strength loss during gameplay?
If you encounter strength loss issues during gameplay, try downloading the **fixstrength** mod to resolve this problem. This mod can help restore and maintain your character's strength values properly.

---
# MoreHead 常见问题解答

## 1. 为什么进入游戏后没有MoreHead按钮？
按钮会出现在 **大厅** 和 **完全进入游戏后** 两个地方。你可以在大厅准备游戏时，或进入关卡后（按ESC键），在左下角找到MoreHead按钮。

如果按钮与其他模组重叠，你也可以通过配置（LobbyButtonPosX/Y 和 EscButtonPosX/Y 选项）在两个位置自定义按钮位置。

## 2. 为什么其他玩家穿着我的饰品？
请确保所有玩家都安装了相同版本的MoreHead。如果有人没有安装该模组，他们将看到默认外观，而你可能会看到他们穿着你的饰品。

## 3. 如果MoreHead在启动时显示错误该怎么办？
MoreHead依赖于MenuLib库。如果MenuLib更新，可能会导致错误。请回滚MenuLib版本，MoreHead稍后会更新以支持最新版本的MenuLib。

## 4. 为什么在使用某些模组与MoreHead一起时，游戏会报错或行为异常？
一些扩展模组可能会意外地在其包中包含旧版本的 `MoreHead.dll`。这可能导致加载过程中的冲突，并导致错误或崩溃。请尝试检查模组内容——如果你发现捆绑的 `MoreHead.dll`，请尝试删除它或联系模组创建者。

## 5. 为什么有些饰品，特别是基础饰品，即使我们有相同的模组版本也无法正常同步？
如果使用不同的模组管理器，可能会发生这种情况。我们建议所有人都使用 **相同的模组管理器**。
目前，我们知道 **r2modman** 有时会导致基础饰品列表中的中文字符在加载时乱码。虽然我们的测试表明，乱码的基础饰品仍然可以正确同步，但有少数玩家报告了不同步的问题——你可以查看此讨论以获取更多信息：https://github.com/Masaicker/repo-MoreHead/issues/14 https://github.com/Masaicker/repo-MoreHead/issues/27 。

目前，对此没有特定的修复方法。如果其他附加饰品模组显示正常但基础饰品不正常，问题很可能是由 **加载过程中的字体编码问题** 引起的。

## 6. 如果在游戏过程中遇到力量丢失问题该怎么办？
如果在游戏过程中遇到力量丢失问题，请尝试下载 **fixstrength** 模组来解决此问题。该模组可以帮助正确恢复和维持你角色的力量值。
