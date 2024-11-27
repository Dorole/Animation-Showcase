using System;

[Flags]
public enum ECharacterState 
{
    NONE        =   0, 
    STANDING    =   1 << 0,
    SITTING     =   1 << 1,
    HORIZONTAL  =   1 << 2
}
