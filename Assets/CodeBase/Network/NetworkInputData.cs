using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public float HorizontalInput;
    public float VerticalInput;
    public bool BrakeInput;
}
