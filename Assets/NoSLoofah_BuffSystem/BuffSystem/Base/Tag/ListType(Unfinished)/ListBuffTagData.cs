//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//[System.Serializable]
//public class ListBuffTagData
//{
//    [SerializeField] private BuffTag buff;
//    [SerializeField] [Tooltip("存在时使该buff不能生效的tag")] private List<BuffTag> conflictTags;
//    private HashSet<BuffTag> conflictTagsHash;

//    public BuffTag Buff => buff;
//    /// <summary>
//    /// 该标签的Buff在tag存在时是否不能添加
//    /// </summary>
//    /// <param name="tag"></param>
//    /// <returns></returns>
//    public bool ConflictTagsContains(BuffTag tag)
//    {
//        if (conflictTagsHash == null) conflictTagsHash = new HashSet<BuffTag>(conflictTags);
//        return conflictTagsHash.Contains(tag);
//    }
//}
