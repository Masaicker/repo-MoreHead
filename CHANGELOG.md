# v1.3.11
* 减少了中文字符导致的乱码显示以及控制台不断警告提示的问题
* Reduced garbled text and console warnings caused by Chinese characters

# v1.3.10
* 新增配置选项，可自定义ESC菜单和大厅按钮的位置坐标
* Added config options to customize button position in ESC menu and lobby

# v1.3.9
* 将装备方案切换快捷键从Ctrl+1-5改为F1-F5
* Changed outfit switching shortcuts from Ctrl+1-5 to F1-F5

# v1.3.8
* 在游戏大厅添加了MoreHead按钮
* Added MoreHead button to the lobby

# v1.3.7
* 新增：在界面右侧添加五个快速换装按钮，并支持使用Ctrl+1到Ctrl+5快捷键在游戏中随时切换装备方案
* Added five quick switching buttons on the right side of the interface and keyboard shortcuts (Ctrl+1 to Ctrl+5) for switching outfits anytime during gameplay

# v1.3.6
* 添加模型日志配置选项（默认开启）
* 优化标签按钮布局与间距
* Added model logging config option (enabled by default)
* Improved tag button layout and spacing

# v1.3.5
* 添加 "LIMBS" 主标签，支持手脚部位装饰品的统一管理
* 提供对应 v1.3 的 UnityPackage，支持直接配置四肢相关子标签（leftarm, rightarm, leftleg, rightleg）
* Added "LIMBS" main tag for unified management of arm and leg cosmetics
* Updated the v1.3 UnityPackage with direct support for limb-related tags (leftarm, rightarm, leftleg, rightleg)

# v1.3.4  
* 修复了将版本号识别为模组名称的问题  
* 修复打开改变颜色窗口时，出现多个模型重叠的问题  
* Fixed version numbers being incorrectly identified as mod names  
* Fixed the issue where multiple character models overlap when opening the color change window  

# v1.3.3  
* 修复了退出场景时空引用的问题  
* Fixed null reference issue when exiting a scene  

# v1.3.2  
* 悬停装饰品按钮时显示模组来源  
* 点击当前标签页返回顶部（1.3.0 新增）  
* Show mod names when hovering over decoration buttons  
* Click the current tab to return to the top (added in 1.3.0)  

# v1.3.1
* 添加了一顶Nick'sHat，感谢nick制作的menulib模组，为社区带来便利
* Added Nick's Hat. Thanks to Nick for creating the MenuLib mod, bringing convenience to the community.

# v1.3.0
* 依赖 MenuLib 2.1.3，本版本及以后不再兼容 MenuLib 早期版本
* 优化了UI界面的响应速度
* 修复了一些小问题
* Now requires MenuLib 2.1.3. Starting from this version, earlier versions of MenuLib are no longer supported.
* Optimized UI interface response speed
* Fixed some minor issues

# v1.2.8
* 依赖 MenuLib 2.0.0，本版本及以后不再兼容 MenuLib 1.x
* 修复因 MenuLib 2.0.0 更新导致的兼容性问题，确保 Morehead 正常工作
* Now requires MenuLib 2.0.0. Starting from this version, MenuLib 1.x is no longer supported.
* Fixed compatibility issues caused by the MenuLib 2.0.0 update to ensure Morehead functions properly.

# v1.2.7
* 修复单人模式依然能看到 world 标签装饰品的问题
* Fixed the issue where world-tagged cosmetics were still visible in single-player mode.

# V1.2.6
* 修复了因 world 标签的装饰品导致的翻滚动画异常，以及角色无法完全倒下的问题。特别感谢 AnzunoHagi 朋友的测试，以及多位玩家的问题反馈
* Fixed the issue where decorations with the "world" tag caused ragdoll rolling animation errors and prevented the character from fully falling down.
Special thanks to AnzunoHagi for testing and to multiple players for their feedback.

# v1.2.5
* 按钮现在已按字母顺序（A-Z）排序
* The buttons are now sorted in alphabetical order (A-Z).

# v1.2.4
* 新增公开API接口，支持扩展功能
* 添加模型和资源追踪系统，允许开发者注入自定义脚本
* 支持将AssetBundle作为嵌入式资源加载
* 详情请查看README.md中的"开发者API与扩展"部分
* Added public API interfaces for extensions
* Added model and resource tracking system for custom scripts
* Added support for loading AssetBundles as embedded resources
* For details, see the "Developer API & Extension" section in README

# v1.2.3
* 添加"hip"父级标签，支持在臀部/下半身位置添加装饰品
* 优化body部位装饰品，现在会随角色变矮而同步压缩
* 优化world装饰品位置，避免在第一人称视角中看到
* Added "hip" parent tag for decorations on hip/lower body area
* Improved body decorations to scale with character height
* Optimized world decorations to be invisible in first-person view

# v1.2.2
* 添加标签筛选功能，可按类型查看装饰品
* Added tag filtering to view decorations by type

# v1.2.1
* 添加"world"父级标签，装饰品跟随角色位置但保持水平方向
* 更新AssetBundleBuilder工具支持"world"标签
* Added "world" parent tag for horizontal orientation
* Updated AssetBundleBuilder tool for "world" tag support

# v1.2.0
* 更改装饰品数据保存位置
* 添加清空所有装饰按钮
* Changed cosmetic data save location
* Added clear all accessories button

# v1.1.10
* 添加了十六夜的发型和一把小旗子。
* 1.1.9的readme文件出了点问题，我们重新上传了一份。
* Added Sakuya Izayoi's hairstyle and a small flag
* There was an issue with the readme file for version 1.1.9, so we re-uploaded a new one.

# v1.1.9
* 优化部分代码，略微提升一点点性能
* Optimized some code for a slight performance improvement.

# v1.1.8
* 依赖 MenuLib 1.0.3，本版本及以后不再兼容 MenuLib 1.0.2
* Now requires MenuLib 1.0.3. Starting from this version, MenuLib 1.0.2 is no longer supported.

# v1.1.7
* 添加了创可贴、特殊的头发和"盲视"
* Added band-aids, special hair, and blind vision.

# v1.1.6
* 优化了README
* Optimized the README.

# v1.1.5
* 修复加载问题
* 临时优化了UI显示，但导入模型过多仍可能导致部分按钮被遮挡，无法选中。未来可能会更换饰品选择方案。
* Fixed loading issues.
* Temporarily improved UI display, but excessive model imports may still cause selection issues. A new accessory selection method may be considered in the future.

# v1.1.4
* 优化了README
* Optimized the README.

# v1.1.3
* 修改了数据保存方案，以防止更新后饰品配置数据丢失
* 恢复了Odradek奥卓德克灯光
* 优化了README
* Modified the data saving method to prevent accessory configuration data loss after updates.
* Restored Odradek's lighting.
* Optimized the README.

# v1.1.2
* 优化部分代码
* Optimized some code.

# v1.1.1
* 更加明显的 ON/OFF 区别
* More distinct ON/OFF differentiation.

# v1.1.0
* 为Odradek奥卓德克添加了动画
* Added animations for Odradek.

# v1.0.9
* 优化了UI界面
* Optimized the UI interface.

# v1.0.8
* 修复死亡时更换装饰品复活后不同步的问题
* Fixed an issue where accessories changed upon death were not synchronized after revival.

# v1.0.7
* 为Odradek奥卓德克添加了更加炫酷的灯光效果
* Added more impressive lighting effects for Odradek
# v1.0.6
* 添加了小丑鼻子、草帽、Odradek奥卓德克（带有灯光效果！）、竹蜻蜓（带有动画！）
* Added clown nose, straw hat, Odradek (with lighting effects!), bamboo copter (with animation!)

# v1.0.5
* 现在可以加载plugins文件夹下的所有.hhh
* Now it is possible to load all .hhh files located in the plugins folder.

# v1.0.4
* 优化了滚动界面中按钮被遮挡的问题
* Optimized the issue where buttons were obscured in scrolling interfaces.

# v1.0.2
* 开发者包已经优化(v1.01)，以方便制作者批量的打包模型
* 添加了一根非常酷的，留有轨迹的雪茄
* 吸烟有害健康，但游戏里不，不过说真的，现实世界还是别抽了
* The developer package has been optimized (v1.01) to make it easier for creators to batch package models
* Added a very cool cigar that leaves a trail
* "Smoking is harmful to health, but not in games. However, seriously speaking, it's better not to smoke in the real world."


# v1.0.1
* 提升了部分模组加载器的兼容性
* 开发包已经优化(v1.01)，以方便制作者更好的找到位置锚点
* Improved compatibility with some mod loaders
* The development package has been optimized (v1.01) to help creators better locate position anchors

# v1.0.0
* 初始版本
* Initial release