using System;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// Buff的所有Tag
    /// Tag的名称可以修改，但不可修改数量和取值
    /// </summary>
    [Flags]
    public enum BuffTag : int
    {
        none = 0,
        tag1 = 1 << 0,
        tag2 = 1 << 1,
        tag3 = 1 << 2,
        tag4 = 1 << 3,
        tag5 = 1 << 4,
        tag6 = 1 << 5,
        tag7 = 1 << 6,
        tag8 = 1 << 7,
        tag9 = 1 << 8,
        tag10 = 1 << 9,
        tag11 = 1 << 10,
        tag12 = 1 << 11,
        tag13 = 1 << 12,
        tag14 = 1 << 13,
        tag15 = 1 << 14,
        tag16 = 1 << 15,
        tag17 = 1 << 16,
        tag18 = 1 << 17,
        tag19 = 1 << 18,
        tag20 = 1 << 19,
        tag21 = 1 << 20,
        tag22 = 1 << 21,
        tag23 = 1 << 22,
        tag24 = 1 << 23,
        tag25 = 1 << 24,
        tag26 = 1 << 25,
        tag27 = 1 << 26,
        tag28 = 1 << 27,
        tag29 = 1 << 28,
        tag30 = 1 << 29,
        tag31 = 1 << 30,
    }
}