# Unity auto attach component via attributes

### Installation

Add this as a package to your project by adding the below as an entry to the dependencies in the `/Packages/manifest.json` file:

```json
"com.nrjwolf.autoattachattributes": "https://github.com/Nrjwolf/unity-auto-attach-component-attributes.git"
```
For more information on adding git repositories as a package see the [Git support on Package Manager](https://docs.unity3d.com/Manual/upm-git.html) in the Unity Documentation.


Example
``` c#
using UnityEngine;
using UnityEngine.UI;
using Nrjwolf.Attributes;

public class AttachAttributesExample : MonoBehaviour
{
    [FindObjectOfType]
    [SerializeField]
    private Camera m_Camera;

    [GetComponent]
    [SerializeField]
    private Image m_Image;

    [GetComponentInChildren(true)] // include inactive
    [SerializeField]
    private Button m_Button;

    [AddComponent] // Add component in editor and attach it to field
    [SerializeField]
    private SpringJoint2D m_SpringJoint2D;
}
```

Now all components will automatically attach when you select your gameobject in hierarchy

---

So, you can ask why I need it? Maybe you use code like this and do not know, that this is bad for perfomance
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

You can cache via code for example in Start method or you can use drag and drop in editor. But you don't need to do this with this attributes.

Reddit : https://www.reddit.com/r/Nrjwolf/    
Telegram : https://t.me/nrjwolf_live 
