-- 常用别名初始化
-- 导入脚本
-- OOP
require("Object")
-- 字符串拆分
require("SplitTools")
-- Json 解析
Json = require("JsonUtility");
--CS.UnityEngine = require("UnityEngine")
-- Unity相关的
GameObject = CS.UnityEngine.GameObject
Resources = CS.UnityEngine.resouces
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform
TextAsset = CS.UnityEngine.TextAsset
-- 图集对象类
SpriteAtlas = CS.UnityEngine.SpriteAtlas
Vector3 = CS.UnityEngine.Vector3
Vector2 = CS.UnityEngine.Vector2
UI = CS.UnityEngine.UI
Image = UI.Image
Text = UI.Text

Button = UI.Button
Toggle = UI.Toggle
ScrollRect = UI.ScrollRect
UIBehaviour = CS.UnityEngine.EventSystems.UIBehaviour

Canvas = GameObject.Find("Canvas").transform

-- 自己写的CSharp
-- 获取管理器对象
AssetBundleMgr = CS.AssetBundleMgr.GetInstance()