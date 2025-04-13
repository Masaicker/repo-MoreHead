# MoreHead Frequently Asked Questions

## 1. Why is there no MoreHead button after entering the game?
The button only appears after **fully entering the game**, which means after entering the level and being able to control your character. You can press ESC and find the MoreHead button in the lower left corner.

## 2. Why are other players wearing my cosmetics?
Make sure all players have the same version of MoreHead installed. If someone hasn't installed the mod, they will see the default appearance, while you might see them wearing your cosmetics.

## 3. What should I do if MoreHead shows an error at startup?
MoreHead relies on the MenuLib library. If MenuLib is updated, it might cause errors. Please roll back the MenuLib version, and MoreHead will be updated later to support the latest version of MenuLib.

## 4.Why does the game throw errors or behave strangely when using certain mods alongside MoreHead?
Some extension mods may accidentally include an older version of `MoreHead.dll` in their package. This can lead to conflicts during loading and result in bugs or crashes. Try checking the mod contents—if you find a bundled `MoreHead.dll`, try removing it or contacting the mod creator.

## 5. Why are some cosmetics, especially base cosmetics, not syncing properly even though we have the same mod version?
This may happen if different mod managers are used. We recommend using the **same mod manager** for everyone.  
Currently, we know that **r2modman** can sometimes cause Chinese characters in the base cosmetic list to be garbled during loading. Although our tests showed that garbled base cosmetics still sync correctly, a small number of players have reported desync issues — you can check this discussion for more info:   https://github.com/Masaicker/repo-MoreHead/issues/14   https://github.com/Masaicker/repo-MoreHead/issues/27  .

At the moment, there’s no specific fix for this. If other add-on cosmetic mods display normally but base cosmetics do not, the problem is likely caused by the **font encoding issue during loading**.

