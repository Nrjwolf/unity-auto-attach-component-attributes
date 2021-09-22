# Unity auto attach component via attributes

<b>Forum Thread</b> https://forum.unity.com/threads/auto-attach-components-via-attributes.928098/

### Installation

Add this as a package to your project by adding the below as an entry to the dependencies in the `/Packages/manifest.json` file:

```json
"nrjwolf.games.attachattributes": "https://github.com/Nrjwolf/unity-auto-attach-component-attributes.git"
```
For more information on adding git repositories as a package see the [Git support on Package Manager](https://docs.unity3d.com/Manual/upm-git.html) in the Unity Documentation.

### Preview video

[![Play](https://img.youtube.com/vi/LdiJdgHrBl4/0.jpg)](https://www.youtube.com/watch?v=LdiJdgHrBl4)

### Example
``` c#
 using Nrjwolf.Tools.AttachAttributes;

 [FindObjectOfType]
 [SerializeField] private Camera m_Camera;
 
 [GetComponent] 
 [SerializeField] private Image m_Image;
 
 [GetComponentInChildren(true)] // include inactive
 [SerializeField] private Button m_Button;

 [GetComponentInChildren("Buttons/Button1")] // Get the component from the children by path "Buttons/Button1" in hierarchy
 [SerializeField] private Button m_Button;
 
 [AddComponent] // Add component in editor and attach it to field
 [SerializeField] private SpringJoint2D m_SpringJoint2D;
 
 [GetComponentInParent] // Get component from parent
 [SerializeField] private Canvas m_Canvas;
```

Now all components will automatically attach when you select your gameobject in hierarchy

![](https://github.com/Nrjwolf/unity-auto-attach-component-attributes/blob/master/.github/images/globalSettingInContextMenu.png "Global active/deactive") </br>
You can turn it on/off in component context menu or via ```Tools/Nrjwolf/AttachAttributes```

### About

This asset help you to auto attach components into your serialized fields in inpector. I started use it to avoid every time assign components in ```Awake/Start``` 
function. <br> <br>
So, you can ask why I need it? Well, maybe you use code like this and do not know, that this is bad for perfomance
``` c#
private Transform m_CachedTransform
public Transform transform
{
  get
  {
    if (m_CachedTransform == null)
      m_CachedTransform = InternalGetTransform();
    return m_CachedTransform;
  }
}
```
You can read about here: https://blogs.unity3d.com/ru/2014/05/16/custom-operator-should-we-keep-it/

---

>Telegram : https://t.me/nrjwolf_games <br> 
>Discord : https://discord.gg/jwPVsat <br>
>Reddit : https://www.reddit.com/r/Nrjwolf/ <br>
>Twitter : https://twitter.com/nrjwolf <br>
